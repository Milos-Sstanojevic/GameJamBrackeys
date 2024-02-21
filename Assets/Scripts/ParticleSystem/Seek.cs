using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

[RequireComponent(typeof(ParticleSystem))]
public class Seek : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float accelerationStrength;
    [SerializeField] float lookAheadDistance;
    [SerializeField] float killingDistance;

    private Particle[] particles;
    private ParticleSystem myParticleSystem;

    private Vector3 midPoint;

    private void Awake()
    {
        myParticleSystem = GetComponent<ParticleSystem>();
    }
    private void Start()
    {
        particles = new Particle[myParticleSystem.main.maxParticles];
        midPoint = Vector3.Lerp(transform.position, target.position, .5f);
    }
    private void LateUpdate()
    {
        int numParticlesAlive = myParticleSystem.GetParticles(particles);

        float pathLength = (target.position - transform.position).magnitude;
        Vector3 pathDirection = (target.position - transform.position).normalized;

        Vector3 lookAhead = pathDirection * lookAheadDistance;

        for (int i = 0; i < numParticlesAlive; i++)
        {
            var particle = particles[i];
            //--------------------------------------------------------------------------
            float distanceToTarget = (target.position - particle.position).magnitude;
            float distanceTraveled = (transform.position - particle.position).magnitude;

            float t = distanceTraveled / pathLength;

            Vector3 projectionOnPath = Vector3.Project(particle.position - transform.position, pathDirection);

            Vector3 currentTarget = transform.position + projectionOnPath + lookAhead; // * (1f - t);

            Vector3 toCurrentTarget = (currentTarget - particle.position).normalized;

            Vector3 acc = toCurrentTarget * accelerationStrength;


            particle.velocity += acc;
             
            if (distanceToTarget < killingDistance)
            {
                particle.remainingLifetime = 0f;
            }
            //--------------------------------------------------------------------------
            particles[i] = particle;
        }

        myParticleSystem.SetParticles(particles, numParticlesAlive);
    }
}
