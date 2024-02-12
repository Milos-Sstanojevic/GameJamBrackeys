using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    [SerializeField] GameObject bulletPrefab;

    private ObjectPool<Bullet> pool;
    private void Awake()
    {
        CreateSingleton();
    }

    void CreateSingleton()
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
            () => Instantiate(bulletPrefab).GetComponent<Bullet>(),
            bullet => bullet.gameObject.SetActive(true),
            bullet => bullet.gameObject.SetActive(false),
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
