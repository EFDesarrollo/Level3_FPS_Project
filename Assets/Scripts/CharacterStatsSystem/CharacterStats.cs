using System;
using UnityEngine;
[Serializable]
public class CharacterStats
{
    /// <summary>
    /// variable indicating team membership
    /// </summary>
    [Header("Team")]
    public int Team;
    /// <summary>
    /// variable indicating the current lifetime of the character
    /// </summary>
    [Header("Stats")]
    public float Health;
    /// <summary>
    /// variable that indicates the maximum life of the character
    /// </summary>
    public float MaxHealth;
    /// <summary>
    /// Variable belonging to the retry functions
    /// </summary>
    public int RetryPoints, MaxRetryPoints;
    /// <summary>
    /// Variable belonging to the damage reduction mechanic
    /// </summary>
    public float Endurance, MaxEndurance;
    /// <summary>
    /// Variable belonging to the damage increase mechanic
    /// </summary>
    public float Strength=1, MaxStrength=2;
    /// <summary>
    /// Variable belongs to the mechanics of the size of the character
    /// </summary>
    public float Size=1, MaxSize=0.5f, MinSize=1.5f;
    /// <summary>
    /// Variable belonging to the mechanics of character movement
    /// </summary>
    public float Speed, MaxSpeed;
    /// <summary>
    /// Variable belonging to the mechanics of character movement
    /// </summary>
    public float JumpForce, MaxJumpForce, MinJumpForce;
    /// <summary>
    /// Variable belongs to the character's weakness mechanic
    /// </summary>
    [Header("Estates")]
    public bool WeakedState;
    /// <summary>
    /// Variable belonging to the immortality mechanic of the character
    /// </summary>
    public bool InmortalState;
    /// <summary>
    /// Variable belongs to the character search mechanism
    /// </summary>
    public bool SearchingState;
    /// <summary>
    /// Variable belonging to the teleportation mechanics of the character
    /// </summary>
    public bool FallBackState;
}
