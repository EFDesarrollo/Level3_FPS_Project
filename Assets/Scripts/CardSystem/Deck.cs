using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Deck : MonoBehaviour
{
    public string Name;
    [SerializeField]
    private List<Card> Cards;

    public bool IsValid(List<Card> deck)
    {
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
        List<Card> temp = Cards;
        temp.Add(card);
        if (IsValid(temp))
            Cards = temp;
    }
    public void QuitCard(Card card)
    {
        if (Cards.Contains(card))
            Cards.Remove(card);
    }
    public void SetCards(List<Card> newCards)
    {
        Cards = newCards;
    }
    public List<Card> GetCards()
    {
        return Cards;
    }
}
