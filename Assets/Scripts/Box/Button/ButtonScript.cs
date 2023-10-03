using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public Material off_Material;
    public Material on_Material;
    private Renderer m_Renderer;
    public Material off_Face;
    public Material on_Face;
    private Renderer q_Renderer;

    private ButtonPowerCheck parentPower;
    public bool isOn = false;
    public GameObject buttonSFXPrefab;

    // Start is called before the first frame update
    void Start()
    {
        parentPower = transform.parent.GetComponent<ButtonPowerCheck>();
        parentPower.AddFace(this.GetComponent<ButtonScript>());
        m_Renderer = transform.parent.GetComponent<Renderer>();
        q_Renderer = GetComponent<Renderer>();
    }

    /*
    void Update()
    {
        if (parentPower.isOn != isOn)
        {
            isOn = parentPower.isOn;
        }
    }
    */

    public void ChangeState(bool nowOn)
    {
        isOn = nowOn;
        if (isOn)
        {
            m_Renderer.material = on_Material;
            q_Renderer.material = on_Face;
        }
        else
        {
            m_Renderer.material = off_Material;
            q_Renderer.material = off_Face;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GoodBullet")
        {
            ShotButton();
        }
    }

    void ShotButton()
    {
        Instantiate(buttonSFXPrefab, transform.position, Quaternion.identity);
        ChangeState(!isOn);
        parentPower.ChangedState(isOn);
    }
}
