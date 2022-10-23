using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Boid))]
public class Cohesion : MonoBehaviour
{

    private Boid boid;

    public float radius;

    void Start()
    {
        boid = GetComponent<Boid>();
    }

    void Update()
    {
        Boid[] boids = FindObjectsOfType<Boid>();
        
        Vector3 avg = Vector3.zero;
        int found = 0;

        List<Vector3> list = new List<Vector3>();

        foreach (Boid b in boids)
        {
            if (b != boid)
            {
                var diff = b.transform.position - this.transform.position;
                if (diff.magnitude < radius)
                {
                    list.Add(b.transform.position);
                    avg.x += diff.x;
                    avg.z += diff.z;
                    found += 1;
                }
            }
        }

        if (found > 0)
        {
            avg = avg / found;
            Vector3 l = Vector3.Lerp(Vector3.zero, avg, avg.magnitude / radius);
            l.y = 0;
            boid.velocity += l;
        }
    }

    public void setRadius(float new_radius)
    {
        this.radius = new_radius;
    }
}
