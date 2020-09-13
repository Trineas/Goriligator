using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Image blackScreen;
    public float blackScreenFadeSpeed;
    public bool fadeToBlack, fadeFromBlack;

    public Text missionText;
    public float missionTextFadeSpeed;
    public bool fadeTextToBlack, fadeTextFromBlack;

    public GameObject pauseScreen;
    public GameObject pauseFirstButton;

    public Image gameOverScreen;

    public string mainMenu;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (fadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, blackScreenFadeSpeed * Time.deltaTime));

            if (blackScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }

        if (fadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, blackScreenFadeSpeed * Time.deltaTime));

            if (blackScreen.color.a == 0f)
            {
                fadeFromBlack = false;
            }
        }

        if (fadeTextToBlack)
        {
            missionText.color = new Color(missionText.color.r, missionText.color.g, missionText.color.b, Mathf.MoveTowards(missionText.color.a, 1f, missionTextFadeSpeed * Time.deltaTime));

            if (missionText.color.a == 1f)
            {
                fadeTextToBlack = false;
            }
        }

        if (fadeTextFromBlack)
        {
            missionText.color = new Color(missionText.color.r, missionText.color.g, missionText.color.b, Mathf.MoveTowards(missionText.color.a, 0f, missionTextFadeSpeed * Time.deltaTime));

            if (missionText.color.a == 0f)
            {
                fadeTextFromBlack = false;
            }
        }

        if (!fadeTextFromBlack)
        {
            if (Input.anyKey)
            {
                TextFade();
            }
        }
    }

    IEnumerator TextFadeCo()
    {
        yield return new WaitForSeconds(2f);
        fadeTextFromBlack = true;
    }

    public void TextFade()
    {
        StartCoroutine(TextFadeCo());
    }

    public void Resume()
    {
        GameManager.instance.PauseUnpause();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }
}
