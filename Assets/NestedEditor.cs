using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestedEditor : MonoBehaviour
{
    [SerializeField] private List<Transform> bones;

    [Button]
    public void MakeNested()
    {
        for (int i = 0; i < bones.Count; i++)
        {
            if (i + 1 != bones.Count)
                bones[i + 1].parent = bones[i];
        }
    }

    [Button]
    public void UnParent()
    {
        for (int i = 0; i < bones.Count; i++)
            bones[i].parent = transform;
    }
}
