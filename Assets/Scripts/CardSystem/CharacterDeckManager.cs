using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeckManager : MonoBehaviour
{
    [SerializeField]
    private Deck deck;
    [SerializeField] private int selectedCard;
    public string gameMangerTag;

    private void Awake()
    {
        deck = GetDeck();
    }
    private void Start()
    {
    }

    #region Methods
    public Deck GetDeck()
    {
        Deck deck = GameObject.FindGameObjectsWithTag(gameMangerTag)[0].GetComponent<GameManager>().Deck;
        if (deck == null)
            deck = new Deck();
        return deck;
    }
    public void SetCharacterDeck(Deck newDeck)
    {
        GameObject.Find(gameMangerTag).GetComponent<GameManager>().Deck = newDeck;
    }
    public List<Card> GetHabilityCards(bool sorted = false)
    {
        List<Card> cards = new List<Card>();
        foreach (Card card in deck.GetCards())
        {
            if (card.Type > 1)
                cards.Add(card);
        }
        if (sorted)
            return SortCards(cards);
        return cards;
    }
    public List<Card> GetFireArmCards(bool sorted = false)
    {
        List<Card> cards = new List<Card>();
        foreach (Card card in deck.GetCards())
        {
            if (card.Type == 1)
                cards.Add(card);
        }
        if (sorted)
            return SortCards(cards);
        return cards;
    }
    public List<Card> SortCards(List<Card> cards)
    {
        List<Card> sortedCards = cards;
        foreach (Card card in sortedCards)
        {
            card.Priority = Random.Range(0, 10);
        }
        sortedCards.Sort(SortFunctionByPriority);
        return sortedCards;
    }
    public int SortFunctionByPriority(Card a, Card b)
    {
        if (a.Priority < b.Priority)
            return -1;
        if (a.Priority > b.Priority)
            return 1;
        return 0;
    }
    #endregion
}
