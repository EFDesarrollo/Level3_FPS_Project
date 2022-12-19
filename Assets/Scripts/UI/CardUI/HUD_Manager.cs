using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Manager : MonoBehaviour
{
    public GameObject HabilityCardContent, FireArmCard, HealthBar, EnduranceBar, BulletsBar;
    public List<GameObject> CardPointsUI;
    public Color ValidCard = Color.green, InvalidCard = Color.red;
    private int selectedCard, crd;
    private List<Card> cardsForHUD;
    private CharacterDeckManager characterDeckManager;
    private CharacterStatsManager characterStatsManager;
    private FireArm fireArmManager;
    // Start is called before the first frame update
    void Start()
    {
        characterDeckManager = GetComponent<CharacterDeckManager>();
        characterStatsManager = GetComponent<CharacterStatsManager>();
        fireArmManager = GetComponent<FireArm>();
        InstantiateHabilityCards();
        HabilitySelectCard();
        UpdateCardPointsUI();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthUI();
        UpdateBulletUI();
        UpdateEnduranceUI();
        if (HabilityCardContent == null) return;
        HabilityCardSelecting();
        CardUI currentCardInHUD = HabilityCardContent.transform.GetChild(selectedCard).GetComponent<CardUI>();
        if (Input.GetKeyUp(KeyCode.Q) && characterStatsManager.Stats.CardPoints >= currentCardInHUD.Card.Value)
        {
            characterStatsManager.SubstractCardPoints(currentCardInHUD.Card.Value);
            CrdSelecting();
            if (CardExistInHUD(cardsForHUD[crd]))
                CrdSelecting();
            //----
            StartCoroutine(GetComponent<CharacterStatsManager>().TimedUpdateCharacterStats(currentCardInHUD.Card.NewCharacterStats, currentCardInHUD.Card.ActiveTime));
            currentCardInHUD.setImage(cardsForHUD[crd].Image);
            currentCardInHUD.Card = cardsForHUD[crd];
            HabilitySelectCard();
        }
        HabilitySelectCard();
        UpdateCardPointsUI();
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
        if (HabilityCardContent == null) return;
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
        if (HabilityCardContent == null) return;
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
        if (HabilityCardContent == null) return;
        int i = 0;
        foreach (Transform t in HabilityCardContent.transform)
        {
            if (i == selectedCard)
            {
                t.GetComponent<Image>().color = ValidCard;
                if (t.GetComponent<CardUI>().Card.Value > characterStatsManager.Stats.CardPoints)
                    t.GetComponent<Image>().color = InvalidCard;
            }
            else
                t.GetComponent<Image>().color = Color.white;
            i++;
        }
    }
    public void ChangeFireArmCard(Card card)
    {
        if (FireArmCard == null) return;
        FireArmCard.GetComponent<CardUI>().setImage(card.Image);
        FireArmCard.GetComponent<CardUI>().Card = card;
    }

    private void UpdateHealthUI()
    {
        HealthBar.transform.localScale = new Vector3(characterStatsManager.Stats.Health / characterStatsManager.Stats.MaxHealth, HealthBar.transform.localScale.y, HealthBar.transform.localScale.z);
    }
    private void UpdateEnduranceUI()
    {
        EnduranceBar.transform.localScale = new Vector3(characterStatsManager.Stats.Endurance / characterStatsManager.Stats.MaxEndurance, EnduranceBar.transform.localScale.y, EnduranceBar.transform.localScale.z);
    }
    private void UpdateBulletUI()
    {
        BulletsBar.transform.localScale = new Vector3(fireArmManager.fireArmStats.Ammo / fireArmManager.fireArmStats.MaxAmmo, BulletsBar.transform.localScale.y, BulletsBar.transform.localScale.z);
    }
    private void UpdateCardPointsUI()
    {
        if (HabilityCardContent == null) return;
        int i = 0;
        foreach(GameObject g in CardPointsUI)
        {
            if (characterStatsManager.Stats.CardPoints > i)
                g.GetComponent<Image>().color = ValidCard;
            else
                g.GetComponent<Image>().color = InvalidCard;
            if (characterStatsManager.Stats.CardPoints == characterStatsManager.Stats.MaxCardPoints)
                g.GetComponent<Image>().color = ValidCard;
            i += characterStatsManager.Stats.MaxCardPoints/CardPointsUI.Count;
            //Debug.Log("UpdateCardPointsUI: " + i);
            //Debug.Log("UpdateCardPointsUI: " + GetComponent<CharacterStatsManager>().Stats.CardPoints);
            //Debug.Log("UpdateCardPointsUI: " + this.name);
        }
    }
}
