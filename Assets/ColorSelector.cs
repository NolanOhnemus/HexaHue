using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSelector : MonoBehaviour
{
    /*
     * This next attribute is the "Color" of the selector
     * The following list is what those numbers means in terms of colors
     * 0 - Blue
     * 1 - Yellow
     * 2 - Red
     * 3 - Green
     */
    public int color;

    public bool isSelected;

    public GameObject selectionOutline;

    private void Update()
    {
        if (isSelected)
        {
            selectionOutline.transform.position = new Vector3(transform.position.x, transform.position.y, 
                                                            (float)(transform.position.z + 3.8));

        }
    }
}
