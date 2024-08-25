using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVignette : MonoBehaviour
{
    [SerializeField] private MeshRenderer vignetteMesh;
    [SerializeField] private float fadeTime;
    private float timer;

    private void Update()
    {
        if (timer <= 0) return;

        Color color = vignetteMesh.material.color;
        float newAlpha = timer / fadeTime;
        vignetteMesh.material.color = new Color(color.r, color.g, color.b, newAlpha);
        timer -= Time.deltaTime;
    }

    public void DoEffect()
    {
        timer = fadeTime;
    }
}
