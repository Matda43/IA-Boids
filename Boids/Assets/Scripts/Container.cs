using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[RequireComponent(typeof(Boid))]
public class Container : MonoBehaviour
{
    private Boid boid;

    public float radius;

    public float boundaryForce;

    Vector3 center = Vector3.zero;

    void Start()
    {
        boid = GetComponent<Boid>();    
    }

    void Update()
    {
        bool res = PointInSphere(center, boid.transform.position, radius);
        if (!res)
        {
            Vector3 offset = this.transform.position.normalized + (center - this.transform.position) * boundaryForce * Time.deltaTime;
            offset.y = 0;
            boid.velocity += offset; 
        }
    }

    bool PointInSphere(Vector3 center, Vector3 point, float radius)
    {
        Vector3 difference = center - point;
        float distance = Mathf.Pow(difference.x, 2) + Mathf.Pow(difference.y, 2) + Mathf.Pow(difference.z, 2);
        return distance < Mathf.Pow(radius, 2);
    }

    public void setRadius(float new_radius)
    {
        this.radius = new_radius;
    }

    public void setBoundaryForce(float new_boundaryForce)
    {
        this.boundaryForce = new_boundaryForce;
    }

    public void setCenter(Vector3 new_center)
    {
        this.center = new_center;
    }
}
