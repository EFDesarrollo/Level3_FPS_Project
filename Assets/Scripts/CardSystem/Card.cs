using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Card
{
    [SerializeField] private Sprite image;
    [SerializeField] private CharacterStats newCharacterStats;
    [SerializeField] private FireArmStats newFireArmStats;
    [SerializeField] private int type;
    [SerializeField] private int priority;
    [SerializeField] private float activeTime;
    [SerializeField] private int value;

    public Sprite Image { get { return image; } }
    public CharacterStats NewCharacterStats { get { return newCharacterStats; } }
    public FireArmStats NewFireArmStats { get { return newFireArmStats; } }
    public int Type { get { return type; } }
    public int Priority { get { return priority; } set { priority = value; } }
    public float ActiveTime { get { return activeTime; } }
    public int Value { get { return value; } }
}
