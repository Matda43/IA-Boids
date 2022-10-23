using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Security.Cryptography;
using UnityEngine;

public class Boid : MonoBehaviour
{

    public Vector3 velocity;
    public float maxVelocity;

    private void Start()
    {
        velocity = this.transform.forward * maxVelocity;
    }

    private void Update()
    {
        if(velocity.magnitude > maxVelocity)
        {
            velocity = velocity.normalized * maxVelocity;
        }
        if(velocity.y != 0)
        {
            velocity.y = 0;
        }
        this.transform.position += velocity * Time.deltaTime;
        this.transform.rotation = Quaternion.LookRotation(velocity);
    }

    public void setVelocity(Vector3 new_velocity)
    {
        this.velocity = new_velocity;
    }

    public void setMaxVelocity(float new_maxVelocity)
    {
        this.maxVelocity = new_maxVelocity;
    }
}