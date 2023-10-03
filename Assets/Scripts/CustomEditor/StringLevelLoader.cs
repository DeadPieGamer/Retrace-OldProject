using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using TMPro;

public class StringLevelLoader : MonoBehaviour
{
    /*
        Mission for next time:
        Figure out how to check public int of all gameobjects with a certain tag (door/button number connection)
        ^ Is maybe done now

        Try out a test level
        Fix thousands of bugs that suddenly appeared

        Add glass
        ^ Is added

        Add destructible walls
        Add option to turn faces invisible (maybe only on destructible walls)
        Add a custom start (with/without gun)
        Add gun as placeable
        Add a goal-post?
    */

    // Rotation of walls
    [SerializeField] private Quaternion wallRot = new Quaternion(0, 0, 90, 0);

    // All the assigned objects that can be created
    [SerializeField] private GameObject player;
    [SerializeField] private Transform[] decorations;
    [SerializeField] private Transform wall;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject glass;
    [SerializeField] private GameObject crackedWall;

    // Strings for determining which blocks are called what
    private const string sEmpty = ".";
    private const string sDecoration = "Q";
    private const string sWall = "X";
    private const string sButton = "B";
    private const string sDoor = "D";
    private const string sStart = "P";
    private const string sGlass = "G";
    private const string sCrack = "C";

    // A float for determining the rotation of objects
    private float tempRot = 0f;

    // A string for determining whether there are numbers in the currently loading block
    private string currentString = string.Empty;
    private string emptyNumber = string.Empty;
    private string emptyString = string.Empty;
    private int currentVal = 0;
    
