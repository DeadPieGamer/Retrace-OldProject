using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPowerCheck : MonoBehaviour
{
    public bool isOn = false;
    public int myNumber = 0;

    public List<Door> myDoors = new List<Door>();
    public List<ButtonScript> myFaces = new List<ButtonScript>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Sets the number of the Button for future connection with door. Will check if any current doors are connected with this specific number and connect if that is the case
    public void SetNumber(int getNum)
    {
        myNumber = getNum;
        if (myNumber != 0)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Door").Length; i++)
            {
                if (GameObject.FindGameObjectsWithTag("Door")[i].GetComponent<Door>().myNumber == myNumber)
                {

                }
                GameObject.FindGameObjectsWithTag("Door")[i].GetComponent<Door>().FindButton();
            }
            // Debug.Log("You have yet to program this".Bold().Color("red"));
        }
    }

    public void AddFace(ButtonScript newFace)
    {
        myFaces.Add(newFace);
    }

    public void AddDoor(Door extraDoor)
    {
        myDoors.Add(extraDoor);
    }

    public void ChangedState(bool nowOn)
    {
        isOn = nowOn;
        // Debug.Log("Button On is " + isOn);
        for (int i = 0; i < myDoors.Count; i++)
        {
            // Debug.Log("Talking to door #" + i);
            myDoors[i].isOpened = isOn;
        }
        for (int faceInt = 0; faceInt < myFaces.Count; faceInt++)
        {
            // Debug.Log("Talking to face #" + faceInt);
            myFaces[faceInt].ChangeState(isOn);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
