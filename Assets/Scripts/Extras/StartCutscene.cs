using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    [SerializeField] private GameObject[] toTurnOff;
    [SerializeField] private GameObject[] toTurnOn;
    [SerializeField] private string tagName;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == tagName)
        {
            for (int i = 0; i < toTurnOn.Length; i++)
            {
                toTurnOn[i].SetActive(true);
            }
            for (int i = 0; i < toTurnOff.Length; i++)
            {
                Destroy(toTurnOff[i]);
            }
            // StartCoroutine(myNumerator());
        }
    }

    /*
    IEnumerator myNumerator()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < toTurnOff.Length; i++)
        {
            Destroy(toTurnOff[i]);
        }
    }
    */
}
