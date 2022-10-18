using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [Range(1,100)]
    public int xAxisLenght;
    int xAxisLenghtRemember;

    [Range(1, 100)]
    public int yAxisLenght;
    int yAxisLenghtRemember;

    [Range(1, 100)]
    public int zAxisLenght;
    int zAxisLenghtRemember;

    float step = 1;

    Map map;

    /*******************************/

    public int numBoids;

    [Range(1f, 100f)]
    public float maxVelocity;
    float maxVelocityRemember;

    [Range(2f, 10f)]
    public float weight;

    [Range(1f, 100f)]
    public float moveCloser;

    [Range(1f, 100f)]
    public float moveWith;

    [Range(1f, 100f)]
    public float moveAway;

    [Range(1f, 20f)]
    public float minDistance;

    [Range(1f, 20f)]
    public float distance_to_close;

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

        this.maxVelocityRemember = maxVelocity;
        this.xAxisLenghtRemember = xAxisLenght;
        this.yAxisLenghtRemember = yAxisLenght;
        this.zAxisLenghtRemember = zAxisLenght;
    }


    void Update()
    {
        if (this.ctpIsPassed())
        {
            checkIfAxisLengthChange();

            if (maxVelocityRemember != maxVelocity)
            {
                foreach (GameObject go in this.boids)
                {
                    Boid boid = go.GetComponent<Boid>();
                    boid.setMaxVelocity(maxVelocityRemember);
                    this.maxVelocityRemember = maxVelocity;
                }
            }


            foreach(GameObject go in this.boids)
            {
                Boid boid = go.GetComponent<Boid>();
                List<Boid> closeBoids = new List<Boid>();
                foreach (GameObject otherGo in this.boids)
                {
                    if(otherGo != go)
                    {
                        Boid otherBoid = otherGo.GetComponent<Boid>();
                        float distance = boid.distance(otherBoid);
                        if(distance < distance_to_close)
                        {
                            closeBoids.Add(otherBoid);
                        }
                    }
                }

                boid.moveCloser(closeBoids, moveCloser);
                boid.moveWith(closeBoids, moveWith);
                boid.moveAway(closeBoids, minDistance, moveAway);
                map.isInMap(boid, weight);
                boid.move();
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

    void checkIfAxisLengthChange()
    {
        if (xAxisLenghtRemember != xAxisLenght)
        {
            this.map.setXAxisLength(xAxisLenght);
            xAxisLenghtRemember = xAxisLenght;
        }
        if (yAxisLenghtRemember != yAxisLenght)
        {
            this.map.setYAxisLength(yAxisLenght);
            yAxisLenghtRemember = yAxisLenght;
        }
        if (zAxisLenghtRemember != zAxisLenght)
        {
            this.map.setZAxisLength(zAxisLenght);
            zAxisLenghtRemember = zAxisLenght;
        }
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
