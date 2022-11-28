using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeckManager : MonoBehaviour
{
    [SerializeField]
    private Deck deck;

    private void Start()
    {
        GetDeck();
    }

    #region Methods
    private void GetDeck()
    {
        deck = GameObject.Find("GameManager").GetComponent<GameManager>().Deck;
    }
    private void SetCharacterDeck(Deck newDeck)
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().Deck = newDeck;
    }
    #endregion
}
