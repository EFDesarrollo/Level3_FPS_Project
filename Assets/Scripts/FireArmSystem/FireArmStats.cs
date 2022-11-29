using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FireArmStats
{
    [Header("Fire Arm Stats")]
    [SerializeField] private float cameraShakeForce = .05f;
    [SerializeField] private float fireArmRange = 30f;
    [SerializeField] private float fireRate = 15f; // shoot delay?
    [SerializeField] private Vector3 fireSpreadVariance = new Vector3(.1f, .1f, .1f);
    [SerializeField] private float shootDelay = .5f; // fire rate?
    [Header("Bullet Stats")]
    [SerializeField] private float ammo = 50;
    [SerializeField] private float bulletDamage = 10f;
    [SerializeField] private float bulletSpeed = 100f;
    [SerializeField] private float bounceDistance = 10f;
    [SerializeField] private float impactForce = 30f;

    public float CameraShakeForce { get { return cameraShakeForce; } set { cameraShakeForce = value; } }
    public float FireArmRange { get { return fireArmRange; } set { fireArmRange = value; } }
    public float FireRate { get { return fireRate; } set { fireRate = value; } }
    public Vector3 FireSpreadVariance { get { return fireSpreadVariance; } set { fireSpreadVariance = value; } }
    public float ShootDelay { get { return shootDelay; } set { shootDelay = value; } }
    public float Ammo { get { return ammo; } set { ammo = value; } }
    public float BulletDamage { get { return bulletDamage; } set { bulletDamage = value; } }
    public float BulletSpeed { get { return bulletSpeed; } set { bulletSpeed = value; } }
    public float BounceDistance { get { return bounceDistance; } set { bounceDistance = value; } }
    public float ImpactForce { get { return impactForce; } set { impactForce = value; } }
}
