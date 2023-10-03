using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDestroy : MonoBehaviour
{
    // [SerializeField] private GameObject[] toCreate;
    [SerializeField] private GameObject[] toDestroy;
    [SerializeField] private GameObject[] toEnable;
    [SerializeField] private GameObject[] toDisable;

    /*
    public void Create()
    {
        for (int i = 0; i < toCreate.Length; i++;)
        {
            Instantiate(toCreate[i]);
        }
    }
    */

    public void Destroy()
    {
        for (int i = 0; i < toDestroy.Length; i++)
        {
            Destroy(toDestroy[i]);
        }
    }

    public void Enable()
    {
        for (int i = 0; i < toEnable.Length; i++)
        {
            toEnable[i].SetActive(true);
        }
    }

    public void Disable()
    {
        for (int i = 0; i < toDisable.Length; i++)
        {
            toDisable[i].SetActive(false);
        }
    }
}
