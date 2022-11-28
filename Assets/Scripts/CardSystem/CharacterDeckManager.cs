using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeckManager : MonoBehaviour
{
    [SerializeField]
    private Deck deck;
    public string gameMangerTag;

    private void Start()
    {
        Debug.Log("ManagerGettingDeck");
        deck = GetDeck();
    }

    #region Methods
    public Deck GetDeck()
    {
        Debug.Log("CharacterDeckManager: GetDeck");
        Debug.Log(GameObject.FindGameObjectsWithTag(gameMangerTag).Length);
        Deck deck = GameObject.FindGameObjectsWithTag(gameMangerTag)[0].GetComponent<GameManager>().Deck;
        if (deck == null)
            deck = new Deck();
        return deck;
    }
    public void SetCharacterDeck(Deck newDeck)
    {
        Debug.Log("CharacterDeckManager: SetingDeck");
        GameObject.Find(gameMangerTag).GetComponent<GameManager>().Deck = newDeck;
    }
    #endregion
}
