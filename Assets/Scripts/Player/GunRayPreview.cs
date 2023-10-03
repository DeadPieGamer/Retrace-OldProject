using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRayPreview : MonoBehaviour
{
    public Material on_Material;
    private Renderer m_Renderer;
    private Camera cam;
    public float chargeUp = 0.8f;
    private float currentCharge;
    [SerializeField] private LayerMask ignoreMe;

    // Start is called before the first frame update
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        cam = Camera.main;
        currentCharge = chargeUp;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, ~ignoreMe))
        {
            if (Input.GetButtonDown("Fire1") || Input.GetButton("Fire1"))
            {
                if (currentCharge > 0f)
                {
                    currentCharge -= Time.deltaTime;
                }
                else
                {
                    m_Renderer.material = on_Material;
                }

                transform.position = hit.point;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
