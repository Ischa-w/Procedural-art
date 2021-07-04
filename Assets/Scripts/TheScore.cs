using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheScore : MonoBehaviour
{
    [HideInInspector] public int theScore = 0;
    [SerializeField] Text scoreText;

    [SerializeField] public Color hitColor;
    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Coins = " + theScore;
    }
}
