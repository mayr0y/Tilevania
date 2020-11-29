using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {

    [SerializeField] int playerLives = 3;
    [SerializeField] Text lives;

    private void Awake() {
        int numGameSession = FindObjectsOfType<GameSession>().Length;
        if(numGameSession > 1) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start() {
        lives.text = playerLives.ToString();
    }

    public void ProcessPlayerDeath() {
        if (playerLives > 1) {
            TakeLives();
        }
        else {
            ReserGameSession();
        }
    }

    private void ReserGameSession() {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void TakeLives() {
        playerLives--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        lives.text = playerLives.ToString();
    }
}
