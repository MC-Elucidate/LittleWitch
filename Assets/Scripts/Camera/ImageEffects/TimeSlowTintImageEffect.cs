using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowTintImageEffect : MonoBehaviour {

    public Material TintMaterial;

    void Start()
    {
        TintMaterial.color = TintMaterial.color;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (TintMaterial != null)
        {
            Graphics.Blit(src, dst, TintMaterial);
        }
    }
}
