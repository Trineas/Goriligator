using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string sceneToLoad;

    public int musicToPlay;

    public bool dontMove, cutScenePlaying;

    void Start()
    {
        instance = this;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        AudioManager.instance.PlayMusic(musicToPlay);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !cutScenePlaying || Input.GetKeyDown(KeyCode.Joystick1Button7) && cutScenePlaying)
        {
            PauseUnpause();
        }
    }

    IEnumerator GameOverCo()
    {
        UIManager.instance.fadeToBlack = true;

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(sceneToLoad);
        PlayerController.instance.stopMove = false;
    }

    public void GameOver()
    {
        StartCoroutine(GameOverCo());
    }

    public void PauseUnpause()
    {
        if (UIManager.instance.pauseScreen.activeInHierarchy)
        {
            UIManager.instance.pauseScreen.SetActive(false);
            Time.timeScale = 1f;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        else
        {
            UIManager.instance.pauseScreen.SetActive(true);
            Time.timeScale = 0f;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
