using System;
using UnityEngine;
[Serializable]
public class CharacterStats
{
    [Header("Team")]
    [SerializeField]
    private int team;
    /// <summary>
    /// variable indicating team membership
    /// </summary>
    public int Team { get { return team; } set { team = value; } }
    [Header("Stats")]
    /// <summary>
    /// Variable belonging to the retry functions
    /// </summary>
    public int RetryPoints;
    /// <summary>
    /// Variable belonging to the retry functions
    /// </summary>
    public int MaxRetryPoints;
    /// <summary>
    /// Variable belonging to the Hability card functions
    /// </summary>
    public int CardPoints, MaxCardPoints;
    /// <summary>
    /// variable indicating the current lifetime of the character
    /// </summary>
    public float Health, MaxHealth;
    /// <summary>
    /// Variable belonging to the damage reduction mechanic
    /// </summary>
    public float Endurance, MaxEndurance;
    /// <summary>
    /// Variable belonging to the damage increase mechanic
    /// </summary>
    public float Strength, MaxStrength;
    /// <summary>
    /// Variable belongs to the mechanics of the size of the character
    /// </summary>
    public float Size, MaxSize, MinSize;
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

    #region methods
    /// <summary>
    /// This method updates the team variable in the referenced <see cref="CharacterStats"/> object,
    /// as long as it is greater than 0.
    /// </summary>
    /// <param name="value"></param>
    public void UpdateTeam(int value)
    {
        if (value < 0)
            Team = value;
    }
    /// <summary>
    /// Update the value of the Health stat by adding the <paramref name="value"/> parameter to it.
    /// <para>This function ensures that the value of <see cref="Health"/> is kept between the ranges <see langword="0"/> and <see cref="MaxHealth"/></para>
    /// </summary>
    /// <param name="value">float value used in current health calculation</param>
    public void UpdateHealth(float value)
    {
        // calculate and set new value to Health
        Health += value;
        // ensures that it stays within the value ranges
        if (Health + value >= MaxHealth)
            Health = MaxHealth;
        if (Health + value <= 0)
            Health = 0;
    }
    /// <summary>
    /// Update the value of the Endurance stat by adding the <paramref name="value"/> parameter to it
    /// <para>This function ensures that the value of <see cref="Endurance"/> is kept between the ranges <see langword="0"/> and <see cref="MaxEndurance"/></para>
    /// </summary>
    /// <param name="value">float value used in current Endurance calculation</param>
    public void UpdateEndurance(float value)
    {
        // calculate and set new value to Endurance
        Endurance += value;
        // ensures that it stays within the value ranges
        if (Endurance >= MaxEndurance)
            Endurance = MaxEndurance;
        if (Endurance <= 0)
            Endurance = 0;
    }
    /// <summary>
    /// Update the value of the RetryPoints stat by adding the <paramref name="value"/> parameter to it
    /// <para>This function ensures that the value of <see cref="RetryPoints"/> is kept between the ranges <see langword="0"/> and <see cref="MaxRetryPoints"/></para>
    /// </summary>
    /// <param name="value">float value used in current Endurance calculation</param>
    public void UpdateRetryPoints(int value)
    {
        // calculate and set new value to RetryPoints
        RetryPoints += value;
        // ensures that it stays within the value ranges
        if (RetryPoints >= MaxRetryPoints)
            RetryPoints = MaxRetryPoints;
        if (RetryPoints <= 0)
            RetryPoints = 0;
    }
    /// <summary>
    /// Update the value of the retryPoints by adding the <paramref name="value"/> parameter to it
    /// <para>This function ensures that the value of <see cref="Strength"/> is kept between the ranges <see langword="0" and/> and <see cref="MaxStrength"/></para>
    /// </summary>
    /// <param name="value">float value used in current Strength calculation</param>
    public void UpdateStrength(float value)
    {
        // calculate and set new value to Strength
        Strength += value;
        // Ensure that it stays within the value ranges
        if (Strength >= MaxStrength)
            Strength = MaxStrength;
        if (Strength <= 0)
            Strength = 0;

    }
    /// <summary>
    /// Update the value of the Speed by adding the <paramref name="value"/> parameter to it
    /// <para>This function ensures that the value of <see cref="Speed"/> is kept between the ranges <see langword="0" and/> and <see cref="MaxSpeed"/></para>
    /// </summary>
    /// <param name="value">float value used in current Speed calculation</param>
    public void UpdateSpeed(float value)
    {
        // Calculate and set new value to Speed
        Speed += value;
        // Ensure that it stays within the value ranges
        if (Speed >= MaxSpeed)
            Speed = MaxSpeed;
        if (Speed <= 0)
            Speed = 0;
    }
    /// <summary>
    /// Update the value of the JumpForce by adding the <paramref name="value"/> parameter to it
    /// <para>This function ensures that the value of <see cref="JumpForce"/> is kept between the ranges <see langword="0" and/> and <see cref="MaxJumpForce"/></para>
    /// </summary>
    /// <param name="value">float value used in current Speed calculation</param>
    public void UpdateJumpForce(float value)
    {
        // Calculate and set new value to JumpForce
        JumpForce += value;
        // Ensure that it stays within the value ranges
        if (JumpForce >= MaxJumpForce)
            JumpForce = MaxJumpForce;
        if (JumpForce <= 0)
            JumpForce = 0;
    }
    /// <summary>
    /// Update the value of the Size by adding the <paramref name="value"/> parameter to it
    /// <para>This function ensures that the value of <see cref="Size"/> is kept between the ranges <see langword="0" and/> and <see cref="MaxSize"/></para>
    /// </summary>
    /// <param name="value">float value used in current Speed calculation</param>
    public void UpdateSize(float value)
    {
        Size += value;
        if (Size + value >= MaxSize)
            Size = MaxSize;
        if (Size + value <= MinSize)
            Size = MinSize;
    }

    public void IsWeaked(bool value)
    {
        WeakedState = value;
    }
    public void IsInmortal(bool value)
    {
        InmortalState = value;
    }
    public void IsSearching(bool value)
    {
        SearchingState = value;
    }
    public void IsFallBack(bool value)
    {
        FallBackState = value;
    }
    #endregion
}
