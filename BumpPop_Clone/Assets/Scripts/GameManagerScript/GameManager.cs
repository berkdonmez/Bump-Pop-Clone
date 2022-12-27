using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] CameraController _CameraController;

    public static GameManager Instance;
    public List<Transform> Balls = new List<Transform>();
    public Rigidbody playerRigidbody;
    public Transform FinishLine;
    public Transform InitialPlayer;

    //-------Score---------------------
    public Text Chain1Text;
    public Text Chain2Text;
    public GameObject Chain1;
    public GameObject Chain2;
    public int Chain1Score;
    public int Chain2Score;

    //------Money-----------------------
    public Text MoneyText;
    public float CountMoney;

    void Awake()
    {
        Instance = this;
        Chain1Score = 10;
        Chain2Score = 8;
        CountMoney = 0;
    }

    private void Update()
    {
        Chain1Text.text = Chain1Score.ToString();
        Chain2Text.text = Chain2Score.ToString();
        MoneyText.text = CountMoney.ToString();

        if (Chain1Score <= 0)
        {
            Chain1.SetActive(false);
            Chain1Text.enabled = false;
        }
        if (Chain2Score <= 0)
        {
            Chain2.SetActive(false);
            Chain2Text.enabled = false;
        }
    }


    public void SetNewPlayer()
    {
        player.GetComponent<Collider>().enabled = false;
        playerRigidbody.isKinematic = true;

        InitialPlayer.transform.position = player.transform.position;
        _CameraController.player = InitialPlayer;
        playerRigidbody = InitialPlayer.GetComponent<Rigidbody>();
        Balls.Remove(player);
        Destroy(player.gameObject);
        player = InitialPlayer;
        InitialPlayer.GetComponent<PlayerController>().OpenThrowLine();


    }

    public Transform FindClosesBall()
    {
        Transform ClosesTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (var item in Balls)
        {
            if (item.GetComponent<GreenBalls>().isMoving == false)
            {
                continue;
            }
            Vector3 direction = FinishLine.transform.position - item.transform.position;
            var targetDistance = direction.sqrMagnitude;

            if (targetDistance < maxDistance)
            {
                ClosesTarget = item;
                maxDistance = targetDistance;

            }
        }
        player = ClosesTarget;
        return player;
    }

    public void CheckVelocity()
    {
        if (playerRigidbody == null)
            return;
        if (playerRigidbody.velocity.magnitude < 0.15f)
        {
            playerRigidbody.velocity = Vector3.zero;
            _CameraController.isShoot = false;
            SetNewPlayer();
        }
    }


    public void NextLevel()
    {
        SceneManager.LoadScene(1);
    }


    public void RestartTheGame()
    {
        SceneManager.LoadScene(0);
    }

}
