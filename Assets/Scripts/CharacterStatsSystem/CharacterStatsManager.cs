using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    [SerializeField]
    private CharacterStats characterStats = new CharacterStats();

    #region methods
    /// <summary>
    /// This method updates the team variable in the referenced <see cref="CharacterStats"/> object,
    /// as long as it is greater than 0.
    /// </summary>
    /// <param name="value"></param>
    public void UpdateTeam(int value)
    {
        if (value < 0)
            characterStats.Team = value;
    }
    /// <summary>
    /// Update the value of the Health stat by adding the <paramref name="value"/> parameter to it.
    /// <para>This function ensures that the value of <see cref="CharacterStats.Health"/> is kept between the ranges <see langword="0"/> and <see cref="CharacterStats.MaxHealth"/></para>
    /// </summary>
    /// <param name="value">float value used in current health calculation</param>
    public void UpdateHealth(float value)
    {
        // calculate and set new value to Health
        characterStats.Health += value;
        // ensures that it stays within the value ranges
        if (characterStats.Health + value >= characterStats.MaxHealth)
            characterStats.Health = characterStats.MaxHealth;
        if (characterStats.Health + value <= 0)
            characterStats.Health = 0;
    }
    /// <summary>
    /// Update the value of the Endurance stat by adding the <paramref name="value"/> parameter to it
    /// <para>This function ensures that the value of <see cref="CharacterStats.Endurance"/> is kept between the ranges <see langword="0"/> and <see cref="CharacterStats.MaxEndurance"/></para>
    /// </summary>
    /// <param name="value">float value used in current Endurance calculation</param>
    public void UpdateEndurance(float value)
    {
        // calculate and set new value to Endurance
        characterStats.Endurance += value;
        // ensures that it stays within the value ranges
        if (characterStats.Endurance >= characterStats.MaxEndurance)
            characterStats.Endurance = characterStats.MaxEndurance;
        if (characterStats.Endurance <= 0)
            characterStats.Endurance = 0;
    }
    /// <summary>
    /// Update the value of the RetryPoints stat by adding the <paramref name="value"/> parameter to it
    /// <para>This function ensures that the value of <see cref="CharacterStats.RetryPoints"/> is kept between the ranges <see langword="0"/> and <see cref="CharacterStats.MaxRetryPoints"/></para>
    /// </summary>
    /// <param name="value">float value used in current Endurance calculation</param>
    public void UpdateRetryPoints(int value)
    {
        // calculate and set new value to RetryPoints
        characterStats.RetryPoints += value;
        // ensures that it stays within the value ranges
        if (characterStats.RetryPoints >= characterStats.MaxRetryPoints)
            characterStats.RetryPoints = characterStats.MaxRetryPoints;
        if (characterStats.RetryPoints <= 0)
            characterStats.RetryPoints = 0;
    }
    /// <summary>
    /// Update the value of the retryPoints by adding the <paramref name="value"/> parameter to it
    /// <para>This function ensures that the value of <see cref="CharacterStats.Strength"/> is kept between the ranges <see langword="0" and/> and <see cref="CharacterStats.MaxStrength"/></para>
    /// </summary>
    /// <param name="value">float value used in current Strength calculation</param>
    public void UpdateStrength(float value)
    {
        // calculate and set new value to Strength
        characterStats.Strength += value;
        // Ensure that it stays within the value ranges
        if (characterStats.Strength >= characterStats.MaxStrength)
            characterStats.Strength = characterStats.MaxStrength;
        if (characterStats.Strength <= 0)
            characterStats.Strength = 0;

    }
    /// <summary>
    /// Update the value of the Speed by adding the <paramref name="value"/> parameter to it
    /// <para>This function ensures that the value of <see cref="CharacterStats.Speed"/> is kept between the ranges <see langword="0" and/> and <see cref="CharacterStats.MaxSpeed"/></para>
    /// </summary>
    /// <param name="value">float value used in current Speed calculation</param>
    public void UpdateSpeed(float value)
    {
        // Calculate and set new value to Speed
        characterStats.Speed += value;
        // Ensure that it stays within the value ranges
        if (characterStats.Speed >= characterStats.MaxSpeed)
            characterStats.Speed = characterStats.MaxSpeed;
        if (characterStats.Speed <= 0)
            characterStats.Speed = 0;
    }
    /// <summary>
    /// Update the value of the JumpForce by adding the <paramref name="value"/> parameter to it
    /// <para>This function ensures that the value of <see cref="CharacterStats.JumpForce"/> is kept between the ranges <see langword="0" and/> and <see cref="CharacterStats.MaxJumpForce"/></para>
    /// </summary>
    /// <param name="value">float value used in current Speed calculation</param>
    public void UpdateJumpForce(float value)
    {
        // Calculate and set new value to JumpForce
        characterStats.JumpForce += value;
        // Ensure that it stays within the value ranges
        if (characterStats.JumpForce >= characterStats.MaxJumpForce)
            characterStats.JumpForce = characterStats.MaxJumpForce;
        if (characterStats.JumpForce <= 0)
            characterStats.JumpForce = 0;
    }
    /// <summary>
    /// Update the value of the Size by adding the <paramref name="value"/> parameter to it
    /// <para>This function ensures that the value of <see cref="CharacterStats.Size"/> is kept between the ranges <see langword="0" and/> and <see cref="CharacterStats.MaxSize"/></para>
    /// </summary>
    /// <param name="value">float value used in current Speed calculation</param>
    public void UpdateSize(float value)
    {
        characterStats.Size += value;
        if (characterStats.Size + value >= characterStats.MaxSize)
            characterStats.Size = characterStats.MaxSize;
        if (characterStats.Size + value <= characterStats.MinSize)
            characterStats.Size = characterStats.MinSize;
    }

    public void IsWeaked(bool value)
    {
        characterStats.WeakedState = value;
    }
    public void IsInmortal(bool value)
    {
        characterStats.InmortalState = value;
    }
    public void IsSearching(bool value)
    {
        characterStats.SearchingState = value;
    }
    public void IsFallBack(bool value)
    {
        characterStats.FallBackState = value;
    }
    #endregion
    #region Mechanics
    /// <summary>
    /// This method calculates the damage received based on the <paramref name="damage"/> and <see cref="CharacterStats.Endurance"/> parameters.
    /// </summary>
    /// <param name="damage">value of damage recived</param>
    public void TakeDamage(float damage)
    {
        // Calculo de daño
        damage -= (characterStats.Endurance / characterStats.MaxEndurance) * damage;
        // Actualización de Stats
        //// reduce Endurance by a constant value
        UpdateEndurance(-2);
        //// multiply the value by -1 to be able to subtract it from life
        UpdateHealth(damage * -1);

    }
    public void SetCharacterStats(CharacterStats cs)
    {
        characterStats = cs;
    }
    public void UpdateCharacterStats(CharacterStats cs)
    {
        UpdateHealth(cs.Health);
        UpdateEndurance(cs.Endurance);
        UpdateStrength(cs.Strength);
        UpdateSpeed(cs.Speed);
        UpdateJumpForce(cs.JumpForce);
        UpdateSize(cs.Size);
        IsWeaked(cs.WeakedState);
        IsSearching(cs.SearchingState);
        IsInmortal(cs.InmortalState);
        IsFallBack(cs.FallBackState);
    }
    #endregion
}
