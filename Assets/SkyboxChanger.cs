using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material[] Skybox;
    // Start is called before the first frame update
   
    public void ChangeSkybox(int SkyboxNumber)
    {
        RenderSettings.skybox = Skybox[SkyboxNumber];
    }
 
}
