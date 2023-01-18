using TMPro;
using UnityEngine;
using utils;

public class MovingText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] float sizingSpeed;
    [SerializeField] float maxSize;
    [SerializeField] float minSize;
    bool goesToMax = true;
    private void FixedUpdate() {
        if (goesToMax) {
            text.rectTransform.sizeDelta = text.rectTransform.sizeDelta.UpdateAxis(text.rectTransform.sizeDelta.y + sizingSpeed, VectorAxis.Y) ;
            if(text.fontSize > maxSize) goesToMax = false;
        }
        else {
            text.rectTransform.sizeDelta = text.rectTransform.sizeDelta.UpdateAxis(text.rectTransform.sizeDelta.y - sizingSpeed, VectorAxis.Y);
            if (text.fontSize < minSize) goesToMax = true;
        }
        
    }

}
