using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    public Material material;
    LineRenderer lineRenderer;
    Plant plant = null;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = material;
    }

    void Update()
    {
        if (plant != null)
        {
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            List<Vector3> positions = new List<Vector3>() { this.transform.position, plant.getCenter() };
            lineRenderer.positionCount = positions.Count;
            lineRenderer.SetPositions(positions.ToArray());
        }
        else
        {
            lineRenderer.positionCount = 0;
            lineRenderer.startWidth = 0f;
            lineRenderer.endWidth = 0f;
        }
    }

    public void setPlant(Plant new_Plant)
    {
        this.plant = new_Plant;
    }
}
