using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

[RequireComponent(typeof(ParticleSystem))]
public class Seek : MonoBehaviour
{
    [SerializeField] Vector3 target;
    [SerializeField] float accelerationStrength;
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
        midPoint = Vector3.Lerp(transform.position, target, .5f);
    }
    private void LateUpdate()
    {
        int numParticlesAlive = myParticleSystem.GetParticles(particles);

        float distance = (target - transform.position).magnitude;

        for (int i = 0; i < numParticlesAlive; i++)
        {
            var particle = particles[i];
            //--------------------------------------------------------------------------

            float distanceTraveled = (particle.position - transform.position).magnitude;
            float distanceToTarget = (target - particle.position).magnitude;

            //float t = 1f - particle.remainingLifetime / particle.startLifetime;
            float t = distanceTraveled / distance;
            //float steped = t > 0.5f ? 1f : 0f;
            //float f0_03_1 = Mathf.Max(0, (t * 3 - 1) / 2);
            float quickLinearTransition = Mathf.Min(1, Mathf.Max(0, t * 5 - 2));
            float f1_015 = Mathf.Pow(1f - t * .6f, 2);

            Vector3 lerpedTarget = Vector3.Lerp(midPoint, target, quickLinearTransition);
            if (i == 0)
            {
                Debug.Log(lerpedTarget);
            }
            Vector3 acc = (target - particle.position).normalized * accelerationStrength;// * f1_015;

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
    public void SetTarget(Vector3 position)
    {
        target = position;
    }
    //private void OnValidate()
    //{
    //    CalculatePreTarget();
    //}
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(target, .1f);
    //    Gizmos.DrawSphere(preTarget, .1f);
    //}
}
