using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] EnemyData configuration;

    private AudioSource audioSource;

    private Transform target;
    private EnemyState state;
    private int direction;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();

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
        var diff = (target.position - transform.position).normalized;

        var bullet = BulletPool.Instance.GetBullet();

        var trailRendered = bullet.GetComponent<TrailRenderer>();
        trailRendered.Clear();
        trailRendered.AddPosition(transform.position);

        bullet.transform.position = transform.position;
        bullet.Velocity = diff * configuration.bulletSpeed;
        bullet.ActivationTime = Time.time;

        if (audioSource != null)
        {
            audioSource.PlayOneShot(configuration.shootigSound);
        }
    }
    private void GetCloser()
    {
        var diff = target.position.x - transform.position.x;
        if (Mathf.Abs(diff) > 0.1f)
        {
            transform.Translate(Mathf.Sign(diff) * configuration.movementSpeed * Time.deltaTime * Vector3.right);
        }

        if (transform.position.x < leftBoundary)
        {
            transform.Translate(1 * configuration.movementSpeed * Time.deltaTime * Vector3.right);
        }
        else if (transform.position.x > rightBoundary)
        {
            transform.Translate(-1 * configuration.movementSpeed * Time.deltaTime * Vector3.right);
        }
    }

    private IEnumerator Alerted()
    {
        Shoot();
        state = EnemyState.GettingCloser;
        yield return new WaitForSeconds(configuration.shootingSpeed);

        var distanceToTarget = (target.position - transform.position).magnitude;
        if (distanceToTarget < configuration.shootingDistance)
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
        transform.Translate(direction * configuration.movementSpeed * Time.deltaTime * Vector3.right);

        if (transform.position.x < leftBoundary)
        {
            direction = 1;
            transform.Translate(direction * configuration.movementSpeed * Time.deltaTime * Vector3.right);
        }
        else if (transform.position.x > rightBoundary)
        {
            direction = -1;
            transform.Translate(direction * configuration.movementSpeed * Time.deltaTime * Vector3.right);
        }

        var distanceToTarget = (target.position - transform.position).magnitude;
        if (distanceToTarget < configuration.shootingDistance)
        {
            state = EnemyState.Alerted;
        }
    }
}
