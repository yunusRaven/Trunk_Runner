using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Wall : MonoBehaviour
{
    public List<GameObject> walls;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ball"))
        {
            Destroy(other.gameObject);
            GameObject destroyingWall = walls[0];
            transform.DOMove(transform.position + Vector3.up * 1.1f, 0.05f);
            walls.Remove(walls[0]);
            transform.DOMove(transform.position - Vector3.up * 2f, 0.3f);

            Destroy(destroyingWall);
            if (walls.Count == 0)
            {
                for (int i = 0; i < walls.Count; i++)
                {
                    Destroy(walls[i]);
                }
            }

        }
    }
}
