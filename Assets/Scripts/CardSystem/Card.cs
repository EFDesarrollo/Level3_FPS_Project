using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Card
{
    [SerializeField]
    private Sprite image;
    [SerializeField]
    private CharacterStats newCharacterStats;
    [SerializeField]
    private int type;
    [SerializeField]
    private int priority;
    [SerializeField]
    private float activeTime;
    [SerializeField]
    private float refreshTime;

    public Sprite Image { get { return image; } }
    public CharacterStats NewCharacterStats { get { return newCharacterStats; } }
    public int Type { get { return type; } }
    public int Priority { get { return priority; } }
    public float ActiveTime { get { return activeTime; } }
    public float RefreshTime { get { return refreshTime; } }
}
