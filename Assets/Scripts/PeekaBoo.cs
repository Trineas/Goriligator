using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeekaBoo : MonoBehaviour
{
    public GameObject girl;
    public Animator doorAnim, girlAnim;
    public float speed;

    private bool girlMovesForward, girlMovesBack, youMoved;

    public float intervalMin, intervalMax;

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

        yield return new WaitForSeconds(Random.Range(1.25f, 2.5f));
        girlMovesForward = true;

        yield return new WaitForSeconds(0.5f);
        GameManager.instance.dontMove = true;

        yield return new WaitForSeconds(Random.Range(1f, 5f));
        girlMovesForward = false;
        girlMovesBack = true;

        yield return new WaitForSeconds(0.5f);
        girlMovesBack = false;
        GameManager.instance.dontMove = false;
        doorAnim.SetTrigger("CloseDoor");
    }

    IEnumerator CaughtCo()
    {
        girlAnim.SetTrigger("PointAt");
        PlayerController.instance.stopMove = true;

        yield return new WaitForSeconds(1.5f);

        girlAnim.SetTrigger("FoundYou");

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