    // Something mega cool that turns the text into a grid
    string[][] readString(string file)
    {
        string text = file;
        string[] lines = Regex.Split(text, "\r\n");
        int rows = lines.Length;

        string[][] levelBase = new string[rows][];
        for (int i = 0; i < lines.Length; i++)
        {
            string[] stringsOfLine = Regex.Split(lines[i], " ");
            levelBase[i] = stringsOfLine;
        }
        return levelBase;
    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GimmeLevel(TMP_InputField coolTxtLevel)
    {
        // A grid text reader
        string[][] jagged = readString(coolTxtLevel.text);

        // Create planes based on matrix (Go through every x for each z direction of the text map)
        for (int z = 0; z < jagged.Length; z++)
        {
            for (int x = 0; x < jagged[0].Length; x++)
            {
                tempRot = 0f;
                emptyNumber = string.Empty;
                emptyString = string.Empty;
                currentString = jagged[z][x];

                // Checks if the current position is an unknown variable and tells me if it is
                if (!currentString.StartsWith(sButton) && !currentString.StartsWith(sEmpty) && !currentString.StartsWith(sStart) && !currentString.StartsWith(sWall) && !currentString.StartsWith(sDecoration) && !currentString.StartsWith(sDoor) && !currentString.StartsWith(sGlass) && !currentString.StartsWith(sCrack))
                {
                    Debug.Log("Location (" + x + ", -" + z + ") does not contain any readable block (" + currentString + ")");
                }

                // Checks if the current position is nothing
                if (currentString.StartsWith(sEmpty))
                {
                    // Doesn't do anything but tell me it's nothing
                    // Debug.Log("Placed nothing at (" + x + ", " + z + ")");
                }

                // Checks if the current position is a Wall
                if (currentString.StartsWith(sWall))
                {
                    // Instantiates a wall at said location
                    Instantiate(wall, new Vector3(x, 0, -z), wallRot);
                    // Debug.Log("Placed " + "wall".Bold() + " at (" + x + ", -" + z + ")");
                }

                // Checks if the current position is Glass
                if (currentString.StartsWith(sGlass))
                {
                    // Instantiates a wall at said location
                    Instantiate(glass, new Vector3(x, 0, -z), Quaternion.identity);
                    Debug.Log("Placed " + "glass".Bold() + " at (" + x + ", -" + z + ")");
                }

                // Checks if the current string is a Door
                if (currentString.StartsWith(sDoor))
                {
                    // Determines the direction the door should be facing
                    if (currentString.Contains("N"))
                    {
                        tempRot = 90f;
                        emptyString = "East";
                    }
                    else if (currentString.Contains("E"))
                    {
                        tempRot = 180f;
                        emptyString = "South";
                    }
                    else if (currentString.Contains("S"))
                    {
                        tempRot = 270f;
                        emptyString = "West";
                    }
                    else if (currentString.Contains("W"))
                    {
                        tempRot = 0f;
                        emptyString = "North"; 
                    }
                    
                    // Checks if a number was assigned for the button (Numbers connect buttons and mechanics such as doors)
                    for (int i = 0; i < currentString.Length; i++)
                    {
                        if (System.Char.IsDigit(currentString[i]))
                        {
                            emptyNumber += currentString[i];
                        }
                    }

                    // If a number was assigned, use that, otherwise make it a 0 (0 is a default with which object wont interact with other mechanics)
                    if (emptyNumber.Length > 0)
                    {
                        currentVal = int.Parse(emptyNumber);
                    }
                    else
                    {
                        currentVal = 0;
                    }

                    // Creates door and tells it which way it should move
                    GameObject gogogoChange = (GameObject)Instantiate(door, new Vector3(x, 0, -z), Quaternion.identity);
                    gogogoChange.transform.Rotate(0f, tempRot, 0f);
                    gogogoChange.GetComponent<Door>().FindOutDirection(emptyString);
                    gogogoChange.GetComponent<Door>().AssignNumber(currentVal);
                    Debug.Log("Placed " + "door".Bold() + " at (" + x + ", -" + z + ")");
                }

                // Checks if current location is a button
                if (currentString.StartsWith(sButton))
                {
                    // Creates button
                    GameObject gogogoChange = (GameObject)Instantiate(button, new Vector3(x, 0, -z), Quaternion.identity);
                    // Checks which sides of the button should have faces
                    if (currentString.Contains("N"))
                    {
                        gogogoChange.GetComponent<FaceMaker>().Direction("North");
                    }
                    if (currentString.Contains("E"))
                    {
                        gogogoChange.GetComponent<FaceMaker>().Direction("East");
                    }
                    if (currentString.Contains("S"))
                    {
                        gogogoChange.GetComponent<FaceMaker>().Direction("South");
                    }
                    if (currentString.Contains("W"))
                    {
                        gogogoChange.GetComponent<FaceMaker>().Direction("West");
                    }

                    // Checks if a number was assigned for the button (Numbers connect buttons and mechanics such as doors)
                    for (int i = 0; i < currentString.Length; i++)
                    {
                        if (System.Char.IsDigit(currentString[i]))
                        {
                            emptyNumber += currentString[i];
                        }
                    }

                    // If a number was assigned, use that, otherwise make it a 0 (0 is a default with which object wont interact with other mechanics)
                    if (emptyNumber.Length > 0)
                    {
                        currentVal = int.Parse(emptyNumber);
                    }
                    else
                    {
                        currentVal = 0;
                    }
                    gogogoChange.GetComponent<ButtonPowerCheck>().SetNumber(currentVal);
                    Debug.Log("Placed " + "button".Bold() + " at (" + x + ", -" + z + ")");
                }

                // Checks if current location is a cracked wall
                if (currentString.StartsWith(sCrack))
                {
                    // Creates cracked wall
                    GameObject gogogoChange = (GameObject)Instantiate(crackedWall, new Vector3(x, 0, -z), wallRot);

                    // Checks if a number was assigned for the cracked wall
                    for (int i = 0; i < currentString.Length; i++)
                    {
                        if (System.Char.IsDigit(currentString[i]))
                        {
                            emptyNumber += currentString[i];
                        }
                    }

                    // If a number was assigned, determine which and maybe make cracks invisible
                    if (emptyNumber.Length > 0)
                    {
                        currentVal = int.Parse(emptyNumber);
                    }
                    else
                    {
                        currentVal = 0;
                    }
                    if (currentVal == 1)
                    {
                        gogogoChange.GetComponent<FaceMaker>().InvisibleDirection("North");
                        gogogoChange.GetComponent<FaceMaker>().InvisibleDirection("East");
                        gogogoChange.GetComponent<FaceMaker>().InvisibleDirection("South");
                        gogogoChange.GetComponent<FaceMaker>().InvisibleDirection("West");
                    }
                    Debug.Log("Placed " + "cracked wall".Bold() + " at (" + x + ", -" + z + ")");
                }

                // Checks if the current position is a decoration spot
                if (currentString.StartsWith(sDecoration))
                {
                    // Checks if a number was assigned for the decoration
                    for (int i = 0; i < currentString.Length; i++)
                    {
                        if (System.Char.IsDigit(currentString[i]))
                        {
                            emptyNumber += currentString[i];
                        }
                    }

                    // If a number was assigned, use that, otherwise make it a random decoration
                    if (emptyNumber.Length > 0)
                    {
                        currentVal = int.Parse(emptyNumber);
                    }
                    else
                    {
                        currentVal = Random.Range(0, decorations.Length);
                    }

                    // Instantiate the decoration chosen
                    Instantiate(decorations[currentVal], new Vector3(x, 0, -z), Quaternion.identity);
                    Debug.Log("Placed " + "decoration".Bold() + " at (" + x + ", -" + z + ")");
                }

                // Checks if the current position is the spawn-point
                if (currentString.StartsWith(sStart))
                {
                    // Determines the direction the player should be facing
                    if (currentString.Contains("N"))
                    {
                        tempRot = 0f;
                    }
                    else if (currentString.Contains("E"))
                    {
                        tempRot = 90f;
                    }
                    else if (currentString.Contains("S"))
                    {
                        Debug.Log("Reached South Section");
                        tempRot = 180f;
                    }
                    else if (currentString.Contains("W"))
                    {
                        tempRot = 270f;
                    }

                    // Checks if a number was assigned for the player
                    for (int i = 0; i < currentString.Length; i++)
                    {
                        if (System.Char.IsDigit(currentString[i]))
                        {
                            emptyNumber += currentString[i];
                        }
                    }

                    // If a number was assigned, use that, otherwise make it 0
                    if (emptyNumber.Length > 0)
                    {
                        currentVal = int.Parse(emptyNumber);
                    }
                    else
                    {
                        currentVal = 0;
                    }

                    // Instantiates player at said coordinates and rotation
                    // GameObject gogogoChange = (GameObject)Instantiate(player, new Vector3(x, 0, -z), new Quaternion(0f, tempRot, 0f, 0f));
                    // Moves player character to desired position
                    player.transform.position = new Vector3(x, 0, -z);
                    player.transform.Rotate(0f, tempRot, 0f);
                    // Gets player components such as music and changes them according to the loader settings
                    // player.gameObject.GetComponent<blank>();
                    Debug.Log("Made " + "player spawnpoint".Bold() + " at (" + x + ", -" + z + ")");
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
