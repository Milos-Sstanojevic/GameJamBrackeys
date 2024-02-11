using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 Velocity;
    void Update()
    {
        transform.Translate(Velocity * Time.deltaTime);
    }
}
