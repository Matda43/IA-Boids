using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using static UnityEngine.GraphicsBuffer;
using System.Drawing;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    [Range(1f, 20f)]
    public float moveSpeed;

    const int yMin = 3;
    const int yMax = 8;

    [Range(10f, 20f)]
    public float elevation;

    PlayerController controller;
    new Camera camera;

    void Start()
    {
        controller = GetComponent<PlayerController>();
        camera = Camera.main;
        camera.transform.position = new Vector3(25, 5, 0);
        camera.transform.LookAt(new Vector3(0, -5, 0));
    }

    void Update()
    {
        camera.transform.RotateAround(new Vector3(0, -5, 0), new Vector3(0.0f, 1.0f, 0.0f), Time.deltaTime * -moveSpeed * Input.GetAxisRaw("Horizontal"));
        Vector3 offsetVertical = new Vector3(0, 0, Input.GetAxisRaw("Vertical")) * Time.deltaTime * moveSpeed;
        if ((camera.transform.position.y > yMin && camera.transform.position.y < yMax) || (camera.transform.position.y < yMin && offsetVertical.z < 0) || (camera.transform.position.y > yMax && offsetVertical.z > 0))
        {
            camera.transform.Translate(offsetVertical);
        }

        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);
    }
}
