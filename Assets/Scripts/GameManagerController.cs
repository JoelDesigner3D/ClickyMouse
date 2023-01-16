using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerController : MonoBehaviour
{

    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartBt;

    public GameObject titleScreen;

    public bool isGameActive;

    public AudioSource explosionAudio;
    public AudioSource gameOverAudio;
    public AudioSource music;

    private int score;
    private float spawnRate = 2;


    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;    
    }

    public void GameOver()
    {

        if (isGameActive)
        {
            gameOverAudio.Play();
            music.Stop();
            gameOverText.gameObject.SetActive(true);
            restartBt.gameObject.SetActive(true);
        }

        isGameActive = false;

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;

        spawnRate /= difficulty;

        restartBt.gameObject.SetActive(false);

        StartCoroutine(SpawnTarget());

        UpdateScore(0);
        gameOverText.gameObject.SetActive(false);

        titleScreen.SetActive(false);
    }

    public void PlayExplosion()
    {
        explosionAudio.Play();
    }
}
