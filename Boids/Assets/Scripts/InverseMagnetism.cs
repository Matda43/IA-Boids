using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Boid))]
public class InverseMagnetism : MonoBehaviour
{
    private Boid boid;

    public float radius;

    public float repulsionForce;

    void Start()
    {
        boid = GetComponent<Boid>();
    }

    void Update()
    {
        Boid[] boids = FindObjectsOfType<Boid>();

        Vector3 avg = Vector3.zero;
        int found = 0;

        foreach (Boid b in boids)
        {
            if (b != boid)
            {
                var diff = b.transform.position - this.transform.position;
                if (diff.magnitude < radius)
                {
                    avg += diff;
                    found += 1;
                }
            }
        }

        if (found > 0)
        {
            avg = avg / found;
            boid.velocity -= Vector3.Lerp(Vector3.zero, avg, avg.magnitude / radius) * repulsionForce;
        }
    }

    public void setRadius(float new_radius)
    {
        this.radius = new_radius;
    }

    public void setRepulsionForce(float new_repulsionForce)
    {
        this.repulsionForce = new_repulsionForce;
    }

}
