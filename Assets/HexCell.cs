using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
using Rand = System.Random;

public class HexCell : MonoBehaviour
{
    //Class for anything relating to an individual cell
    /*
     * This next attribute is the "Color" of the Cell
     * The following list is what those numbers means in terms of colors
     * 0 - Blue
     * 1 - Yellow
     * 2 - Red
     * 3 - Green
     */
    public int color;

    public bool wasSolved = false;

    public Sprite debugSprite;

    public GameObject[] neighbors;
    
    private ArrayList hints = new ArrayList();
    
    private int hintIndex = 0;

    private bool isReady = false;

    private GameObject fill= null;
    private float speed = 25;

    private readonly Vector3[] hintLocations =
    {
        new Vector3(-0.3f, 0, -1),
        new Vector3(-0.27f, -0.2f, -1),
        new Vector3(-0.1f, -0.33f, -1),
        new Vector3(0.1f, -0.33f, -1),
        new Vector3(0.27f, -0.2f, -1),
        new Vector3(0.3f, 0, -1)
    };

    private void Start()
    {
        color = Random.Range(0, 4);
        neighbors = GetNeighbors();
        if (neighbors.Length <= 1) return;
        DrawHints();
        isReady = true;
        
        var fillColor = color switch
        {
            0 => Color.blue,
            1 => Color.yellow,
            2 => Color.red,
            3 => Color.green,
            _ => Color.magenta
        };
        fill = CreateFillCell(fillColor);
    }

    private void Update()
    {
        if (!isReady)
        {
            neighbors = GetNeighbors();
            if (neighbors.Length <= 1) return;
            isReady = true;
            DrawHints();
            var fillColor = color switch
            {
                0 => Color.blue,
                1 => Color.yellow,
                2 => Color.red,
                3 => Color.green,
                _ => Color.magenta
            };
            fill = CreateFillCell(fillColor);
        }
        else
        {
            if (wasSolved && fill.transform.localScale.x < 1.001f)
            {
                var scale = new Vector3(1.001f, 1.001f, 1);
                fill.transform.localScale = Vector3.Lerp(fill.transform.localScale, scale, speed * Time.deltaTime);
            }
            else if (wasSolved)
            {
                var sprite = transform.GetComponent<SpriteRenderer>();
                sprite.color = color switch
                {
                    0 => Color.blue,
                    1 => Color.yellow,
                    2 => Color.red,
                    3 => Color.green,
                    _ => Color.magenta
                };
            }
            UpdateHints();
        }
    }
    
    private GameObject CreateFillCell(Color fillColor)
    {
        var fill = new GameObject("Fill");
        fill.AddComponent<SpriteRenderer>().sprite = debugSprite;
        var spriteRenderer = fill.GetComponent<SpriteRenderer>();
        spriteRenderer.color = fillColor;
        fill.transform.localScale = new Vector3(0f, 0f, 1);
        fill.transform.parent = transform;
        fill.transform.position = transform.position;
        return fill;
    }

    private void UpdateHints()
    {
        int i = 0;
        foreach (var obj in hints)
        {
            var hint = (GameObject)obj;
            if (hint == null)
            {
                hints.ToArray()[i] = null;
                continue;
            }
            if (hint.GetComponent<Hint>().cellContainer.GetComponent<HexCell>().wasSolved)
            {
                Destroy(hint);
            }
        }
    }

    public int getNumNeighbors()
    {
        var neighborsList = new ArrayList();
        Vector3[] directionArray =
        {
            new Vector3(1, 1, 0),
            new Vector3(1,0,0),
            new Vector3(1,-1,0),
            new Vector3(-1,-1,0),
            new Vector3(-1,0,0),
            new Vector3(-1,1,0)
        };
        for (var i = 0; i < 6; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(directionArray[i]), 
                    out hit))
            {
                neighborsList.Add(hit.collider.gameObject);
            }
        }

        return neighborsList.Count;
    }
    
    private GameObject[] GetNeighbors()
    {
        var neighborsList = new ArrayList();
        Vector3[] directionArray =
        {
            new Vector3(1, 1, 0),
            new Vector3(1,0,0),
            new Vector3(1,-1,0),
            new Vector3(-1,-1,0),
            new Vector3(-1,0,0),
            new Vector3(-1,1,0)
        };
        for (var i = 0; i < 6; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(directionArray[i]), 
                   out hit))
            {
                if (hit.collider.gameObject.name == "Hexagon")
                {
                    neighborsList.Add(hit.collider.gameObject);
                }
            }
        }

        var tmpNeighbors = new GameObject[neighborsList.Count];

        for (var i = 0; i < neighborsList.Count; i++)
        {
            tmpNeighbors[i] = (GameObject)neighborsList[i];
        }
        
        
        return tmpNeighbors;
    }

    public void DrawHints()
    {
        var locationIndex = 0;
        var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (neighbors.Length == 0)
        {
            neighbors = GetNeighbors();
        }
        
        Shuffle(neighbors);

        foreach (var cell in neighbors)
        {
            var currentHints = gameManager.getNumHints();
            var maxHints = gameManager.getMaxHints();
            var hintsAttempted = gameManager.getHintsAttempted();
            var totalPossible = HexGrid.getTotalHints();

            var probability = (maxHints - currentHints) / (float)(totalPossible - hintsAttempted);
            var randomFloat = Random.value;
            
            if (cell != null && probability >= randomFloat)
            {
                var hint = new GameObject("Hint");
                hint.AddComponent<SpriteRenderer>().sprite = debugSprite;
                hint.transform.localScale = new Vector3(0.2f, 0.2f, 1);
                hint.transform.parent = transform;
                hint.transform.position = transform.position;
                hint.transform.localPosition = hintLocations[locationIndex];
                hint.AddComponent<Hint>().cellContainer = cell;

                var hintColor = cell.GetComponent<HexCell>().color;
                hint.GetComponent<SpriteRenderer>().color = hintColor switch
                {
                    0 => Color.blue,
                    1 => Color.yellow,
                    2 => Color.red,
                    3 => Color.green,
                    _ => Color.white
                };

                var shadow = new GameObject("Shadow");
                shadow.AddComponent<SpriteRenderer>().sprite = debugSprite;
                shadow.transform.localScale = new Vector3(0.3f, 0.3f, 1);
                shadow.transform.parent = hint.transform;
                shadow.transform.position = transform.position;
                shadow.transform.localPosition = new Vector3(0, 0, 0.5f);
                shadow.GetComponent<SpriteRenderer>().color = new Color(29f / 255f, 29f / 255f, 29f / 255f, 1);

                hints.Add(hint);
                hintIndex++;
                locationIndex++;
                gameManager.addHint();
            }

            gameManager.attemptHint();
        }
        hintIndex = 0;
    }

    private static void Shuffle<T>(IList<T> ts) {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i) {
            var r = Random.Range(i, count);
            (ts[i], ts[r]) = (ts[r], ts[i]);
        }
    }
}
