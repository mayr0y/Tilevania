using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {
    [SerializeField] float secondsToLoad = 1f;

    private void OnTriggerEnter2D(Collider2D collision) {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene() {
        Time.timeScale = 0.4f;
        yield return new WaitForSeconds(secondsToLoad);
        Time.timeScale = 1f;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
