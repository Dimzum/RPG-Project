using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    #region Singleton

    public static PlayerManager instance;

    void Awake() {
        instance = this;
    }

    #endregion

    public GameObject player;

    public void KillPlayer() {
        float delay = 3.0f;

        Debug.Log("GAME OVER! RESTARTING THE GAME IN " + delay + " Seconds.");
        StartCoroutine(RestartGame(delay));

        //Debug.Log("GAME OVER! RESTARTING THE GAME.");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator RestartGame(float delay) {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
