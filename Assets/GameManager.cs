using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    private int _currentColor;
    private int _errorsLeft = 4;
    private GameObject _previousSelection;
    public GameObject endCanvas;
    public GameObject hexGrid;
    public Canvas optionsMenu;
    public Canvas pauseMenu;
    public Canvas startMenu;
    private int maxHints;
    private int numHints;
    private int hintsAttempted;
    public AudioSource cameraAudioSource;
    public AudioSource errorAudioSource;
    public AudioSource correctAudioSource;
    private bool isOver = false;
    private bool isPaused = false;

    // These go unused but are good quick references.
    //private int[] easyMaxHintArray = { 0, 0, 24, 43, 73, 111, 135, 162, 183, 210, 225};
    //private int[] normalMaxHintArray = { 0, 0, 5, 24, 43, 73, 111, 135, 162, 183, 210};
    //private int[] hardMaxHintArray = { 0, 0, 3, 13, 26, 52, 79, 111, 135, 162, 183};
    
    private int[,] maxHintArray = new int [3,11]
    {
        { 0, 0, 24, 43, 73, 111, 135, 162, 183, 210, 225},
        { 0, 0, 5, 24, 43, 73, 111, 135, 162, 183, 210},
        { 0, 0, 3, 13, 26, 52, 79, 111, 135, 162, 183}
    };
    
    

    public void attemptHint()
    {
        hintsAttempted++;
    }

    public int getHintsAttempted()
    {
        return hintsAttempted;
    }

    public void addHint()
    {
        numHints++;
    }

    public int getNumHints()
    {
        return numHints;
    }

    public int getMaxHints()
    {
        return maxHints;
    }
    
    private void Awake()
    {
        _previousSelection = GameObject.Find("Blue Selector");
        numHints = 0;
        hintsAttempted = 0;
        var difficulty = CrossScene.GetDifficulty();
        var size = CrossScene.GetGridSize();

        if (difficulty < 0 || size < 0)
        {
            difficulty = 2;
            size = 6;
        }
        
        maxHints = maxHintArray[difficulty, size];
        if (cameraAudioSource == null) return;
        cameraAudioSource.volume = CrossScene.GetSoundFXVolume();
        errorAudioSource.volume = CrossScene.GetSoundFXVolume();
        correctAudioSource.volume = CrossScene.GetSoundFXVolume();
    }

    public void OpenPause()
    {
        cameraAudioSource.Play();
        isPaused = true;
        pauseMenu.gameObject.SetActive(true);
    }

    public void ExitPause()
    {
        cameraAudioSource.Play();
        isPaused = false;
        pauseMenu.gameObject.SetActive(false);
    }
    
    public void GoToOptions()
    {
        optionsMenu.gameObject.SetActive(true);
        pauseMenu.gameObject.SetActive(false);
    }

    public void ChangeGameSettings()
    {
        endCanvas.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        startMenu.gameObject.SetActive(true);
    }

    public void ExitGameSettings()
    {
        if (isOver)
        {
            endCanvas.gameObject.SetActive(true);
        }
        else if (isPaused)
        {
            pauseMenu.gameObject.SetActive(true);
        }
        startMenu.gameObject.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Close()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    private void Update()
    {

        CheckForWin();
        
        if (_errorsLeft <= 0 && !isOver)
        {
            var endText = endCanvas.transform.GetChild(1);
            endText.GetComponent<TextMeshProUGUI>().text = "You ran out of tries... \n Try Again?";
            endCanvas.SetActive(true);
            isOver = true;
        }
        
        if (Input.GetMouseButtonDown(0) && _errorsLeft > 0 && !isPaused)
        {
            System.Diagnostics.Debug.Assert(Camera.main != null, "Camera.main != null");
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var raycastHit, 100f))
            {
                if (raycastHit.collider != null)
                {
                    if (raycastHit.collider.gameObject.name == "Hexagon")
                    {
                        var hitHexCell = raycastHit.collider.gameObject.GetComponent<HexCell>();
                        var hitColor = hitHexCell.color;
                        if (hitColor == _currentColor)
                        {
                            hitHexCell.wasSolved = true;
                            correctAudioSource.Play();
                        }
                        else
                        {
                            if (!hitHexCell.wasSolved)
                            {
                                errorAudioSource.Play();
                                var indicatorName = "Life Indicator " + _errorsLeft;
                                _errorsLeft--;
                                // color is 2B2B2B
                                var lifeIndicator = GameObject.Find(indicatorName);
                                lifeIndicator.gameObject.GetComponent<SpriteRenderer>().color = 
                                    new Color(29f / 255f, 29f / 255f, 29f / 255f, 1);
                            }
                        }
                        
                    }
                    else if (raycastHit.collider.gameObject.name.Contains("Selector"))
                    {
                        cameraAudioSource.Play();
                        _currentColor = raycastHit.collider.gameObject.GetComponent<ColorSelector>().color;
                        _previousSelection.GetComponent<ColorSelector>().isSelected = false;
                        GameObject o;
                        (o = raycastHit.collider.gameObject).GetComponent<ColorSelector>().isSelected = true;
                        _previousSelection = o;
                    }
                }
            }
        }
    }

    private void CheckForWin()
    {
        if (hexGrid.GetComponent<HexGrid>().IsSolved() && !isOver)
        {
            var endText = endCanvas.transform.GetChild(1);
            endText.GetComponent<TextMeshProUGUI>().text = "You Won! \nPlay again?";
            endCanvas.SetActive(true);
            isOver = true;
        }
    }
}
