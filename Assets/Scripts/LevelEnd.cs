using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public string nextLevelToLoad;

    IEnumerator LoadNextLevelCo()
    {
        UIManager.instance.fadeToBlack = true;

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(nextLevelToLoad);
        PlayerController.instance.stopMove = false;
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevelCo());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.stopMove = true;
            LoadNextLevel();
        }
    }
}
