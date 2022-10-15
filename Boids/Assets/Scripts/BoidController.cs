using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class BoidController : MonoBehaviour
{
    Vector3 velocity;
    new Rigidbody rigidbody;
    LineRenderer lineRenderer;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.isKinematic = false;

        this.AddComponent<LineRenderer>();

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void FixedUpdate()
    {
        Quaternion targetRotation = Quaternion.LookRotation(velocity);
        targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.fixedDeltaTime);
        rigidbody.MoveRotation(targetRotation);


        //rigidbody.transform.rotation = Quaternion.LookRotation(velocity);

        //Vector3 m_EulerAngleVelocity = rigidbody.position.normalized * 100;
        //Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
        //rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);

        

        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);

        
        List<Vector3> list = new List<Vector3>() { rigidbody.position, rigidbody.position + velocity.normalized * 1.5f };

        lineRenderer.positionCount = list.Count;
        lineRenderer.SetPositions(list.ToArray());
    }
}
