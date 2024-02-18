using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineManager : MonoBehaviour
{
    [SerializeField] CinemachineConfiner2D confiner;
    [SerializeField] List<PolygonCollider2D> colliders;

    public static CinemachineManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void OnTeleportFinished(Vector3 newPosition)
    {
        var newCollider = colliders.Find(c =>
        {
            c.enabled = true;
            var b = c.OverlapPoint(newPosition);
            c.enabled = false;

            return b;
        });

        if (newCollider != null)
        {
            confiner.m_BoundingShape2D = newCollider;
        }
    }
}
