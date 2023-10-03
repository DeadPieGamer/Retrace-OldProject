using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    [SerializeField] private bool stronkBullet;
    public float speed = 5f;
    public int damage = 5;
    [SerializeField] private float timeTillCollision = 0.1f;
    public Transform playerBody;
    [SerializeField] private List<string> tagList = new List<string>();
    

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GameObject.Find("FirstPersonPlayer").transform;
        transform.LookAt(playerBody);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeTillCollision > 0f)
        {
            timeTillCollision -= Time.deltaTime;
            if (timeTillCollision <= 0f)
            {
                gameObject.tag = "GoodBullet";
            }
        }
    }

    // FixedUpdate is called once per physics update
    void FixedUpdate()
    {
        transform.Translate(0f, 0f, speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (timeTillCollision <= 0f)
        {
            if (tagList.Contains(other.gameObject.tag))
            {
                Destroy(gameObject);
            }
        }
    }
}
