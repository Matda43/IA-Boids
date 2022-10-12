using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [Range(1,20)]
    public int xAxisLenght;

    [Range(1, 20)]
    public int yAxisLenght;

    [Range(1, 20)]
    public int zAxisLenght;

    float step = 1;

    Map map;

    /*******************************/

    public int numBoids;

    [Range(1f, 10f)]
    public float maxVelocity;

    public GameObject prefab;

    GameObject[] boids;


    void Start()
    {
        this.boids = new GameObject[numBoids];
        this.map = new Map(xAxisLenght, yAxisLenght, zAxisLenght, step, transform.position);
        generateBoids(numBoids, prefab, maxVelocity);
    }


    void Update()
    {
        
    }


    void generateBoids(int numBoids, GameObject prefab, float maxVelocity)
    {
        for(int i = 0; i < numBoids; i++)
        {
            float rx = Random.Range(0, this.xAxisLenght * step);
            float ry = Random.Range(0, this.yAxisLenght * step);
            float rz = Random.Range(0, this.zAxisLenght * step);
            Vector3 new_position = new Vector3(rx, ry, rz);
            Debug.Log(new_position);
            GameObject go = Instantiate(prefab, new_position, Quaternion.identity);
            Boid b = go.GetComponent<Boid>();
            b.setMaxVelocity(maxVelocity);
            boids[i] = go;
        }
    }
}
