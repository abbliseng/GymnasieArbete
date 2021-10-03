using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    Vector3 velocity;

    private void Update() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        speed += Input.mouseScrollDelta.y * 10f;
        if (speed < 12f) speed = 12f;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButton("Jump")) {
            velocity.y = 1f;
        } else if(Input.GetKey(KeyCode.LeftShift)) {
            velocity.y = -1f;
        } else {
            velocity.y = 0f;
        }


        controller.Move(velocity * speed * Time.deltaTime);
    }
}
