using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBalls : MonoBehaviour
{
    Rigidbody ballsRb;
    float ballsSpeed = 200;
    float powerUpSpeed = 20;
    PlayerController _playerController;

    public GameObject PointsCanvasPrefab;
    public bool isMoving;

    private void Awake()
    {
        ballsRb = GetComponent<Rigidbody>();
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ball"))
        {
            collision.rigidbody.GetComponent<Rigidbody>().isKinematic = false;
            ballsRb.AddForce(Vector3.forward * ballsSpeed);
            isMoving = true;
            _playerController.didHitIt = true;
            PointsCanvasPrefab.SetActive(true);
            StartCoroutine(CloseThePointCanvas());
            GameManager.Instance.CountMoney += 0.2f;
        }
        else if (collision.gameObject.CompareTag("FinishPoint"))
        {
            _playerController.NextLevelPanel.SetActive(true);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("chain1"))
        {
            GameManager.Instance.Chain1Score--;
        }
        else if (other.gameObject.CompareTag("chain2"))
        {
            GameManager.Instance.Chain2Score--;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            ballsRb.AddForce(Vector3.forward * powerUpSpeed);
        }
    }



    IEnumerator CloseThePointCanvas()
    {
        yield return new WaitForSeconds(1f);
        PointsCanvasPrefab.SetActive(false);
    }
}
