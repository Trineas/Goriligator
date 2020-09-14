using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningCutscne : MonoBehaviour
{
    public Image blackScreen;
    public float blackScreenFadeSpeed;
    public bool fadeToBlack, fadeFromBlack;
    public string levelToLoad;

    public Image image1, image2, image3, image4, image5, image6, image7, image8;
    public int scissorsSound, lightOnSound, laughingSound1, laughingSound2, sewingSound;

    void Start()
    {
        PlayCutscene();
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
    }

    IEnumerator PlayCutsceneCo()
    {
        image1.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);

        image2.gameObject.SetActive(true);
        AudioManager.instance.PlaySFX(lightOnSound);
        yield return new WaitForSeconds(2f);

        image3.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);

        image4.gameObject.SetActive(true);
        AudioManager.instance.PlaySFX(lightOnSound);
        yield return new WaitForSeconds(2f);

        image5.gameObject.SetActive(true);
        AudioManager.instance.PlaySFX(laughingSound1);
        yield return new WaitForSeconds(2f);

        image6.gameObject.SetActive(true);
        AudioManager.instance.PlaySFX(scissorsSound);
        yield return new WaitForSeconds(2f);

        image7.gameObject.SetActive(true);
        AudioManager.instance.PlaySFX(sewingSound);
        yield return new WaitForSeconds(6f);

        image8.gameObject.SetActive(true);
        AudioManager.instance.PlaySFX(laughingSound2);
        yield return new WaitForSeconds(4f);

        fadeToBlack = true;
        yield return new WaitForSeconds(3.1f);

        SceneManager.LoadScene(levelToLoad);
    }

    public void PlayCutscene()
    {
        StartCoroutine(PlayCutsceneCo());
    }
}
