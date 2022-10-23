using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    private Boid boid;

    public float radius;

    public float followForce;

    Vector3 center = Vector3.zero;
    
    bool follow = false;

    void Start()
    {
        boid = GetComponent<Boid>();
    }

    void Update()
    {
        if (follow)
        {
            bool res = PointInSphere(center, boid.transform.position, radius);
            if (!res)
            {
                Vector3 offset = this.transform.position.normalized + (center - this.transform.position) * followForce * Time.deltaTime;
                offset.y = 0;
                boid.velocity += offset;
            }
            else
            {
                
                res = PointInSphere(center, boid.transform.position, radius - 3);
                if (res)
                {
                    Vector3 offset = this.transform.position.normalized + (center - this.transform.position) * followForce * Time.deltaTime;
                    offset.y = 0;
                    boid.velocity -= offset;
                }
                
            }
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

    public void setFollowForce(float new_followForce)
    {
        this.followForce = new_followForce;
    }

    public void setCenter(Vector3 new_center)
    {
        this.center = new_center;
    }

    public void setIsFollowing(bool boolean)
    {
        this.follow = boolean;
    }

    public bool PointInSphere(Vector3 point)
    {
        Vector3 difference = center - point;
        float distance = Mathf.Pow(difference.x, 2) + Mathf.Pow(difference.y, 2) + Mathf.Pow(difference.z, 2);
        return distance < Mathf.Pow(3, 2);
    }
}
