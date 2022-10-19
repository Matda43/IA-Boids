using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    float radius;
    Vector3 center;

    public Map(float radius, Vector3 parentCenter)
    {
        this.radius = radius;
        this.center = parentCenter;
        generateMap();
    }

    public void setCenter(Vector3 parentCenter)
    {
        this.center = parentCenter;
    }

    public void setRadius(float new_radius)
    {
        this.radius = new_radius;
    }

    void generateMap()
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.transform.position = this.center;
        go.transform.localScale = Vector3.one;

    }

    bool PointInSphere(Vector3 point, float radius)
    {
        Vector3 difference = center - point;
        float distance = Mathf.Pow(difference.x,2) + Mathf.Pow(difference.y,2) + Mathf.Pow(difference.z,2);
        return distance < Mathf.Pow(radius,2);
    }

    public void isInMap(Boid boid, float weight)
    {
        Vector3 position = boid.transform.position;
        Vector3 velocity = boid.getCurrentVelocity();
        Vector3 velocityNormalized = velocity.normalized;
        Vector3 new_velocity = Vector3.zero;

        float rx = Random.Range(0, 1) * velocityNormalized.x;
        float ry = Random.Range(0, 1) * velocityNormalized.y;
        float rz = Random.Range(0, 1) * velocityNormalized.z;

        /*
        if (position.x < minPosition.x)
        {
            new_velocity += new Vector3(1, ry, rz) * weight;
        }else if (position.x > maxPosition.x)
        {
            new_velocity += new Vector3(-1, ry, rz) * weight;
        }

        if (position.y < minPosition.y)
        {
            new_velocity += new Vector3(rx, 1, rz) * weight;
        }else if (position.y > maxPosition.y)
        {
            new_velocity += new Vector3(rx, -1, rz) * weight;
        }

        if (position.z < minPosition.z)
        {
            new_velocity += new Vector3(rx, ry, 1) * weight;
        }else if (position.z > maxPosition.z)
        {
            new_velocity += new Vector3(rx, ry, -1) * weight;
        }
        */

        bool res = PointInSphere(position, radius);
        if (!res)
        {
            if(position.x < center.x)
            {
                new_velocity += new Vector3(1, ry, rz) * weight;
            }
            else if (position.x > center.x)
            {
                new_velocity += new Vector3(-1, ry, rz) * weight;
            }

            if (position.y < center.y)
            {
                new_velocity += new Vector3(rx, 1, rz) * weight;
            }
            else if (position.y > center.y)
            {
                new_velocity += new Vector3(rx, -1, rz) * weight;
            }

            if (position.z < center.z)
            {
                new_velocity += new Vector3(rx, ry, 1) * weight;
            }
            else if (position.z > center.z)
            {
                new_velocity += new Vector3(rx, ry, -1) * weight;
            }
        }


        res = PointInSphere(position, radius - 5);
        if (res)
        {
            if (position.x > center.x)
            {
                new_velocity += new Vector3(1, ry, rz) * weight;
            }
            else if (position.x < center.x)
            {
                new_velocity += new Vector3(-1, ry, rz) * weight;
            }

            if (position.y > center.y)
            {
                new_velocity += new Vector3(rx, 1, rz) * weight;
            }
            else if (position.y < center.y)
            {
                new_velocity += new Vector3(rx, -1, rz) * weight;
            }

            if (position.z > center.z)
            {
                new_velocity += new Vector3(rx, ry, 1) * weight;
            }
            else if (position.z < center.z)
            {
                new_velocity += new Vector3(rx, ry, -1) * weight;
            }
        }

        boid.setCurrentVelocity(velocity + new_velocity);
    }
}
