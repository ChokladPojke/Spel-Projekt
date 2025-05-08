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
        StartCoroutine(nameof(fade));        
    }

    public IEnumerator fade(){
        float currentalpha = startAlpha;
        //Color REALcolor = new Color(color.r, color.g, color.b, startAlpha);
        while (true){
            if(fadeOut){
                currentalpha -= Time.deltaTime;
            }
            else{
                currentalpha += Time.deltaTime;
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
