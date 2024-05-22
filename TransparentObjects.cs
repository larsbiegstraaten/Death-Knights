using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentObjects : MonoBehaviour
{
    public float fadeSpeeed;
    public float fadeAmount;
    float originalOpacity;
    Material[] mats;
    public bool doFade = false;
    // Start is called before the first frame update
    void Start()
    {
        fadeAmount = 0.2f;
        fadeSpeeed = 7f;

        mats = GetComponent<Renderer>().materials;
        foreach (Material mat in mats)
        {
            originalOpacity = mat.color.a;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (doFade)
        {
            FadeNow();
        }

        else
        {
            resetFade();
        }


    }
    void FadeNow()
    {
        foreach (Material mat in mats)
        {
            // Cambiar el rendering mode a Transparent
            mat.SetFloat("_Mode", 3);
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000; // Valor para materiales transparentes
            Color currentColor = mat.color;
            Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, fadeAmount, fadeSpeeed * Time.deltaTime));
            mat.color = smoothColor;
        }
    }
    void resetFade()
    {
        foreach (Material mat in mats)
        {
            Color currentColor = mat.color;
            float targetAlpha = originalOpacity; // Alpha original

            // Interpolacion para devolver el alpha a su valor original de manera mas rapida
            Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, targetAlpha, fadeSpeeed * Time.deltaTime));
            mat.color = smoothColor;

            // Verificar si el alpha ha alcanzado el valor original lo suficiente
            if (Mathf.Abs(mat.color.a - targetAlpha) < 0.4f)
            {
                // Restaurar el rendering mode a Opaque
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                mat.SetInt("_ZWrite", 1);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.DisableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = -1; // Valor por defecto para materiales opacos
            }
        }
    }



}
