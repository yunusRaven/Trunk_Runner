using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance
    {
        get => instance; set => instance = value;
    }

    public GameObject ballPrefab;
    public GameObject insPoint;
    public GameObject wall;
   
    [SerializeField] private Settings settings;
    public bool canShoot = false;
    private float timer = 0;
    public GameObject player;
    [SerializeField] private Text ballText;

    public Animator Animator;
    void Start()
    {
        Animator.SetBool("isFail", false);
        Animator.SetBool("Dancing", false);
        DOTween.Init();
        settings.ballPoint = 0;
        
    }
    void Update()
    {
        ballText.GetComponent<Text>().text = settings.ballPoint.ToString();
        if (canShoot)
        {
            timer -= Time.deltaTime;
            if (timer <= -0.25f)
            {
                if (settings.ballPoint > 0)
                {
                    Fire();
                    timer = 0.1f;
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wall"))
        {
            canShoot = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("wall"))
        {
            canShoot = false;
        }
    }
    void Fire()
    {
        settings.ballPoint--;
        GameObject ball = Instantiate(ballPrefab, insPoint.transform.position, Quaternion.identity);
        Physics.IgnoreCollision(player.transform.GetChild(0).GetComponent<CapsuleCollider>(), ball.GetComponent<Collider>());

        ball.AddComponent<Rigidbody>();
        ball.GetComponent<Rigidbody>().useGravity = false;
        ball.GetComponent<Rigidbody>().AddForce(Vector3.forward * 70, ForceMode.Impulse);
    }
   
   
   
}
