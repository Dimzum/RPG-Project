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
        Debug.Log("GAME OVER! RESTARTING THE GAME.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
