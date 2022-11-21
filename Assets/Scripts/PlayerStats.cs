using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float endurance;
    public float maxEndurance;
    public float speed;
    public float maxSpeed;
    public float size;
    public float maxSize, minSize;

    public void TakeDamage(float value)
    {
        ModifyEndurance(-2);
        float damage = (endurance / maxEndurance) * value - value;
        ModifyHealth(damage);
            
    }
    /// <summary>
    /// Modify the Stat Health
    /// </summary>
    /// <param name="value"></param>
    public void ModifyHealth(float value)
    {
        health += value;
        if (health+value >= maxHealth)
            health = maxHealth;
        if (health+value <= 0)
            health = 0;
    }
    /// <summary>
    /// Modify the Stat Endurance
    /// </summary>
    /// <param name="value"></param>
    public void ModifyEndurance(float value)
    {
        endurance += value;
        if (endurance + value >= maxEndurance)
            endurance = maxEndurance;
        if (endurance+value <= 0)
            endurance = 0;
    }
    /// <summary>
    /// Modify the stat Speed
    /// </summary>
    /// <param name="value"></param>
    public void ModifySpeed(float value)
    {
        speed += value;
        if (speed+value >= maxSpeed)
            speed = maxSpeed;
        if (speed+value <= 0)
            speed = 0;
    }
    /// <summary>
    /// Modify the stat Size
    /// </summary>
    /// <param name="value"></param>
    public void ModifySize(float value)
    {
        size += value;
        if (size+value >= maxSize)
            size = maxSize;
        if (size+value <= minSize)
            size = minSize;
    }
}
