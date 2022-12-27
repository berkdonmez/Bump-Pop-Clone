using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    Vector3 offset = new Vector3(0, 35, -20);



    public bool isShoot;


    void LateUpdate()
    {
        if (isShoot == true)
        {
            player = GameManager.Instance.FindClosesBall();

            if (GameManager.Instance.playerRigidbody == null || GameManager.Instance.playerRigidbody != player.gameObject.GetComponent<Rigidbody>())
            {
                GameManager.Instance.playerRigidbody = player.gameObject.GetComponent<Rigidbody>();
            }
            GameManager.Instance.CheckVelocity();
        }

        transform.position = Vector3.Lerp(transform.position, player.position + offset, Time.deltaTime);

    }




}
