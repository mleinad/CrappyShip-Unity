using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ASCIIImage : MonoBehaviour
{
    [SerializeField]
    RawImage image;
    
    [SerializeField]
    Camera cam;
    RenderTexture renderTexture;    //terminal rawImage | camera feed render texture
    //implement video mode
    
    
    public RenderTexture SetRenderTexture(int width, int height)
    {
        renderTexture = new RenderTexture(width, height, 0, RenderTextureFormat.ARGB32);
        cam.targetTexture = renderTexture;
        return renderTexture;
    }
    
    
    public void SetImage(Texture tex)
    {
        image.texture = tex;
    }

    public void SetWebCamTexture()
    {
        WebCamDevice device = WebCamTexture.devices[1];
        WebCamTexture tex = new WebCamTexture(device.name);
        
        SetImage(tex);
        tex.Play();
    }
}
