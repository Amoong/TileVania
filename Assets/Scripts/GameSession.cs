using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int playerScore = 0;

    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = playerScore.ToString();
    }


    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            StartCoroutine("TakeLife");
        }
        else
        {
            StartCoroutine("ResetGameSession");
        }
    }

    public void AddToScore(int score)
    {
        playerScore += score;
        scoreText.text = playerScore.ToString();
    }

    IEnumerator TakeLife()
    {
        yield return new WaitForSecondsRealtime(2f);

        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }

    IEnumerator ResetGameSession()
    {
        yield return new WaitForSecondsRealtime(2f);

        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
