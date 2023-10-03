using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpened = false;
    
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private float distanceToMove = 1f;
    private Vector3 startLocation;
    [SerializeField] private Vector3 endLocation;
    [SerializeField] private int directionMove = 1;
    public int myNumber = 0;

    private int forFindingButt;

    // Loads direction based on rotation
    public void FindOutDirection(string compass)
    {
        startLocation = transform.position;
        if (compass == "North")
        {
            endLocation.Set(startLocation.x, startLocation.y, startLocation.z + distanceToMove);
            directionMove = 3;
        }
        if (compass == "East")
        {
            endLocation.Set(startLocation.x + distanceToMove, startLocation.y, startLocation.z);
            directionMove = 1;
        }
        if (compass == "South")
        {
            endLocation.Set(startLocation.x, startLocation.y, startLocation.z - distanceToMove);
            directionMove = 4;
        }
        if (compass == "West")
        {
            endLocation.Set(startLocation.x - distanceToMove, startLocation.y, startLocation.z);
            directionMove = 2;
        }
    }

    // Assigns a number to the door and connects it with any button that may have the same number
    public void AssignNumber(int getNum)
    {
        myNumber = getNum;
        if (myNumber != 0)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Button").Length; i++)
            {
                if (GameObject.FindGameObjectsWithTag("Button")[i].GetComponent<ButtonPowerCheck>().myNumber == myNumber)
                {
                    GameObject.FindGameObjectsWithTag("Button")[i].GetComponent<ButtonPowerCheck>();
                }
            }
            FindButton();
        }
    }
    
    public void FindButton()
    {
        if (myNumber != 0)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Button").Length; i++)
            {
                if (GameObject.FindGameObjectsWithTag("Button")[i].GetComponent<ButtonPowerCheck>().myNumber == myNumber)
                {
                    GameObject.FindGameObjectsWithTag("Button")[i].GetComponent<ButtonPowerCheck>().AddDoor(this.GetComponent<Door>());
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpened && transform.position != endLocation)
        {
            transform.position = Vector3.MoveTowards(transform.position, endLocation, Time.deltaTime * speed);
        }
        else if (!isOpened)
        {
            if (directionMove == 1 && transform.position.x > startLocation.x)
            {
                transform.position = Vector3.MoveTowards(transform.position, startLocation, Time.deltaTime * speed);
            }
            if (directionMove == 2 && transform.position.x < startLocation.x)
            {
                transform.position = Vector3.MoveTowards(transform.position, startLocation, Time.deltaTime * speed);
            }
            if (directionMove == 3 && transform.position.z > startLocation.z)
            {
                transform.position = Vector3.MoveTowards(transform.position, startLocation, Time.deltaTime * speed);
            }
            if (directionMove == 4 && transform.position.z < startLocation.z)
            {
                transform.position = Vector3.MoveTowards(transform.position, startLocation, Time.deltaTime * speed);
            }
        }
    }
}
