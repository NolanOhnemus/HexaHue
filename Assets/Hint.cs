using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cellContainer;

    public void Update()
    {
        var colorInt = cellContainer.GetComponent<HexCell>().color;
        var renderer =GetComponent<SpriteRenderer>();
        renderer.color = colorInt switch
        {
            0 => Color.blue,
            1 => Color.yellow,
            2 => Color.red,
            3 => Color.green,
            _ => Color.white
        };
        if (cellContainer.GetComponent<HexCell>().wasSolved)
        {
            Destroy(gameObject);
        }
    }
}
