using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    Vector3 position;
    const float defaultRadius = 1f;
    const float maxRadius = 10;
    const float incRadius = 0.005f;
    float radius;

    int explosed = 0;


    void Start()
    {
        radius = defaultRadius;
    }

    void Update()
    {
        if (radius > 0)
        {
            if (radius < maxRadius)
            {
                radius += incRadius;
            }
            else
            {
                radius = 0;
                explosed++;
            }

            this.transform.localScale = new Vector3(radius, radius, radius);
        }

        setCenter(getCenter() + Vector3.right * Time.deltaTime);
    }

    public void decRadius()
    {
        this.radius -= incRadius;
    }

    public bool shouldBeReset()
    {
        return radius <= 0;
    }

    public void resetPlant(Vector3 new_position)
    {
        setCenter(new_position);
        this.radius = UnityEngine.Random.Range(defaultRadius, defaultRadius+4);
    }

    public Vector3 getCenter()
    {
        return position;
    }

    public void setCenter(Vector3 new_center)
    {
        this.position = new_center;
        this.transform.position = new_center;
    }

    public float getRadius()
    {
        return radius;
    }

    public void setRadius(float new_radius)
    {
        this.radius = new_radius;
    }

    public int getExplosed()
    {
        return explosed;
    }
}
