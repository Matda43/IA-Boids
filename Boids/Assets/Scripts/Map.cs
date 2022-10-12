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
    }

    public void isInMap(Boid boid)
    {
        Vector3 v = Vector3.zero;
        Vector3 position = boid.transform.position;
        Vector3 velocity = boid.getCurrentVelocity();
        Vector3 maxPosition = new Vector3(this.xAxisLenght, this.yAxisLenght, this.zAxisLenght) * this.step - this.center + this.parentCenter;
        Vector3 minPosition = this.parentCenter - this.center;
        Debug.Log("Max " + maxPosition);
        Debug.Log("Min " + minPosition);

        if (position.x < minPosition.x && velocity.x <= 0)
        {
            Vector3 new_velocity = velocity + new Vector3(1, 0, 0) * Random.value;
            boid.setCurrentVelocity(new_velocity);
        }
        if (position.x > maxPosition.x && velocity.x >= 0)
        {
            Vector3 new_velocity = velocity + new Vector3(-1, 0, 0) * Random.value;
            boid.setCurrentVelocity(new_velocity);
        }

        if (position.y < minPosition.y && velocity.y <= 0)
        {
            Vector3 new_velocity = velocity + new Vector3(0, 1, 0) * Random.value;
            boid.setCurrentVelocity(new_velocity);
        }
        if (position.y > maxPosition.y && velocity.y >= 0)
        {
            Vector3 new_velocity = velocity + new Vector3(0, -1, 0) * Random.value;
            boid.setCurrentVelocity(new_velocity);
        }

        if (position.z < minPosition.z && velocity.z <= 0)
        {
            Vector3 new_velocity = velocity + new Vector3(0, 0, 1) * Random.value;
            boid.setCurrentVelocity(new_velocity);
        }
        if (position.z > maxPosition.z && velocity.z >= 0)
        {
            Vector3 new_velocity = velocity + new Vector3(0, 0, -1) * Random.value;
            boid.setCurrentVelocity(new_velocity);
        }
    }
}
