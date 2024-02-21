using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

[RequireComponent(typeof(ParticleSystem))]
public class Seek : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float speed;

    private Particle[] particles;
    private ParticleSystem myParticleSystem;


    private void Awake()
    {
        myParticleSystem = GetComponent<ParticleSystem>();
    }
    private void Start()
    {
        particles = new Particle[myParticleSystem.main.maxParticles];

        var collider = target.GetComponent<Collider2D>();
        myParticleSystem.trigger.AddCollider(collider);
    }
    private void LateUpdate()
    {
        int numParticlesAlive = myParticleSystem.GetParticles(particles);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            var particle = particles[i];

            Vector3 toTarget = (target.transform.position - particle.position).normalized;
            particle.velocity += toTarget * speed;

            particles[i] = particle;
        }

        myParticleSystem.SetParticles(particles, numParticlesAlive);
    }
}
