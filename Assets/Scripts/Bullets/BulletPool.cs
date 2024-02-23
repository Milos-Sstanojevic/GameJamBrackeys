using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;

    private ObjectPool<Bullet> pool;

    public static BulletPool Instance;
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

    private void Start()
    {
        pool = new ObjectPool<Bullet>(
            () =>
            {
                Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
                return bullet;
            },
            bullet =>
            {
                bullet.gameObject.SetActive(true);
            },
            bullet =>
            {
                bullet.gameObject.SetActive(false);
            },
            bullet => Destroy(bullet.gameObject),
            true, 20, 40
        );
    }
    public Bullet GetBullet()
    {
        return pool.Get();
    }
    public void ReleaseBullet(Bullet bullet)
    {
        pool.Release(bullet);
    }
}
