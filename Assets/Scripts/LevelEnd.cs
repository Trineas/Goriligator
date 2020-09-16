using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public string nextLevelToLoad;

    public bool trainActive, toiletActive, fireplaceActive, blenderActive, fireworksActive;

    public int trainCrashSound, toiletFlushSound, fireplaceSound, blenderSound, fireworksSound;

    public GameObject player;

    public bool deathCoroutinePlaying;

    private void Update()
    {
        if (deathCoroutinePlaying)
        {
            PeekaBoo.instance.StopAllCoroutines();
        }
    }

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
        deathCoroutinePlaying = false;
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
        deathCoroutinePlaying = false;
        PlayerController.instance.stopMove = false;

    }

    IEnumerator DeathByFireCo()
    {
        UIManager.instance.blackScreen.color = new Color(UIManager.instance.blackScreen.color.r, UIManager.instance.blackScreen.color.g, UIManager.instance.blackScreen.color.b, 1f);
        AudioManager.instance.PlaySFX(fireplaceSound);
        player.transform.position = new Vector3(0f, 10f, 0f);

        yield return new WaitForSeconds(4f);
        UIManager.instance.fadeFromBlack = true;
        UIManager.instance.gameOverScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        UIManager.instance.fadeToBlack = true;
        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene(nextLevelToLoad);
        deathCoroutinePlaying = false;
        PlayerController.instance.stopMove = false;

    }

    IEnumerator DeathByBlenderCo()
    {
        UIManager.instance.blackScreen.color = new Color(UIManager.instance.blackScreen.color.r, UIManager.instance.blackScreen.color.g, UIManager.instance.blackScreen.color.b, 1f);
        AudioManager.instance.PlaySFX(blenderSound);
        player.transform.position = new Vector3(0f, 10f, 0f);

        yield return new WaitForSeconds(4f);
        UIManager.instance.fadeFromBlack = true;
        UIManager.instance.gameOverScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        UIManager.instance.fadeToBlack = true;
        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene(nextLevelToLoad);
        deathCoroutinePlaying = false;
        PlayerController.instance.stopMove = false;

    }
    IEnumerator DeathByFireworksCo()
    {
        UIManager.instance.blackScreen.color = new Color(UIManager.instance.blackScreen.color.r, UIManager.instance.blackScreen.color.g, UIManager.instance.blackScreen.color.b, 1f);
        yield return new WaitForSeconds(2f);

        UIManager.instance.fadeFromBlack = true;
        yield return new WaitForSeconds(2f);
        deathCoroutinePlaying = false;

        SceneManager.LoadScene(nextLevelToLoad);
    }

    public void LoadNextLevel()
    {
        deathCoroutinePlaying = true;
        StartCoroutine(LoadNextLevelCo());
    }

    public void DeathByTrain()
    {
        deathCoroutinePlaying = true;
        StartCoroutine(DeathByTrainCo());
    }

    public void DeathByToilet()
    {
        deathCoroutinePlaying = true;
        StartCoroutine(DeathByToiletCo());
    }

    public void DeathByFire()
    {
        deathCoroutinePlaying = true;
        StartCoroutine(DeathByFireCo());
    }
    public void DeathByBlender()
    {
        deathCoroutinePlaying = true;
        StartCoroutine(DeathByBlenderCo());
    }
    public void DeathByFireworks()
    {
        deathCoroutinePlaying = true;
        StartCoroutine(DeathByFireworksCo());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.stopMove = true;

            PeekaBoo.instance.StopAllCoroutines();

            if (trainActive)
            {
                DeathByTrain();
            }
            else if (toiletActive)
            {
                DeathByToilet();
            }
            else if (fireplaceActive)
            {
                DeathByFire();
            }
            else if (blenderActive)
            {
                DeathByBlender();
            }
            else if (fireworksActive)
            {
                DeathByFireworks();
            }
            else
            {
                LoadNextLevel();
            }

        }
    }
}
