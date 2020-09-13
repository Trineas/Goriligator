﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public string nextLevelToLoad;

    public bool trainActive, toiletActive;

    public int trainCrashSound, toiletFlushSound;

    public GameObject player;

    IEnumerator LoadNextLevelCo()
    {
        UIManager.instance.fadeToBlack = true;

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(nextLevelToLoad);
        PlayerController.instance.stopMove = false;
    }

    IEnumerator DeathByTrainCo()
    {
        UIManager.instance.blackScreen.color = new Color(UIManager.instance.blackScreen.color.r, UIManager.instance.blackScreen.color.g, UIManager.instance.blackScreen.color.b, 1f);
        AudioManager.instance.PlaySFX(trainCrashSound);
        player.transform.position = new Vector3(0f, 10f, 0f);

        yield return new WaitForSeconds(4f);
        UIManager.instance.fadeFromBlack = true;
        UIManager.instance.gameOverScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        UIManager.instance.fadeToBlack = true;
        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene(nextLevelToLoad);
        PlayerController.instance.stopMove = false;
    }

    IEnumerator DeathByToiletCo()
    {
        UIManager.instance.blackScreen.color = new Color(UIManager.instance.blackScreen.color.r, UIManager.instance.blackScreen.color.g, UIManager.instance.blackScreen.color.b, 1f);
        AudioManager.instance.PlaySFX(toiletFlushSound);
        player.transform.position = new Vector3(0f, 10f, 0f);

        yield return new WaitForSeconds(4f);
        UIManager.instance.fadeFromBlack = true;
        UIManager.instance.gameOverScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        UIManager.instance.fadeToBlack = true;
        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene(nextLevelToLoad);
        PlayerController.instance.stopMove = false;
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevelCo());
    }

    public void DeathByTrain()
    {
        StartCoroutine(DeathByTrainCo());
    }

    public void DeathByToilet()
    {
        StartCoroutine(DeathByToiletCo());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.stopMove = true;

            if (trainActive)
            {
                DeathByTrain();
            }
            else if (toiletActive)
            {
                DeathByToilet();
            }
            else
            {
                LoadNextLevel();
            }

        }
    }
}
