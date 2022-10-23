using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(BoidSpawner))]
[RequireComponent(typeof(PlantSpawner))]
public class Map : MonoBehaviour
{
    public int radius;
    public float sizeCube;
    public Material sideMaterial;
    public Material grassMaterial;
    public Material grassLightMaterial;
    public GameObject prefabCenter;


    Vector3 center = Vector3.zero;

    BoidSpawner boidSpawner;
    PlantSpawner plantSpawner;
    const int nbPlant = 10;

    void Start()
    {
        boidSpawner = GetComponent<BoidSpawner>();
        plantSpawner = GetComponent<PlantSpawner>();
        List<Vector3> spawnablePlant = generateMap();
        plantSpawner.setSpawns(spawnablePlant);
        for (int i = 0; i < nbPlant; i++)
        {
            plantSpawner.instanciateAPlant();
        }
    }

    void Update()
    {
        chekButtonClickMouse();
        plantSpawner.movePlant(center, radius - 1f);
    }

    void chekButtonClickMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Plant res = plantSpawner.plantSelected();

            if (res != null)
            {
                boidSpawner.setIsFollowing(true);
                boidSpawner.setPlantFollow(res);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            boidSpawner.setIsFollowing(false);
        }
    }

    List<Vector3> generateMap()
    {
        List<Vector3> spawnablePlant = new List<Vector3>();
        int diameter = radius * 2;
        Vector3 maxPosition = center - new Vector3(1, 0, 1) * radius;
        for (int i = 0; i < diameter; i++)
        {
            for (int j = 0; j < diameter; j++)
            {
                Vector3 position = new Vector3(i, -5, j) * sizeCube + maxPosition;
                bool res = PointInSphere(center, position, radius);
                if (res)
                {
                    GameObject go;
                    
                    res = PointInSphere(center, position, radius - 1);
                    if (res)
                    {
                        int r1 = Random.Range(0, 10);
                        int r2 = Random.Range(0, 10);
                        int r3 = Random.Range(0, 10);
                        if ((r1 <= r2 && r2 <= r3) || (r3 <= r1 && r3 <= r2))
                        {
                            float rotation = Random.Range(0, 4);
                            rotation *= 90;
                            go = Instantiate(prefabCenter, position, Quaternion.identity);
                            go.transform.parent = transform;
                            go.transform.localRotation = Quaternion.AngleAxis(rotation, new Vector3(0, 1, 0));
                            if (rotation == 0)
                            {
                                go.transform.position = position + new Vector3(0.5f, 0, 0.5f);
                            }else if(rotation == 90)
                            {
                                go.transform.position = position + new Vector3(0.5f, 0, -0.5f);
                            }
                            else if (rotation == 180)
                            {
                                go.transform.position = position + new Vector3(-0.5f, 0, -0.5f);
                            }
                            else
                            {
                                go.transform.position = position + new Vector3(-0.5f, 0, 0.5f);
                            }
                        }
                        else if (r2 < r1 && r2 < r3)
                        {
                            go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            go.transform.parent = transform;
                            go.transform.position = position;
                            go.GetComponent<MeshRenderer>().material = grassMaterial;
                        }
                        else
                        {
                            go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            go.transform.parent = transform;
                            go.transform.position = position;
                            go.GetComponent<MeshRenderer>().material = grassLightMaterial;
                        }


                        res = PointInSphere(center, position, radius - 5);
                        if (res)
                        {
                            if (position.x % 4 == 0 && position.z % 4 == 0)
                            {
                                spawnablePlant.Add(position + new Vector3(0, 3, 0));
                            }
                        }
                    }
                    else
                    {
                        go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        go.transform.parent = transform;
                        go.transform.position = position;
                        go.GetComponent<MeshRenderer>().material = sideMaterial;
                    }
                }
            }
        }
        return spawnablePlant;
    }

    bool PointInSphere(Vector3 center, Vector3 point, float radius)
    {
        Vector3 difference = center - point;
        float distance = Mathf.Pow(difference.x, 2) + Mathf.Pow(difference.y, 2) + Mathf.Pow(difference.z, 2);
        return distance < Mathf.Pow(radius, 2);
    }
}
