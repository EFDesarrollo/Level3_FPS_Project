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
    public void OnClick()
    {
        Debug.Log("MenuController Find: "+GameObject.FindGameObjectsWithTag("MenuController").Length);
        GameObject obj = GameObject.FindGameObjectsWithTag("MenuController")[0];
        obj.GetComponent<DeckMenu>().ChangeAndUpdateList(this.gameObject);
    }
}
