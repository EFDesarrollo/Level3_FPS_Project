using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckMenu : MonoBehaviour
{
    public GameObject playerDeckContainer, systemCardsContainer;
    public GameObject cardUIPrefab;
    [SerializeField]
    private Deck playerDeck, systemDeck;
    private List<GameObject> cardsPrefabList = new();
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("DeckMenu: System deck reference");
        Debug.Log("DeckMenu: Player deck reference");
        playerDeck = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameManager>().Deck;
        Debug.Log("DeckMenu: Instantiate system Deck");
        InstantiateSystemCard(CardListSorting(systemDeck.GetCards()), systemCardsContainer.transform);
        //Debug.Log("DeckMenu: Instantiate Player Deck");
        //instantiatePlayerCard(playerDeck.GetCards());
    }
    public int SortFunction(Card a, Card b)
    {
        if (a.Type < b.Type)
            return -1;
        if (a.Type > b.Type)
            return 1;
        return 0;
    }
    public List<Card> CardListSorting(List<Card> deck)
    {
        deck.Sort(SortFunction);
        return deck;
    }public bool IsInPlayerDeck(Card a)
    {
        foreach (Card b in playerDeck.GetCards())
        {
            if (a == b) return true;
        }
        return false;
    }
    public void MovePlayerCard()
    {
        foreach(GameObject card in cardsPrefabList)
        {
            if (card.GetComponent<CardUI>().inDeck)
            {
                card.transform.parent = playerDeckContainer.transform;
            }
            else
            {
                card.transform.parent = systemCardsContainer.transform;
            }
        }
    }
    public void InstantiateSystemCard(List<Card> cards, Transform parent)
    {
        cardsPrefabList.ForEach(card => Destroy(card));
        cardsPrefabList.Clear();
        Debug.Log("DeckMenu: Instantiating System Deck");
        if (cards.Count == 0)
            return;
        for (int i = 0; i < cards.Count; i++)
        {
            GameObject prefab = Instantiate(cardUIPrefab, parent);
            prefab.GetComponent<CardUI>().setImage(cards[i].Image);
            prefab.GetComponent<CardUI>().inDeck = IsInPlayerDeck(cards[i]);
            prefab.GetComponent<CardUI>().Card = cards[i];
            cardsPrefabList.Add(prefab);
        }
        MovePlayerCard();
    }
    public void OnClickChangeAndUpdateList(GameObject Prefab)
    {
        if (Prefab.GetComponent<CardUI>().inDeck)
            playerDeck.QuitCard(Prefab.GetComponent<CardUI>().Card);
        else
            playerDeck.AddCard(Prefab.GetComponent<CardUI>().Card);
        InstantiateSystemCard(CardListSorting(systemDeck.GetCards()), systemCardsContainer.transform);
    }
}
