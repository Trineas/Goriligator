using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeekaBoo : MonoBehaviour
{
    public GameObject girl;
    public Animator doorAnim, girlAnim;
    public float speed;

    private bool girlMovesForward, girlMovesBack;

    public float intervalMin, intervalMax;

    public int doorOpenSound, doorCloseSound, caughtSound;

    void Start()
    {
        float randomTime = Random.Range(intervalMin, intervalMax);

        Invoke("OpenDoor", randomTime);

        girlAnim.SetTrigger("ReturnToNormal");

    }

    void Update()
    {
        if (girlMovesForward)
        {
            girl.transform.localPosition = new Vector3(Mathf.Lerp(girl.transform.localPosition.x, 15f, speed * Time.deltaTime), girl.transform.localPosition.y, girl.transform.localPosition.z);
        }

        if (girlMovesBack)
        {
            girl.transform.localPosition = new Vector3(Mathf.Lerp(girl.transform.localPosition.x, 255f, speed * Time.deltaTime), girl.transform.localPosition.y, girl.transform.localPosition.z);
        }

        if (GameManager.instance.dontMove && Input.anyKey)
        {
            GameManager.instance.dontMove = false;
            StopAllCoroutines();

            StartCoroutine(CaughtCo());
        }
    }

    IEnumerator OpenDoorCo()
    {
        doorAnim.SetTrigger("DoorSlam");
        AudioManager.instance.PlaySFX(doorOpenSound);

        yield return new WaitForSeconds(Random.Range(1.25f, 2.5f));
        girlMovesForward = true;
        AudioManager.instance.PlaySFX(Random.Range(9, 17));

        yield return new WaitForSeconds(0.75f);
        GameManager.instance.dontMove = true;

        yield return new WaitForSeconds(Random.Range(1f, 5f));
        girlMovesForward = false;
        girlMovesBack = true;
        GameManager.instance.dontMove = false;

        yield return new WaitForSeconds(0.5f);
        girlMovesBack = false;
        doorAnim.SetTrigger("CloseDoor");
        AudioManager.instance.PlaySFX(doorCloseSound);
    }

    IEnumerator CaughtCo()
    {
        girlAnim.SetTrigger("PointAt");
        AudioManager.instance.PlaySFX(caughtSound);
        PlayerController.instance.stopMove = true;

        yield return new WaitForSeconds(2f);

        girlAnim.SetTrigger("FoundYou");
        AudioManager.instance.PlaySFX(Random.Range(5, 8));

        yield return new WaitForSeconds(0.5f);

        GameManager.instance.GameOver();
    }

    private void OpenDoor()
    {
        float randomTime = Random.Range(intervalMin, intervalMax);

        StartCoroutine(OpenDoorCo());

        Invoke("OpenDoor", randomTime);

    }
}
