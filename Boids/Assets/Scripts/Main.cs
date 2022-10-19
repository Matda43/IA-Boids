using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [Range(10f,100f)]
    public float radius;
    float radiusRemember;

    Map map;

    /*******************************/

    public int numBoids;

    [Range(10f, 100f)]
    public float maxVelocity;

    float minVelocity = 10;

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
        this.map = new Map(radius, transform.position);
        generateBoids(numBoids, prefab, maxVelocity, minVelocity);

        this.maxVelocityRemember = maxVelocity;
        this.radiusRemember = radius;
    }


    void Update()
    {
        if (this.ctpIsPassed())
        {
            if (radiusRemember != radius)
            {
                this.map.setRadius(radius);
            }

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

    void generateBoids(int numBoids, GameObject prefab, float maxVelocity, float minVelocity)
    {
        for(int i = 0; i < numBoids; i++)
        {
            Vector3 randomPosition = Random.onUnitSphere * radius;
            GameObject go = Instantiate(prefab, randomPosition, Quaternion.identity);
            go.transform.parent = transform;
            Boid b = go.GetComponent<Boid>();
            b.setMaxVelocity(maxVelocity);
            boids[i] = go;
        }
    }
}
