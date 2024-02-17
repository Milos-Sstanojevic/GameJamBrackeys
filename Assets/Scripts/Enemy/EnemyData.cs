using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public float movementSpeed;
    public float shootingDistance;
    public float shootingSpeed;
    public float bulletSpeed;

    [SerializeField] public AudioClip shootigSound;
}
