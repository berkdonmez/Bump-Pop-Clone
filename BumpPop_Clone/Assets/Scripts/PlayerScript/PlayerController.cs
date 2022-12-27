using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRb;
    CameraController _cameraController;

    float playerVerticalSpeed = 2000f;
    float boundRate = 32;
    bool isClick;

    public bool didHitIt;
    public GameObject ThrowLine;
    public GameObject NextLevelPanel;
    public GameObject RestartGamePanel;


    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        _cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
        isClick = true;
        didHitIt = true;
    }


    private void Update()
    {
        PlayerMovement();
        zBound();
        ControlTheBallStop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            collision.rigidbody.GetComponent<Rigidbody>().isKinematic = false;
            isClick = true;
            _cameraController.isShoot = true;
            this.gameObject.GetComponent<Renderer>().material.color = collision.gameObject.GetComponent<Renderer>().material.color;
            StartCoroutine(DelayThrowLine());
            didHitIt = true;
        }
    }

    private void OnCollisionEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("FinishPoint"))
        {
            NextLevelPanel.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            playerRb.AddForce(Vector3.forward * 20);
        }
    }

    void PlayerMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isClick == true)
            {
                playerRb.velocity = (ThrowLine.gameObject.transform.forward * playerVerticalSpeed * Time.fixedDeltaTime);
                ThrowLine.SetActive(false);
                isClick = false;
                didHitIt = false;
            }
        }
    }

    void zBound()
    {
        if (transform.position.z < -boundRate)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -boundRate);
        }
    }

    public void OpenThrowLine()
    {
        ThrowLine.transform.rotation = new Quaternion(0, 0, 0, 0);
        ThrowLine.SetActive(true);
    }

    void ControlTheBallStop()
    {
        if (playerRb.velocity.magnitude < 2f && didHitIt == false)
        {
            StartCoroutine(DestroyThePlayer());
            RestartGamePanel.SetActive(true);
        }
    }

    // IEnumerators..
    IEnumerator DelayThrowLine()
    {
        ThrowLine.SetActive(false);
        yield return new WaitForSecondsRealtime(3f);
        ThrowLine.SetActive(true);
    }

    IEnumerator DestroyThePlayer()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
