using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lives : MonoBehaviour
{
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;

    float currentLives = 5;
    string currentScore ;
    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "♡ ♡ ♡ ♡ ♡";
    }

    private void Update()
    {
        if (currentLives > 0)
        {
            scoreText.text = Mathf.Round(Time.timeSinceLevelLoad).ToString();
            currentScore = Mathf.Round(Time.timeSinceLevelLoad).ToString();
        } else
        {
            scoreText.text = currentScore + "  press r to restard";
        }

        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentLives--;
        if (currentLives == 4)
        {
            livesText.text = "♡ ♡ ♡ ♡";

        } else if (currentLives == 3)
        {
            livesText.text = "♡ ♡ ♡";

        } else if (currentLives == 2)
        {
            livesText.text = "♡ ♡";

        } else if (currentLives == 1)
        {
            livesText.text = "♡";

        } else if (currentLives <= 0)
        {
            livesText.text = "Dead";

        } else
        {
            livesText.text = "U cheatin for sure";
        }
    }
}
