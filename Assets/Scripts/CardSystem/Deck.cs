using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Deck : MonoBehaviour
{
    public string Name = "Default";
    [SerializeField]
    private List<Card> Cards;

    public Deck()
    {
        Cards = new List<Card>();
    }
    public bool IsValid(List<Card> deck)
    {
        Debug.Log("Deck: IsValid");
        int validateNum = 0;
        foreach (Card i in deck)
        {
            validateNum += i.Type;
        }

        if (validateNum <= 16)
            return true;

        return false;
    }
    public void AddCard(Card card)
    {
        Debug.Log("Deck: AddCard");
        List<Card> temp = Cards;
        temp.Add(card);
        if (IsValid(temp))
            Cards = temp;
    }
    public void QuitCard(Card card)
    {
        Debug.Log("Deck: QuitCard");
        if (Cards.Contains(card))
            Cards.Remove(card);
    }
    public void SetCards(List<Card> newCards)
    {
        Debug.Log("Deck: SetCard");
        Cards = newCards;
    }
    public List<Card> GetCards()
    {
        Debug.Log("Deck: GetCards");
        return Cards;
    }
}
