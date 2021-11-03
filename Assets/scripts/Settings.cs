using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "menu/Settings")]

public class Settings : ScriptableObject
{
    public float playerSpeed;
    public float sensitivity;
    public int ballPoint;
}
