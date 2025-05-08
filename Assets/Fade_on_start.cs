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

    // Start is called before the first frame update
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
        StartCoroutine(nameof(fadeIn));        
    }

    public IEnumerator fadeIn(){
        float currentalpha = startAlpha;
        //Color REALcolor = new Color(color.r, color.g, color.b, startAlpha);
        while (true){
            currentalpha += Time.deltaTime;
            if(hasText){
                text.color = new Color(color.r, color.g, color.b, currentalpha);
            }
            if(hasImage){
                image.color = new Color(color.r, color.g, color.b, currentalpha);
            }
            if(currentalpha >= maxAlpha){
                break;
            }
            yield return null;
        }   
    }
}
