using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonGas : MonoBehaviour
{
    [SerializeField] private float goUpSpeed;
    [SerializeField] private GameManager gameMan;
    private Vector3 endPosition;

    // Start is called before the first frame update
    void Start()
    {
        endPosition.Set(transform.position.x, transform.position.y + 1, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPosition, Time.deltaTime * goUpSpeed);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            gameMan.GameOver();
        }
    }
}
