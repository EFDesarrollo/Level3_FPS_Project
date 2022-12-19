using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckMenu : MonoBehaviour
{
    [Header("UI Containers")]
    public GameObject playerDeckContainer;
    public GameObject systemFireArmCardsContainer, systemHabilityCardsContainer;
    [Header("Card Prefab UI")]
    public GameObject cardUIPrefab;
    [SerializeField]
    private Deck playerDeck, systemDeck;
    [SerializeField]
    private List<GameObject> cardsPrefabList = new();

    private void Awake()
    {
        Debug.Log("DeckMenu: Player deck reference: "+ GameObject.FindGameObjectsWithTag("GameController").Length);
        GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameManager>().Deck = new Deck();
        playerDeck = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameManager>().Deck;
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("DeckMenu: Instantiate system Deck");
        OnStartInstantiateSystemCards();
        //Debug.Log("DeckMenu: Instantiate Player Deck");
        //instantiatePlayerCard(playerDeck.GetCards());
    }
    #region Methods
    private void OnStartInstantiateSystemCards()
    {
        Debug.Log("DeckMenu: Deleting Prefabs");
        cardsPrefabList.ForEach(card => Destroy(card));
        cardsPrefabList.Clear();
        Debug.Log("DeckMenu: Instantiating Prefabs");
        InstantiateCardList(CardListSorting(systemDeck.GetCardsOfType(1)), systemFireArmCardsContainer.transform);
        InstantiateCardList(CardListSorting(systemDeck.GetCardsOfType(2)), systemFireArmCardsContainer.transform);
        InstantiateCardList(CardListSorting(systemDeck.GetCardsOfType(3)), systemFireArmCardsContainer.transform);
    }
    private void InstantiateCardList(List<Card> cards, Transform parent)
    {
        Debug.Log("DeckMenu: Instantiating List");
        if (cards.Count == 0)
            return;
        for (int i = 0; i < cards.Count; i++)
        {
            GameObject prefab = Instantiate(cardUIPrefab, parent);
            prefab.GetComponent<CardUI>().setImage(cards[i].Image);
            prefab.GetComponent<CardUI>().inDeck = IsInPlayerDeck(cards[i]);
            prefab.GetComponent<CardUI>().Card = cards[i];
            cardsPrefabList.Add(prefab);
            Debug.Log("___Nombre: " + prefab.name);
        }
        MovePlayerCard();
    }
    private void MovePlayerCard()
    {
        foreach (GameObject card in cardsPrefabList)
        {
            if (card.GetComponent<CardUI>().inDeck)
            {
                card.transform.parent = playerDeckContainer.transform;
            }
            else
            {
                if (card.GetComponent<CardUI>().Card.Type > 1)
                {
                    card.transform.parent = systemHabilityCardsContainer.transform;
                }
                else
                {
                    card.transform.parent = systemFireArmCardsContainer.transform;
                }
            }
        }
    }
    #endregion
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
    
    #region OnClick
    public void ChangeAndUpdateList(GameObject Prefab)
    {
        Debug.Log("DeckMenu: OnClickChange Activated");
        if (Prefab.GetComponent<CardUI>().inDeck)
            playerDeck.QuitCard(Prefab.GetComponent<CardUI>().Card);
        else
        {
            List<Card> temp = new List<Card>();
            playerDeck.GetCards().ForEach(c => temp.Add(c));
            temp.Add(Prefab.GetComponent<CardUI>().Card);
            if (!playerDeck.IsValid(temp))
            {
                Prefab.GetComponent<Animator>().Play("Can`tDoThis", 0);
                return;
            }
            playerDeck.AddCard(Prefab.GetComponent<CardUI>().Card);
        }
        //InstantiateSystemCard(CardListSorting(systemDeck.GetCards()), systemCardsContainer.transform);
        OnStartInstantiateSystemCards();
    }
    #endregion
}
