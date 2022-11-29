using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FireArm : MonoBehaviour
{
    [Header("Has functios")]
    public bool CanShoot = true;
    public bool AddBulletSpread = true;
    public bool AddBulletTrail = true;
    public bool AddBounceBullets = true;
    public bool AddImpactForeceBullets = true;
    //public bool AddFireArmRecoil = true;
    public bool AddCameraShake = true;
    [Header("")]
    public FireArmStats fireArmStats = new FireArmStats();

    [Header("References")]
    public Camera fpsCamera;
    public Transform BulletSpawnPoint;
    public ParticleSystem muzzleFlash;
    public ParticleSystem bulletFlash;
    public GameObject impactEffect;
    public PoolObjects BulletTrailPool;

    private float nextTimeToFire;
    private Vector3 direction;
    private List<Card> fireArmsCards;
    [SerializeField]
    private int currentFireArmCard;

    private void Start()
    {
        fireArmsCards = GetComponent<CharacterDeckManager>().GetFireArmCards(true);
        currentFireArmCard = 0;
        GetComponent<HUD_Manager>().ChangeFireArmCard(fireArmsCards[currentFireArmCard]);
        SetFireArmStats(fireArmsCards[currentFireArmCard].NewFireArmStats);
    }
    // Update is called once per frame
    void Update()
    {
        // if Player's input && Fire rate allows
        // then Shoot
        if (CanShoot && Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            // Fire Rate Timer
            nextTimeToFire = Time.time + 1f / fireArmStats.FireRate;
            // Shoot Mechanic
            if (fireArmStats.Ammo == 0)
            {
                Card temp = GetNewFireArmCard();
                GetComponent<HUD_Manager>().ChangeFireArmCard(temp);
                SetFireArmStats(temp.NewFireArmStats);
            }
            else
            {
                fireArmStats.Ammo--;
                Shoot();
            }
        }
    }
    private void SetFireArmStats(FireArmStats stats)
    {
        fireArmStats.CameraShakeForce = stats.CameraShakeForce;
        fireArmStats.FireArmRange = stats.FireArmRange;
        fireArmStats.FireRate = stats.FireRate;
        fireArmStats.FireSpreadVariance = stats.FireSpreadVariance;
        fireArmStats.ShootDelay = stats.ShootDelay;
        fireArmStats.Ammo = stats.Ammo;
        fireArmStats.BulletDamage = stats.BulletDamage;
        fireArmStats.BulletSpeed = stats.BulletSpeed;
        fireArmStats.BounceDistance = stats.BounceDistance;
        fireArmStats.ImpactForce = stats.ImpactForce;
    }
    private Card GetNewFireArmCard()
    {
        if (currentFireArmCard >= fireArmsCards.Count - 1)
        {
            fireArmsCards = GetComponent<CharacterDeckManager>().GetFireArmCards(true);
            return fireArmsCards[currentFireArmCard = 0];
        }
        else
            return fireArmsCards[++currentFireArmCard];
    }
    #region Methods
    private Vector3 GetSpreadedDirection()
    {
        Vector3 direction = fpsCamera.transform.forward;

        if (AddBulletSpread)
        {
            direction += new Vector3(
                Random.Range(-fireArmStats.FireSpreadVariance.x, fireArmStats.FireSpreadVariance.x),
                Random.Range(-fireArmStats.FireSpreadVariance.y, fireArmStats.FireSpreadVariance.y),
                Random.Range(-fireArmStats.FireSpreadVariance.z, fireArmStats.FireSpreadVariance.z)
            );

            direction.Normalize();
        }

        return direction;
    }
    private void SetDamage(CharacterStatsManager target)
    {
        if (target != null)
        {
            target.TakeDamage(fireArmStats.BulletDamage);
        }

    }
    private void SetPush(Rigidbody enemyRB, Vector3 normal)
    {
        if (AddImpactForeceBullets && enemyRB != null)
            enemyRB.AddForce(-normal * fireArmStats.ImpactForce);
    }
    #endregion
    #region Mechanic
    /// <summary>
    /// Shoot Mechanic
    /// </summary>
    private void Shoot()
    {
        // Local Vars
        /// void ray that will contain RayCast's Values
        RaycastHit hit;

        // Particles play
        muzzleFlash.Play();
        bulletFlash.Play();
        // If its allow Camera Shake movment hten apply trauma movment
        if (AddCameraShake) fpsCamera.GetComponent<CameraShake>().Trauma = fireArmStats.CameraShakeForce;

        // 
        //-----------------
        // Ray cast direction Builder //
        //-----------------
        //
        direction = GetSpreadedDirection();
        // 
        //-----------------
        // Ray cast detect Collision //
        //-----------------
        //
        Ray ray = new Ray(fpsCamera.transform.position, direction);
        bool rayCollision = Physics.Raycast(ray, out hit, fireArmStats.FireArmRange);
        {
            // 
            //-----------------
            // check if we have to //
            // use a trail render //
            //-----------------
            //
            // Add bullet Trail Render
            if (AddBulletTrail)
            {
                //TrailRenderer trail = Instantiate(BulletTrailPool, BulletSpawnPoint.position, Quaternion.identity);
                TrailRenderer trail = BulletTrailPool.GetObject().GetComponent<TrailRenderer>();
                trail.transform.position = BulletSpawnPoint.position;
                trail.transform.rotation = BulletSpawnPoint.rotation;
                if (hit.transform?.GetComponent<CharacterStatsManager>() != null)
                {
                    StartCoroutine(BulletManager(trail, hit.point, hit.normal, 0, true));
                }
                else
                {
                    StartCoroutine(BulletManager(trail, rayCollision ? hit.point : ray.GetPoint(fireArmStats.FireArmRange), hit.normal, fireArmStats.BounceDistance, rayCollision));
                }
            }
            else
            {
                // 
                //-----------------
                // impact effect Manager //
                //-----------------
                //
                GameObject impactEffectOBJ = Instantiate(impactEffect, rayCollision ? hit.point : ray.GetPoint(fireArmStats.FireArmRange), Quaternion.LookRotation(hit.normal));

            }
            // set damage to enemy
            SetDamage(hit.transform?.GetComponent<CharacterStatsManager>());
            // add push force to enemy
            SetPush(hit.rigidbody, hit.normal);
        }
    }
    #endregion
    #region controllers
    private IEnumerator BulletManager(TrailRenderer Trail, Vector3 HitPoint, Vector3 HitNormal, float BounceDistance, bool MadeImpact)
    {
        //
        //-----------------
        // trail/bullet movment //
        //-----------------
        // get and set startPosition to calculate movment in the future
        Vector3 startPosition = Trail.transform.position;
        // Calculate distace of spawn to hit point
        float distance = Vector3.Distance(Trail.transform.position, HitPoint);
        // remining distance to arive
        float remainingDistance = distance;
        // checking arrive
        while (remainingDistance > 0)
        {
            // move the taril renderer from spawn position to the hit point over a Time <t>
            // the float t is the position over one point and other (0 to 1)
            // In this case the time is calculated by dividing the remaining distance and the initial distance
            // whose result is a value between 0 and 1
            Trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (remainingDistance / distance));
            // calculation of the distance traveled in each frame which is given by the speed of the vale and the time between frames.
            remainingDistance -= fireArmStats.BulletSpeed * Time.deltaTime;
            // yield return null tells the Uinty's coroutine scheduler to wait for the next frame and continue execution from this line
            // which produces a gradual movement effect
            yield return null;
        }
        // Make sure that arrive to evade bugs
        Trail.transform.position = HitPoint;
        //
        //-----------------
        // Impact Manager //
        //-----------------
        // After the bullet arrives, do the Impact logic, if previously detected
        // Checks if we said that bullet impacted
        ////// then check if can bounce, after instantiate a impact efect
        // Else just skip Impact logic and destroy the bullet
        if (MadeImpact)
        {
            //
            //-----------------
            // Bounce Function //
            //-----------------
            //
            // if we said that can Bounce and the Bounce distance of the trajectory does not reach the maximum
            ////// Then calculate a new Bounced direction vector3 and checks Ray collisions logics
            if (AddBounceBullets && BounceDistance > 0)
            {
                Vector3 bounceDirection = Vector3.Reflect(direction, HitNormal);
                bool wasCollision = Physics.Raycast(HitPoint, bounceDirection, out RaycastHit bounceHit, BounceDistance);
                //
                //-----------------
                // ray ckecks //
                //-----------------
                //
                // Comprueba si la trayectoria de la bala chocará con un player
                ////// entonces actualiza PlayerStats y Actualiza movimiento de la bala en BulletManager
                // Comprueba si la trayectoria de la bala choca con algo
                ///// Enviamos los datos de la trayectoria y restamos la BounceDistance
                // comprueba si la trayectoria de la bala no choca con nada
                ////// Enviamos a BulletManager una actualizacion con la trayectoria y especificamos que no hubo colisión
                //
                //-----------------
                // Submit new movement processed //
                // Specifying with contact case//
                //-----------------
                //
                if (wasCollision)
                {
                    //
                    //-----------------
                    // if Collision was with player //
                    //-----------------
                    //
                    if (bounceHit.transform?.GetComponent<CharacterStatsManager>())
                    {
                        SetDamage(bounceHit.transform?.GetComponent<CharacterStatsManager>());
                        SetPush(bounceHit.rigidbody, bounceHit.normal);
                        yield return StartCoroutine(BulletManager(
                            Trail,
                            bounceHit.point,
                            bounceHit.normal,
                            0,
                            true
                            ));
                    }
                    yield return StartCoroutine(BulletManager(
                        Trail,
                        bounceHit.point,
                        bounceHit.normal,
                        BounceDistance - Vector3.Distance(bounceHit.point, HitPoint),
                        true
                    ));
                }
                //
                //-----------------
                // Submit new movement processed //
                // Specifying without contact case//
                //-----------------
                //
                else
                {
                    yield return StartCoroutine(BulletManager(Trail, HitPoint + bounceDirection * BounceDistance, Vector3.zero, 0, false));
                }
            }
            //
            //-----------------
            // Impact Effect Manager //
            //-----------------
            //
            // Instanciate impactEfect
            Instantiate(impactEffect, HitPoint, Quaternion.LookRotation(HitNormal));
        }
        //
        //-----------------
        // Destroy Function //
        //-----------------
        //
        // Destroys bullet when its run ends
        //Destroy(Trail.gameObject, Trail.time);
        yield return new WaitForSeconds(Trail.time);
        Trail.gameObject.SetActive(false);
    }
    #endregion
    //--------------------------- Draw
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(fpsCamera.transform.position, (direction != new Vector3() ? direction : fpsCamera.transform.forward) * fireArmStats.FireArmRange);
    }
}
