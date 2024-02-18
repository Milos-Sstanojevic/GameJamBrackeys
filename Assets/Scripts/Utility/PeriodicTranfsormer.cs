using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicTranfsormer : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector3 startingScale;
    [SerializeField] Vector3 scaleOffset;
    void Update()
    {
        transform.localScale = startingScale + Mathf.Sin(Time.time * speed) * scaleOffset;
    }
}
