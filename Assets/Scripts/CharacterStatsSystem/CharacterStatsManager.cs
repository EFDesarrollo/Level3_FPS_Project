using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    [SerializeField]
    private CharacterStats characterStats = new CharacterStats();
    public CharacterStats Stats { get { return characterStats; } }
    [Header("Card Points")]
    public bool GetPointsByTime = true;
    public float SecondsBetweenPoints = 60;
    public int PointsByTime = 1;
    private float NextTimePoint;
    private PlayerController playerMovmentController;
    private EnemyMovment enemyMovmentController;

    private void Start()
    {
        playerMovmentController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (Time.time >= NextTimePoint)
        {
            NextTimePoint = Time.time+SecondsBetweenPoints;
            AddCardPoints(PointsByTime);
        }
        transform.localScale = new Vector3(Stats.Size, Stats.Size, Stats.Size);
        if (playerMovmentController != null)
        {
            playerMovmentController.velocidad = characterStats.Speed;
            playerMovmentController.fuerzaSalto = characterStats.JumpForce;
        }
        if (enemyMovmentController != null) enemyMovmentController.speed = characterStats.Speed;
    }


    #region Mechanics
    /// <summary>
    /// This method calculates the damage received based on the <paramref name="damage"/> and <see cref="CharacterStats.Endurance"/> parameters.
    /// </summary>
    /// <param name="damage">value of damage recived</param>
    public float TakeDamage(float damage)
    {
        if (characterStats.InmortalState)
            return 0;
        // Calculo de daño
        if (characterStats.Endurance > 0)
        {
        damage -= (characterStats.Endurance / characterStats.MaxEndurance) * damage;
            // Actualización de Stats
            //// reduce Endurance by a constant value
            characterStats.UpdateEndurance(damage==0? -1: -damage/5);
            //// multiply the value by -1 to be able to subtract it from life
            characterStats.UpdateHealth(-damage / 20);

        }
        else
        {
            characterStats.UpdateHealth(-damage);
        }
        if (this.tag == "Enemy" && characterStats.Health == 0) Destroy(gameObject,0.1f);
        return damage;

    }
    public void SubstractCardPoints(int value)
    {
            characterStats.UpdateCardPoints(-value);
    }
    public void AddCardPoints(int value)
    {
        characterStats.UpdateCardPoints(value);
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
        if (cs.Endurance + characterStats.Endurance > characterStats.MaxEndurance)
            cs.Endurance = characterStats.MaxEndurance-characterStats.Endurance;
        if (cs.Strength + characterStats.Strength > characterStats.MaxStrength)
            cs.Strength = characterStats.MaxStrength-characterStats.Strength;
        if (cs.Speed + characterStats.Speed > characterStats.MaxSpeed)
            cs.Speed = characterStats.MaxSpeed-characterStats.Speed;
        if (cs.JumpForce + characterStats.JumpForce > characterStats.MaxJumpForce)
            cs.JumpForce = characterStats.MaxJumpForce-characterStats.JumpForce;
        if (cs.Size + characterStats.Size > characterStats.MaxSize)
            cs.Size = characterStats.MaxSize-characterStats.Size;
        //------------------------------------------------------------------------
        if (cs.Endurance + characterStats.Endurance < 0)
            cs.Endurance = -characterStats.Endurance;
        if (cs.Strength + characterStats.Strength < 0)
            cs.Strength = -characterStats.Strength;
        if (cs.Speed + characterStats.Speed < 0)
            cs.Speed = -characterStats.Speed;
        if (cs.JumpForce + characterStats.JumpForce < 0)
            cs.JumpForce = -characterStats.JumpForce;
        if (cs.Size + characterStats.Size < 0)
            cs.Size = -characterStats.Size;
        UpdateCharacterStats(cs);
        yield return new WaitForSeconds(t);
        UpdateCharacterStats(cs, true);
    }
    #endregion
}
