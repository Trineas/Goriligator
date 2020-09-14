using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDrop : MonoBehaviour
{
    public GameObject hand, handClosed, handOpen;
    public float speed;
    public Transform originalPosition;

    private bool handMoves;

    void Start()
    {
        handMoves = false;
        HandGrab();
        originalPosition.position = hand.transform.position;
    }

    void Update()
    {
        if (handMoves)
        {
            hand.transform.position = new Vector3(hand.transform.position.x, (Mathf.Lerp(hand.transform.position.y, 16f, speed)), hand.transform.position.z);
        }
    }

    IEnumerator HandGrabCo()
    {
        hand.SetActive(true);
        yield return new WaitForSeconds(0.25f);

        handOpen.SetActive(true);
        handClosed.SetActive(false);
        yield return new WaitForSeconds(2f);

        handOpen.SetActive(false);
        handClosed.SetActive(true);
        yield return new WaitForSeconds(0.25f);

        handMoves = true;
        yield return new WaitForSeconds(2f);

        hand.SetActive(false);
    }

    public void HandGrab()
    {
        StartCoroutine(HandGrabCo());
    }
}
