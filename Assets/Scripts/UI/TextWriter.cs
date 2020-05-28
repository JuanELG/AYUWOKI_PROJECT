using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class TextWriter : MonoBehaviour
{
    private Text uiText;
    private string text_to_write;
    private int characterIndex;
    private float timePerCharter;
    private float timer;

    public void addWriter(Text uiText,string text_to_write,float timePerCharter){
        this.uiText = uiText;
        this.text_to_write = text_to_write;
        this.timePerCharter = timePerCharter;
        characterIndex = 0;
    }

    private void Update(){
        if(uiText != null){
            timer -= Time.deltaTime;
            if (timer<=0f){
                timer += timePerCharter;
                characterIndex ++;
                uiText.text = text_to_write.Substring(0,characterIndex);

                if (characterIndex >= text_to_write.Length){
                    uiText = null;
                    return;
                }
            }
        }
    }
}
