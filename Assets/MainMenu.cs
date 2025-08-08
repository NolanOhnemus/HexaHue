using System;
using UnityEngine;
using Scene = UnityEngine.SceneManagement.Scene;

public class MainMenu : MonoBehaviour
{

    public Camera mainCamera;

    public Transform leftTransform;
    public Transform rightTransform;
    public Transform centerTransform;
    public Scene gameScene;

    private bool isMoving = false;

    public float speed;

    private Transform target = null;
    
    private void Update()
    {
        if (isMoving)
        {
            var step = speed * Time.deltaTime;
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, target.position, step);
            if (Vector3.Distance(mainCamera.transform.position, target.position) < 0.001f)
            {
                target = null;
                isMoving = false;
            }
        }
        else
        {
            if (target != null)
            {
                isMoving = true;
            }
        }
    }

    public void StartGame()
    {
        target = leftTransform;
        //mainCamera.transform.position = leftTransform.position;
    }

    public void SelectOptions()
    {
        target = rightTransform;
        //mainCamera.transform.position = rightTransform.position;
    }

    public void Back()
    {
        target = centerTransform;
        //mainCamera.transform.position = centerTransform.position;
    }
    
    public void QuitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
