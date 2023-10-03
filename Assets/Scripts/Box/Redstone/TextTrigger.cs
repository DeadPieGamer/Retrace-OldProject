using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    [SerializeField] private TextNeedsScript overlordText;
    [SerializeField] private int whichText;

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Collided with " + col.gameObject.tag);
        if (col.gameObject.tag == "Player")
        {
            overlordText.ChooseAText(whichText);
        }
    }
}
