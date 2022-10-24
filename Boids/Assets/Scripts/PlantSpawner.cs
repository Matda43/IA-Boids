using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlantSpawner : MonoBehaviour
{
    public GameObject prefab;

    const int CPTMAX = 10;

    List<GameObject> plants;
    List<Vector3> spawnablePositions;

    void Awake()
    {
        plants = new List<GameObject>();
        spawnablePositions = new List<Vector3>();
    }

    int cpt = 0;

    void Update()
    {
        foreach(GameObject go in plants)
        {
            Plant p = go.GetComponent<Plant>();
            if (p.shouldBeReset())
            {
                if (cpt >= CPTMAX)
                {
                    if (spawnablePositions.Count > 0)
                    {
                        int res = getRandomNumber(0, spawnablePositions.Count);
                        p.resetPlant(spawnablePositions[res]);
                    }
                    cpt = 0;
                }
                else
                {
                    cpt++;
                }
            }
        }
    }

    public void setSpawns(List<Vector3> new_spawnablePositions)
    {
        this.spawnablePositions = new_spawnablePositions;
    }

    public void instanciateAPlant()
    {
        if(spawnablePositions.Count > 0)
        {
            int res = getRandomNumber(0, spawnablePositions.Count);
            GameObject go = Instantiate(prefab, spawnablePositions[res], Quaternion.identity);
            go.transform.parent = this.transform;
            Plant p = go.GetComponent<Plant>();
            p.setCenter(spawnablePositions[res]);
            plants.Add(go);
        }
    }

    int getRandomNumber(int minimum, int maximum)
    {
        return Random.Range(minimum, maximum - 1);
    }

    public Plant plantSelected()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 50))
        {
            GameObject g = hit.transform.gameObject;
            Vector3 position = g.transform.position;

            foreach (GameObject go in plants)
            {
                Plant plant = go.GetComponent<Plant>();
                bool res = PointInSphere(plant.getCenter(), position, 3);
                if (res)
                {
                    return plant;
                }
            }
        }
        return null;

    }

    bool PointInSphere(Vector3 center, Vector3 point, float radius)
    {
        Vector3 difference = center - point;
        float distance = Mathf.Pow(difference.x, 2) + Mathf.Pow(difference.z, 2);
        return distance < Mathf.Pow(radius, 2);
    }

    public void movePlant(Vector3 center, float radius)
    {
        foreach(GameObject go in plants)
        {
            Plant p = go.GetComponent<Plant>();
            bool res = PointInSphere(center, p.getCenter(), radius);
            if (!res)
            {
                Vector3 currentPosition = p.getCenter();
                p.setCenter(new Vector3(-currentPosition.x + 1, currentPosition.y, currentPosition.z));
            }
        }
    }
}
