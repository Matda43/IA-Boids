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

    Vector3 getCenter(int xAxisLenght, int yAxisLenght, int zAxisLenght, float step)
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


}
