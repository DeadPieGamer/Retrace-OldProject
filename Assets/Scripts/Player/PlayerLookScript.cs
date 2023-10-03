using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLookScript : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 500f;
    [SerializeField] private Slider ratSlide;

    private Transform playerBody;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void GetMouseSensitive()
    {
        mouseSensitivity = ratSlide.value;
    }
}
