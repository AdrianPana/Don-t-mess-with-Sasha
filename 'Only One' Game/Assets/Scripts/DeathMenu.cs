using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject deathMenuUI;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Player>().HP <= 0)
        {
            Pause();
        }

    }

    void Pause()
    {
        deathMenuUI.SetActive(true);
        gameIsPaused = true;
        Time.timeScale = 0f;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        player.gameObject.SetActive(true);
        SceneManager.LoadScene("Menu");
        
    }
}
