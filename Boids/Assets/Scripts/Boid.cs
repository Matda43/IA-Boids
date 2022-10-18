using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoidController))]
public class Boid : MonoBehaviour
{
    private float maxVelocity = 0;
    private Vector3 currentVelocity = Vector3.zero;
    const int CPT_MAX = 100;
    int cpt = 0;

    BoidController controller;

    void Start()
    {
        this.controller = GetComponent<BoidController>();

    }

    void Update()
    {
        if (this.ctpIsPassed())
        {
            //controller.Move(this.currentVelocity);
            resetCpt();
        }
        else
        {
            this.incCpt();
        }
    }

    void incCpt()
    {
        this.cpt++;
    }

    bool ctpIsPassed()
    {
        return this.cpt > CPT_MAX;
    }

    void resetCpt()
    {
        this.cpt = 0;
    }

    public void setMaxVelocity(float new_maxVelocity)
    {
        this.maxVelocity = new_maxVelocity;
        this.updateCurrentVelocity(new_maxVelocity);
    }

    void updateCurrentVelocity(float new_maxVelocity)
    {
        float rx = Random.Range(1, new_maxVelocity);
        float ry = Random.Range(1, new_maxVelocity);
        float rz = Random.Range(1, new_maxVelocity);
        this.setCurrentVelocity(new Vector3(rx, ry, rz));
    }

    public void setCurrentVelocity(Vector3 new_velocity)
    {
        this.currentVelocity = new_velocity;
    }

    public Vector3 getCurrentVelocity()
    {
        return this.currentVelocity;
    }

    public float distance(Boid boid)
    {
        Vector3 distance = boid.transform.position - this.transform.position;
        return Mathf.Sqrt(Mathf.Pow(distance.x, 2) + Mathf.Pow(distance.y, 2) + Mathf.Pow(distance.z, 2));
    }

    public void moveCloser(List<Boid> boids, float param)
    {
        if (boids.Count < 1)
            return;

        Vector3 avgs = Vector3.zero;
        foreach(Boid boid in boids)
        {
            if(this.transform.position != boid.transform.position)
            {
                avgs += this.transform.position - boid.transform.position;
            }
        }
        avgs /= boids.Count;
        Vector3 new_velocity = this.getCurrentVelocity() - (avgs / param);
        this.setCurrentVelocity(new_velocity);
    }

    public void moveWith(List<Boid> boids, float param)
    {
        if (boids.Count < 1)
            return;

        Vector3 avgs = Vector3.zero;
        foreach (Boid boid in boids)
        {
            avgs += boid.getCurrentVelocity();
        }
        avgs /= boids.Count;
        Vector3 new_velocity = this.getCurrentVelocity() + (avgs / param);
        this.setCurrentVelocity(new_velocity);
    }

    public void moveAway(List<Boid> boids, float minDistance, float param)
    {
        if (boids.Count < 1)
            return;

        Vector3 distances = Vector3.zero;
        int numClose = 0;
        foreach (Boid boid in boids)
        {
            float distance = this.distance(boid);
            if(distance < minDistance)
            {
                numClose++;
                float xDiff = this.transform.position.x - boid.transform.position.x;
                float yDiff = this.transform.position.y - boid.transform.position.y;
                float zDiff = this.transform.position.z - boid.transform.position.z;

                if (xDiff >= 0)
                {
                    xDiff = Mathf.Sqrt(minDistance) - xDiff;
                }
                else
                {
                    xDiff = -Mathf.Sqrt(minDistance) - xDiff;
                }

                if (yDiff >= 0)
                {
                    yDiff = Mathf.Sqrt(minDistance) - yDiff;
                }
                else
                {
                    yDiff = -Mathf.Sqrt(minDistance) - yDiff;
                }

                if (zDiff >= 0)
                {
                    zDiff = Mathf.Sqrt(minDistance) - zDiff;
                }
                else
                {
                    zDiff = -Mathf.Sqrt(minDistance) - zDiff;
                }
                distances += new Vector3(xDiff, yDiff, zDiff);
            }
        }
        if(numClose > 0)
        {
            Vector3 new_velocity = this.getCurrentVelocity() - (distances / param);
            this.setCurrentVelocity(new_velocity);
        }
    }

    bool absGreaterThan(float a, float b)
    {
        return Mathf.Abs(a) > b;
    }

    public void move()
    {
        Vector3 currentVelocity = this.getCurrentVelocity();
        bool rx = absGreaterThan(currentVelocity.x, maxVelocity);
        bool ry = absGreaterThan(currentVelocity.y, maxVelocity);
        bool rz = absGreaterThan(currentVelocity.z, maxVelocity);
        if (rx || ry || rz)
        {
            float scaleFactor = maxVelocity / Mathf.Max(Mathf.Abs(currentVelocity.x), Mathf.Max(Mathf.Abs(currentVelocity.y), Mathf.Abs(currentVelocity.z)));
            Vector3 new_velocity = currentVelocity * scaleFactor;
            this.setCurrentVelocity(new_velocity);
        }

        controller.Move(this.currentVelocity);

    }
}
