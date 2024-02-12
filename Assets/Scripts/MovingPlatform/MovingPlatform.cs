using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float timeOffset;
    [SerializeField] float movingSpeed;
    [SerializeField] Vector3 range;

    private Vector3 homePosition;
    void Start()
    {
        homePosition = transform.position;
    }
    void Update()
    {
        float t = Mathf.Sin(timeOffset + Time.time * movingSpeed) + 1f / 2f;
        transform.position = homePosition + t * range;
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
