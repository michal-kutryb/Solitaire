using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card Card;
    private Image _image;
    
    private void Awake()
    {
        _image = GetComponent<Image>();
    }
}
