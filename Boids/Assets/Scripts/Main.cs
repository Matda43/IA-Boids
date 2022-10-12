using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [Range(1,100)]
    public int xAxisLenght;

    [Range(1, 100)]
    public int yAxisLenght;

    [Range(1, 100)]
    public int zAxisLenght;

    float step = 1;

    Map map;

    /*******************************/

    public int numBoids;

    [Range(1f, 20f)]
    public float maxVelocity;

    public GameObject prefab;

    GameObject[] boids;


    /******************************/

    const int CPT_MAX = 10;
    int cpt = 0;

    void Start()
    {
        this.boids = new GameObject[numBoids];
        this.map = new Map(xAxisLenght, yAxisLenght, zAxisLenght, step, transform.position);
        generateBoids(numBoids, prefab, maxVelocity);
    }


    void Update()
    {
        if (this.ctpIsPassed())
        {
            foreach(GameObject go in this.boids)
            {
                Boid b = go.GetComponent<Boid>();
                map.isInMap(b);
                b.move();
            }
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

    void generateBoids(int numBoids, GameObject prefab, float maxVelocity)
    {
        for(int i = 0; i < numBoids; i++)
        {
            float rx = Random.Range(0, this.xAxisLenght * step);
            float ry = Random.Range(0, this.yAxisLenght * step);
            float rz = Random.Range(0, this.zAxisLenght * step);
            Vector3 new_position = new Vector3(rx, ry, rz);
            GameObject go = Instantiate(prefab, new_position, Quaternion.identity);
            go.transform.parent = transform;
            Boid b = go.GetComponent<Boid>();
            b.setMaxVelocity(maxVelocity);
            boids[i] = go;
        }
    }
}
