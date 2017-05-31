using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowTintImageEffect : MonoBehaviour {

    public Material TintMaterial;

    void Start()
    {
        enabled = false;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (TintMaterial != null)
            Graphics.Blit(src, dst, TintMaterial);
    }
}
