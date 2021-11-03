using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowed : MonoBehaviour
{
    [SerializeField] private Transform followed;
    private float camDistance;
    void Start()
    {
        camDistance = -15;

    }
    void Update()
    {
        transform.position = new Vector3(followed.transform.position.x, followed.transform.position.y+9, followed.transform.position.z +camDistance);
    }
}
