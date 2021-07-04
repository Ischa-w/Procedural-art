using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isHit : MonoBehaviour
{
    bool ishit = false;
    TheScore score;

    Color hittColor = Color.green;

    private void Start()
    {
        GameObject canvas = GameObject.FindWithTag("UICanvas");
        TheScore theScoreScript = canvas.GetComponent<TheScore>();
        hittColor = theScoreScript.hitColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!ishit)
        {
            GetComponent<Renderer>().material.SetColor("Color_31497580c4664319abe2c60c7508ae75", hittColor);
            ishit = true;

            GameObject canvas = GameObject.FindWithTag("UICanvas");
            TheScore theScoreScript = canvas.GetComponent<TheScore>();

            theScoreScript.theScore ++;
        }
        
    }
}
