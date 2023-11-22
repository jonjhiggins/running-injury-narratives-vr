using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(Rigidbody))]
public class RigidbodyParticleWind : MonoBehaviour
{
    public ParticleSystem _particleSystem;
    public ParticleSystemRenderer particleRenderer;
    public Rigidbody _rigidbody;
    private ParticleSystem.Particle[] particles;
    private bool useWind = true;

    void Start()
    {
        particles = new ParticleSystem.Particle[1];
        SetupParticleSystem();
    }

    void FixedUpdate()
    {
        if (!useWind)
        {
            return;
        }
        Debug.Log(particles[0].rotation3D);
        _particleSystem.GetParticles(particles);

        _rigidbody.velocity += particles[0].velocity;
        _rigidbody.rotation = Quaternion.Euler(particles[0].rotation3D);
        particles[0].position = _rigidbody.position;
        particles[0].velocity = Vector3.zero;

        _particleSystem.SetParticles(particles, 1);

        if (transform.position.y > 5)
        {
            useWind = false;
            _rigidbody.velocity = Vector3.zero;
        }
    }

    void SetupParticleSystem()
    {
        var main = _particleSystem.main;
        var emission = _particleSystem.emission;
        var externalForces = _particleSystem.externalForces;

        var rotationBySpeed = _particleSystem.rotationBySpeed;

        main.startLifetime = 5f;
        main.startSpeed = 0;
        main.simulationSpace = ParticleSystemSimulationSpace.World;
        main.maxParticles = 1;

        emission.rateOverTime = 1;
        rotationBySpeed.enabled = true;
        //cant set the following with code so you need to do it manually
        externalForces.enabled = true;

        //2 - Disable "Renderer"
        particleRenderer.enabled = false;

        //the below is to start the particle at the center
        _particleSystem.Emit(1);
        _particleSystem.GetParticles(particles);
        particles[0].position = Vector3.zero;
        _particleSystem.SetParticles(particles, 1);
    }
}