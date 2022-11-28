using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Actual Player Deck
    /// </summary>
    private Deck deck;

    public Deck Deck { get { return deck; } set { deck = value; } }
}
