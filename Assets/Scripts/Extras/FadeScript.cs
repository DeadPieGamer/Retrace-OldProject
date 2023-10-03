using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    private float currentFade;
    [SerializeField] private bool dieAfter = true;
    [SerializeField] private float fadeLength = 2f;
    [SerializeField] private RawImage myImage;
    [SerializeField] private Texture fade2;
    [SerializeField] private Texture fade3;
    [SerializeField] private Texture fade4;
       
    void Start()
    {
        currentFade = 4 * fadeLength;
    }

    // Update is called once per frame
    void Update()
    {
        currentFade -= Time.deltaTime;
        if (currentFade <= 3 * fadeLength && currentFade > 2 * fadeLength)
            myImage.texture = fade2;
        if (currentFade <= 2 * fadeLength && currentFade > fadeLength)
            myImage.texture = fade3;
        if (currentFade <= fadeLength && currentFade > 0f)
            myImage.texture = fade4;
        if (currentFade <= 0f && dieAfter)
            Destroy(gameObject);
    }
}
