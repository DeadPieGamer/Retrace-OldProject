using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeScreenPixel : MonoBehaviour
{
    public RenderTexture mainSceneRT;
    private RenderTexture tempRT;
    
    void OnPostRender()
    {
        tempRT = mainSceneRT;
        Camera.main.targetTexture = null;
        Graphics.Blit(tempRT,null as RenderTexture);
    }
}
