using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Manager : MonoBehaviour
{
    public GameObject HabilityCardContent, FireArmCard, HealthBar, EnduranceBar, BulletsBar;
    public int selectedCard, crd;
    [SerializeField]
    private List<Card> cardsForHUD;
    private CharacterDeckManager characterDeckManager;
    // Start is called before the first frame update
    void Start()
    {
        characterDeckManager = GetComponent<CharacterDeckManager>();
        InstantiateHabilityCards();
        HabilitySelectCard();
    }

    // Update is called once per frame
    void Update()
    {
        HabilityCardSelecting();
        if (Input.GetKeyUp(KeyCode.Q))
        {
            CrdSelecting();
            if (CardExistInHUD(cardsForHUD[crd]))
                CrdSelecting();
            //----
            CardUI currentCardInHUD = HabilityCardContent.transform.GetChild(selectedCard).GetComponent<CardUI>();
            StartCoroutine(GetComponent<CharacterStatsManager>().TimedUpdateCharacterStats(currentCardInHUD.Card.NewCharacterStats, currentCardInHUD.Card.ActiveTime));
            currentCardInHUD.setImage(cardsForHUD[crd].Image);
            currentCardInHUD.Card = cardsForHUD[crd];
        }
    }
    public bool CardExistInHUD(Card card)
    {
        List<Card> CardsInHUD = new List<Card>();
        foreach (Transform t in HabilityCardContent.transform)
        {
            CardsInHUD.Add(t.GetComponent<CardUI>().Card);
        }
        foreach (Card c in CardsInHUD)
        {
            if (c == card)
            {
                return true;
            }
        }
        return false;
    }
    public void CrdSelecting()
    {
        if (crd >= cardsForHUD.Count - 1)
            crd = 0;
        else
            crd++;
    }
    public void InstantiateHabilityCards()
    {
        cardsForHUD = characterDeckManager.GetHabilityCards(true);
        int i = 0;
        foreach (Transform t in HabilityCardContent.transform)
        {
            t.GetComponent<CardUI>().setImage(cardsForHUD[i].Image);
            t.GetComponent<CardUI>().Card = cardsForHUD[i];
            crd = i;
            i++;
        }
    }
    public void HabilityCardSelecting()
    {
        int previousSelectedCard = selectedCard;
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectedCard >= HabilityCardContent.transform.childCount - 1)
                selectedCard = 0;
            else
                selectedCard++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectedCard <= 0)
                selectedCard = HabilityCardContent.transform.childCount - 1;
            else
                selectedCard--;
        }
        if (previousSelectedCard != selectedCard)
        {
            HabilitySelectCard();
        }
    }
    public void HabilitySelectCard()
    {
        int i = 0;
        foreach (Transform t in HabilityCardContent.transform)
        {
            if (i == selectedCard)
                t.GetComponent<Image>().color = Color.red;
            else
                t.GetComponent<Image>().color = Color.white;
            i++;
        }
    }
    public void ChangeFireArmCard(Card card)
    {
        FireArmCard.GetComponent<CardUI>().setImage(card.Image);
        FireArmCard.GetComponent<CardUI>().Card = card;
    }
}
