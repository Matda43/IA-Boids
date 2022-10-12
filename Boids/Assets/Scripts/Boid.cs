using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            controller.Move(currentVelocity);
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

    void setCurrentVelocity(Vector3 new_velocity)
    {
        this.currentVelocity = new_velocity;
    }
}
