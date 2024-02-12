using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Alerted,
    GettingCloser
}
public class EnemyController : MonoBehaviour
{
    [SerializeField] float leftBoundary;
    [SerializeField] float rightBoundary;
    [SerializeField] float movementSpeed;
    [SerializeField] float shootingDistance;
    [SerializeField] float shootingSpeed;
    [SerializeField] float bulletSpeed;

    [SerializeField] GameObject bulletPrefab;

    private Transform target;
    private EnemyState state;
    private int direction;

    void Start()
    {
        state = EnemyState.Idle;
        direction = Random.Range(0f, 1f) > .5f ? 1 : -1;
        target = GameObject.FindAnyObjectByType<PlayerController>().transform;
    }

    void Update()
    {
        if (state == EnemyState.Idle)
        {
            Idle();
        }
        else if (state == EnemyState.Alerted)
        {
            StartCoroutine(Alerted());
        }
        else if (state == EnemyState.GettingCloser)
        {
            GetCloser();
        }
    }
    private void Shoot()
    {
        var bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
        bullet.transform.position = transform.position;
        var diff = target.position - transform.position;
        diff.Normalize();
        bullet.Velocity = diff * bulletSpeed;
    }
    private void GetCloser()
    {
        var diff = target.position.x - transform.position.x;
        if (Mathf.Abs(diff) > 0.1f)
        {
            transform.Translate(Mathf.Sign(diff) * movementSpeed * Time.deltaTime * Vector3.right);
        }

        if (transform.position.x < leftBoundary)
        {
            transform.Translate(1 * movementSpeed * Time.deltaTime * Vector3.right);
        }
        else if (transform.position.x > rightBoundary)
        {
            transform.Translate(-1 * movementSpeed * Time.deltaTime * Vector3.right);
        }
    }

    private IEnumerator Alerted()
    {
        Shoot();
        state = EnemyState.GettingCloser;
        yield return new WaitForSeconds(shootingSpeed);

        var distanceToTarget = (target.position - transform.position).magnitude;
        if (distanceToTarget < shootingDistance)
        {
            state = EnemyState.Alerted;
        }
        else
        {
            state = EnemyState.Idle;
        }
    }
    private void Idle()
    {
        transform.Translate(direction * movementSpeed * Time.deltaTime * Vector3.right);

        if (transform.position.x < leftBoundary)
        {
            direction = 1;
            transform.Translate(direction * movementSpeed * Time.deltaTime * Vector3.right);
        }
        else if (transform.position.x > rightBoundary)
        {
            direction = -1;
            transform.Translate(direction * movementSpeed * Time.deltaTime * Vector3.right);
        }

        var distanceToTarget = (target.position - transform.position).magnitude;
        if (distanceToTarget < shootingDistance)
        {
            state = EnemyState.Alerted;
        }
    }
}
