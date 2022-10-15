using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    int xAxisLenght;
    int yAxisLenght;
    int zAxisLenght;
    float step;
    Vector3[,,] positions;

    Vector3 center;
    Vector3 parentCenter;

    public Map(int xAxisLenght, int yAxisLenght, int zAxisLenght, float step, Vector3 parentCenter)
    {
        this.xAxisLenght = xAxisLenght;
        this.yAxisLenght = yAxisLenght;
        this.zAxisLenght = zAxisLenght;
        this.positions = new Vector3[xAxisLenght, yAxisLenght, zAxisLenght];
        this.step = step;
        this.parentCenter = parentCenter;

        this.center = getCenter(xAxisLenght, yAxisLenght, zAxisLenght, step);
        
        generateMap();
    }

    Vector3 getCenter(float xAxisLenght, float yAxisLenght, float zAxisLenght, float step)
    {
        return new Vector3(xAxisLenght / 2, yAxisLenght / 2, zAxisLenght / 2) * step;
    }

    void generateMap()
    {
        for(int i = 0; i < this.xAxisLenght; i++)
        {
            for (int j = 0; j < this.yAxisLenght; j++)
            {
                for (int k = 0; k < this.zAxisLenght; k++)
                {
                    this.positions[i, j, k] = new Vector3((float)i, (float)j, (float)k) * step - this.center + this.parentCenter;
                }
            }
        }
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.transform.position = new Vector3(this.center.x, - this.center.y - 1, this.center.z) - this.parentCenter;
        go.transform.localScale = new Vector3(this.xAxisLenght, 1, this.zAxisLenght) * step;

    }


    /*
    Vector3 calculateNewVelocity(Vector3 velocity, Vector3 position, Vector3 minPosition, Vector3 maxPosition, float weight)
    {
        Vector3 velocityNormalized = velocity.normalized;
        float rx = Random.Range(0, 1) * velocityNormalized.x;
        float ry = Random.Range(0, 1) * velocityNormalized.y;
        float rz = Random.Range(0, 1) * velocityNormalized.z;

        Vector3 new_velocity = Vector3.zero;

        if (position.x < minPosition.x)
        {
            new_velocity += new Vector3(Mathf.Pow(position.x - maxPosition.x, 2), ry, rz) * weight;
        }
        else if (position.x > maxPosition.x)
        {
            new_velocity += new Vector3(-Mathf.Pow(position.x - maxPosition.x, 2), rx, rz) * weight;
        }

        if (position.y < minPosition.y)
        {
            new_velocity += new Vector3(rx, Mathf.Pow(position.y - maxPosition.y, 2), rz) * weight;
        }
        else if (position.y > maxPosition.y)
        {
            new_velocity += new Vector3(rx, -Mathf.Pow(position.y - maxPosition.y, 2), rz) * weight;
        }

        if (position.z < minPosition.z)
        {
            new_velocity += new Vector3(rx, ry, Mathf.Pow(position.z - maxPosition.z, 2)) * weight;
        }
        else if (position.z > maxPosition.z)
        {
            new_velocity += new Vector3(rx, ry, -Mathf.Pow(position.z - maxPosition.z, 2)) * weight;
        }
        return new_velocity;
    }
    */

    public void isInMap(Boid boid, float weight)
    {
        Vector3 position = boid.transform.position;
        Vector3 velocity = boid.getCurrentVelocity();
        Vector3 maxPosition = this.center + this.parentCenter;
        Vector3 minPosition = this.parentCenter - this.center;

        Vector3 velocityNormalized = velocity.normalized;
        Vector3 new_velocity = Vector3.zero;

        float rx = Random.Range(0, 1) * velocityNormalized.x;
        float ry = Random.Range(0, 1) * velocityNormalized.y;
        float rz = Random.Range(0, 1) * velocityNormalized.z;

        if (position.x < minPosition.x)
        {
            new_velocity += new Vector3(1, ry, rz) * weight;
        }else if (position.x > maxPosition.x)
        {
            new_velocity += new Vector3(-1, rx, rz) * weight;
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
        

        //Vector3 new_velocity = calculateNewVelocity(velocity, position, minPosition, maxPosition, weight);
        boid.setCurrentVelocity(velocity + new_velocity);
    }
}
