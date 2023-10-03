using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMaker : MonoBehaviour
{
    [SerializeField] private GameObject faceNorth;
    [SerializeField] private GameObject faceSouth;
    [SerializeField] private GameObject faceEast;
    [SerializeField] private GameObject faceWest;

    public void Direction(string compass)
    {
        if (compass == "North")
        {
            faceNorth.SetActive(true);
        }
        if (compass == "South")
        {
            faceSouth.SetActive(true);
        }
        if (compass == "East")
        {
            faceEast.SetActive(true);
        }
        if (compass == "West")
        {
            faceWest.SetActive(true);
        }
    }

    public void InvisibleDirection(string compass)
    {
        if (compass == "North")
        {
            faceNorth.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (compass == "South")
        {
            faceSouth.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (compass == "East")
        {
            faceEast.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (compass == "West")
        {
            faceWest.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
