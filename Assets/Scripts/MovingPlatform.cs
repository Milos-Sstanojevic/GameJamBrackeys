using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float movingSpeed;
    [SerializeField] float range;

    private Vector3 homePosition;
    void Start()
    {
        homePosition = transform.position;
    }
    void Update()
    {
        transform.position = Mathf.Sin(Time.time * movingSpeed) * range * Vector3.up + homePosition;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
