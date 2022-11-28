using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckMenu : MonoBehaviour
{
    public GameObject playerDeckContainer, systemCardsContainer;
    public GameObject cardUIPrefab;
    [SerializeField]
    private Deck playerDeck, allCardsDeck;
    private List<GameObject> DeckOBJs = new();
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("DeckMenu: System deck reference");
        allCardsDeck = GetComponent<Deck>();
        Debug.Log("DeckMenu: Player deck reference");
        playerDeck = GetComponent<CharacterDeckManager>().GetDeck();
        Debug.Log("DeckMenu: Instantiate system Deck");
        instantiateSystemCard(allCardsDeck.GetCards());
        //Debug.Log("DeckMenu: Instantiate Player Deck");
        //instantiatePlayerCard(playerDeck.GetCards());
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void instantiateSystemCard(List<Card> cards)
    {
        Debug.Log("DeckMenu: Instantiating System Deck");
        if (cards.Count == 0)
            return;
        for (int i = 0; i < cards.Count; i++)
        {
            GameObject cardsContent = Instantiate(cardUIPrefab, systemCardsContainer.transform);
            cardsContent.GetComponent<CardUI>().setImage(cards[i].Image);
            cardsContent.GetComponent<CardUI>().inDeck = false;
            cardsContent.GetComponent<CardUI>().Card = cards[i];
            DeckOBJs.Add(cardsContent);
        }
    }
    /*public void instantiatePlayerCard(List<Card> cards)
    {
        Debug.Log("DeckMenu: Instantiating Player Deck");
        if (cards.Count == 0)
            return;
        for (int i = 0; i < cards.Count; i++)
        {
            GameObject cardsContent = Instantiate(cardUIPrefab, playerDeckContainer.transform);
            cardsContent.GetComponent<CardUI>().setImage(cards[i].Image);
            cardsContent.GetComponent<CardUI>().inDeck = true;
            cardsContent.GetComponent<CardUI>().Card = cards[i];
        }
    }*/
    public void UpdateList(GameObject card)
    {
        if (card.GetComponent<CardUI>().inDeck)
        {
            card.transform.parent = systemCardsContainer.transform;
            playerDeck.QuitCard(card.GetComponent<CardUI>().Card);
            Debug.Log(DeckOBJs[0].Equals(card));
        }
        else
        {
            card.transform.parent = playerDeckContainer.transform;
            playerDeck.AddCard(card.GetComponent<CardUI>().Card);
            //instantiatePlayerCard(playerDeck.GetCards());
        }
    }
}
