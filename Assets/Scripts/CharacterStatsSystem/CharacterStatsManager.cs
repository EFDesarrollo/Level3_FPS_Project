using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    [SerializeField]
    private CharacterStats characterStats = new CharacterStats();

    
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
        characterStats.UpdateEndurance(-2);
        //// multiply the value by -1 to be able to subtract it from life
        characterStats.UpdateHealth(damage * -1);

    }
    public void SetCharacterStats(CharacterStats cs)
    {
        characterStats = cs;
    }
    public void UpdateCharacterStats(CharacterStats cs)
    {
        characterStats.UpdateHealth(cs.Health);
        characterStats.UpdateEndurance(cs.Endurance);
        characterStats.UpdateStrength(cs.Strength);
        characterStats.UpdateSpeed(cs.Speed);
        characterStats.UpdateJumpForce(cs.JumpForce);
        characterStats.UpdateSize(cs.Size);
        characterStats.IsWeaked(cs.WeakedState);
        characterStats.IsSearching(cs.SearchingState);
        characterStats.IsInmortal(cs.InmortalState);
        characterStats.IsFallBack(cs.FallBackState);
    }
    #endregion
}
