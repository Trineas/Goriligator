using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EndingCutscene : MonoBehaviour
{
    public Image blackScreen;
    public float blackScreenFadeSpeed;
    public bool fadeToBlack, fadeFromBlack;

    public string sceneToLoad;

    public Image image1, image2, image3, image4, image5, image6;
    public int tyingSound, countdownSound, fireworksSound;

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
        AudioManager.instance.PlaySFX(tyingSound);
        yield return new WaitForSeconds(2f);

        image3.gameObject.SetActive(true);
        AudioManager.instance.PlaySFX(countdownSound);
        yield return new WaitForSeconds(2f);

        image4.gameObject.SetActive(true);
        AudioManager.instance.PlaySFX(fireworksSound);
        yield return new WaitForSeconds(2.75f);

        image5.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);

        fadeToBlack = true;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneToLoad);
    }

    public void PlayCutscene()
    {
        StartCoroutine(PlayCutsceneCo());
    }
}
