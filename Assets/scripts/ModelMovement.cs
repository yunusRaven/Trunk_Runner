using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ModelMovement : MonoBehaviour
{
    private Vector3 mousePos;
    private Vector3 mouseStartPos;
    private Vector3 movementDiff;
    [SerializeField] private bool isPlaying;
    [SerializeField] private Settings settings;
    [SerializeField] private Camera orthoCamera;
    [SerializeField] private Movement movement;
    private float xStartPos;
    Rigidbody rb;
    public GameObject TTPText;

    private Vector3 abc = Vector3.one + Vector3.left - Vector3.forward;
    void Start()
    {
        movement.onWin += OnWin;
        TTPText.SetActive(true);
        movement.TrunkAction += OnFail;
        xStartPos = transform.position.x;
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        mouseStartPos = Vector3.Lerp(mouseStartPos, mousePos, 0.1f);

        if (Input.GetMouseButtonDown(0))
        {
            MouseDown(Input.mousePosition);
        }

        else if (Input.GetMouseButton(0))
        {
            MouseHold(Input.mousePosition);
        }

        else if (Input.GetMouseButtonUp(0))
        {
            MouseUp();
        }

        if (isPlaying)
        {
            move();
        }
    }
    private void MouseDown(Vector3 inputPos)
    {
        mousePos = orthoCamera.ScreenToWorldPoint(inputPos);
        mouseStartPos = mousePos;
    }
    private void MouseHold(Vector3 inputPos)
    {
        mousePos = orthoCamera.ScreenToWorldPoint(inputPos);
        movementDiff = mousePos - mouseStartPos;
        movementDiff *= settings.sensitivity;
    }
    private void MouseUp()
    {
        movementDiff = Vector3.zero;
    }
    private void move()
    {
        float xPosition = Mathf.Clamp(transform.position.x, xStartPos - 4, xStartPos + 4);
        transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);
        rb.velocity = new Vector3(movementDiff.x, transform.position.y, settings.playerSpeed);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(transform.localRotation.x,(movementDiff.x * 3),transform.localRotation.z), 0.4f);
    }
    void OnWin()
    {
        isPlaying = false;
        gameObject.transform.DOLocalRotate(new Vector3(0,-180,0), 1).SetRelative(true);
        gameObject.GetComponentInChildren<Animator>().SetBool("Dancing",true);
        rb.velocity = Vector3.zero;
    }

    void OnFail()
    {
        isPlaying = false;
        rb.velocity = Vector3.zero;
    }
    public void TTPButton()
    {
        isPlaying = true;
        TTPText.SetActive(false);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
