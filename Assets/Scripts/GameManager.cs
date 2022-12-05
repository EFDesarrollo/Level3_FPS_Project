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
    [SerializeField]
    private int team = 0;
    public Deck Deck { get { return deck; } set { deck = value; } }
    public int Team { get { return team; } set { team = value; } }

    /*
    private void Start()
    {
        Debug.Log("GameManager: Getting component Deck");
        deck = GetComponent<Deck>();
    }
    */
}
