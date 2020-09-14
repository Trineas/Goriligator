using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public Image blackScreen;
    public float blackScreenFadeSpeed;
    public bool fadeToBlack, fadeFromBlack;

    public string sceneToLoad;

    private void Start()
    {
        CreditsCounter();
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

    IEnumerator CreditsCounterCo()
    {
        yield return new WaitForSeconds(55f);
        fadeToBlack = true;

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneToLoad);
    }

    public void CreditsCounter()
    {
        StartCoroutine(CreditsCounterCo());
    }
}
