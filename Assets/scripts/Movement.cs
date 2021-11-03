using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    private Vector3 mousePos;
    private Vector3 mouseStartPos;
    private Vector3 movementDiff;
    [SerializeField] private Settings settings;
    [SerializeField] private bool isPlaying;
    [SerializeField] private Camera orthoCamera;
    float xStartPos;
    Rigidbody rb;
    public GameObject vacuum;
    public GameObject NextLevelText;
    Vector3 localScale;
    public GameObject TTPText;

    public Action TrunkAction;
    public Action onWin;

    public Animator Animator;
    public List<Transform> body;
    void Start()
    {
        NextLevelText.SetActive(false);
        TTPText.SetActive(true);
        TrunkAction += OnFail;
        onWin += OnWin;
        xStartPos = transform.position.x;
        rb = GetComponent<Rigidbody>();
        localScale = body[0].localScale;
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

        if (isPlaying==true)
        {
            move();
            BreadCrump();
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
        rb.velocity = new Vector3(movementDiff.x, rb.velocity.y, settings.playerSpeed);
        body[0].transform.localRotation = Quaternion.Lerp(body[0].transform.localRotation, Quaternion.Euler(0, 0f, 90f - (movementDiff.x * 3)), 0.4f);
    }
    void BreadCrump()
    {
        for (int i = 1; i < body.Count; i++)
        {
            body[i].transform.position = Vector3.Lerp(body[i].transform.position, body[i - 1].transform.position, 0.2f);
            body[i].transform.DOLocalRotate(body[i - 1].transform.localRotation.eulerAngles, 1);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ball"))
        {
            Swelling();
            other.transform.tag = "Untagged";
            other.transform.DOMove(vacuum.transform.position, 0.5f);
            settings.ballPoint++;
            Destroy(other.gameObject, 0.05f);
        }
        if (other.CompareTag("Brick"))
        {
            TrunkAction?.Invoke();
            Animator.SetBool("isFail", true);
        }
        if (other.CompareTag("FinishLine"))
        {
            onWin?.Invoke();
           
        }
    }
    void OnWin()
    {
        isPlaying = false;
        rb.velocity = Vector3.zero;
        NextLevelText.SetActive(true);
            print("sds");
    }
    void OnFail()
    {
        isPlaying = false;
        rb.velocity = Vector3.zero;
        Debug.Log("cba");
    }

    void Swelling()
    {
        StartCoroutine(SetOffset());
    }

    IEnumerator SetOffset()
    {
        for (int i = 1; i < body.Count; i++)
        {
            yield return new WaitForSeconds(0.01f);
            body[i].transform.localScale = Vector3.one * 4f;
            yield return new WaitForSeconds(0.03f);
            body[i].transform.localScale = Vector3.one * 2.54f;
        }
    }
    public void TTPButton()
    {
        isPlaying = true;
        TTPText.SetActive(false);
        Animator.SetBool("Running", true);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void NextLevelButton()
    {
        LevelManager.Instance.LoadNextLevel();
    }
}
