using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFade : MonoBehaviour
{
    private TextMeshPro text;

    void Start()
    {
        text = GetComponent<TextMeshPro>();
        StartCoroutine(FadeOut());
    }

    //slowly fades the text
    private IEnumerator FadeOut()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime ));
            yield return null;
        }
        Destroy(gameObject);
    }
}
