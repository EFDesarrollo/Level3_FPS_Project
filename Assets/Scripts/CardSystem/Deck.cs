using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Deck
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
    public List<Card> GetCardsOfType(int type)
    {
        List<Card> newList = new List<Card>();
        foreach (Card card in Cards)
        {
            Debug.Log("card type: " + card.Type);
            if (card.Type == type)
            {
                Debug.Log("Find type: "+type);
                newList.Add(card);
            }
        }
        return newList;
    }
}
