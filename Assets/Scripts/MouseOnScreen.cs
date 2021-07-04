using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOnScreen : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 30;
    }
    // Start is called before the first frame update
    private void OnMouseEnter()
    {
        Debug.Log("inwindwo");
        Time.timeScale = 1f;
        Application.targetFrameRate = 30;
    }

    private void OnMouseExit()
    {
        Time.timeScale = 0f;
        Application.targetFrameRate = 15;
    }
}
