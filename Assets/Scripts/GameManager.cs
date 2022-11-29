using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Actual Player Deck
    /// </summary>
    [SerializeField]
    private Deck deck;
    public Deck Deck { get { return deck; } set { deck = value; } }

    /*
    private void Start()
    {
        Debug.Log("GameManager: Getting component Deck");
        deck = GetComponent<Deck>();
    }
    */
}
