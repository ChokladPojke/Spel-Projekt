using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Fade_on_start : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI text;
    public Color color;
    public bool hasImage;
    public float startAlpha;
    public bool hasText;
    public float maxAlpha = 1f;
    public bool fadeOut;

    // Find the image or text component
    void OnEnable(){   
        if (GetComponent<TextMeshProUGUI>() != null){
            text = GetComponent<TextMeshProUGUI>();
            hasText = true;
            color = text.color;
        }   
        if (GetComponent<Image>() != null){
            image = GetComponent<Image>();
            hasImage = true;
            color = image.color;
        }
        StartCoroutine(nameof(fade));        
    }
    // Fade in or out
    // depending on the value of fadeOut
    public IEnumerator fade(){
        float currentalpha = startAlpha;
        while (true){
            if (fadeOut)
            {
                currentalpha -= Time.unscaledDeltaTime;
            }
            else
            {
                currentalpha += Time.unscaledDeltaTime;
            }
            if(hasText){
                text.color = new Color(color.r, color.g, color.b, currentalpha);
            }
            if(hasImage){
                image.color = new Color(color.r, color.g, color.b, currentalpha);
            }
            if(fadeOut == false && currentalpha >= maxAlpha){
                break;
            }
            else if(fadeOut == true && currentalpha <= 0){
                break;
            }
            yield return null;
        }   
    }
}
