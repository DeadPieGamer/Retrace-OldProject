using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 move;

    [SerializeField] private float speed = 8f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;
    }

    // FixedUpdate is called once per physics update
    void FixedUpdate()
    {
        controller.Move(move * speed * Time.fixedDeltaTime);
    }
}
