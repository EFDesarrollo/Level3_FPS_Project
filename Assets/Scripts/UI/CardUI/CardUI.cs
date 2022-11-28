using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public bool inDeck = false;
    public Card Card;
    public void setImage(Sprite img)
    {
        Debug.Log("CardUI: SetingImage");
        GetComponent<Image>().sprite = img;
    }
}
