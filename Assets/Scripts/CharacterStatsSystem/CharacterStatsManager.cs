using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    [SerializeField]
    private CharacterStats characterStats = new CharacterStats();
    public CharacterStats Stats { get { return characterStats; } }


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
    public void UpdateCharacterStats(CharacterStats cs, bool inverted = false)
    {
        characterStats.UpdateHealth(cs.Health);
        characterStats.UpdateEndurance(inverted ? -cs.Endurance : cs.Endurance);
        characterStats.UpdateStrength(inverted ? -cs.Strength : cs.Strength);
        characterStats.UpdateSpeed(inverted ? -cs.Speed : cs.Speed);
        characterStats.UpdateJumpForce(inverted ? -cs.JumpForce : cs.JumpForce);
        characterStats.UpdateSize(inverted ? -cs.Size : cs.Size);
        characterStats.IsWeaked(inverted ? false : cs.WeakedState);
        characterStats.IsSearching(inverted ? false : cs.SearchingState);
        characterStats.IsInmortal(inverted ? false : cs.InmortalState);
        characterStats.IsFallBack(inverted ? false : cs.FallBackState);
    }
    public IEnumerator TimedUpdateCharacterStats(CharacterStats cs, float t)
    {
        UpdateCharacterStats(cs);
        yield return new WaitForSeconds(t);
        UpdateCharacterStats(cs, true);
    }
    #endregion
}
