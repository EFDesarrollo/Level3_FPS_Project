using System.Collections;
using System.Collections.Generic;
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
    [Header("Fire Arm Stats")]
    public float CameraShakeForce = .05f;
    public float fireArmRange = 100f;
    public float fireRate = 15f; // shoot delay?
    public Vector3 fireSpreadVariance = new Vector3(.1f,.1f,.1f);
    public float ShootDelay = .5f; // fire rate?
    [Header("Bullet Stats")]
    public float bulletDamage = 10f;
    public float bulletSpeed = 100f;
    public float bounceDistance = 10f;
    public float impactForce = 30f;
    [Header("References")]
    public Camera fpsCamera;
    public Transform BulletSpawnPoint;
    public ParticleSystem muzzleFlash;
    public ParticleSystem bulletFlash;
    public GameObject impactEffect;
    public TrailRenderer BulletTrail;

    private float nextTimeToFire;
    private Vector3 direction;
    // Update is called once per frame
    void Update()
    {
        // if Player's input && Fire rate allows
        // then Shoot
        if (CanShoot && Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            // Fire Rate Timer
            nextTimeToFire = Time.time + 1f/fireRate;
            // Shoot Mechanic
            Shoot();
        }
    }
    /// <summary>
    /// Shoot Mechanic
    /// </summary>
    private void Shoot()
    {
        // void ray that will contain RayCast's Values
        RaycastHit hit;
        // Particles play
        muzzleFlash.Play();
        bulletFlash.Play();
        // Get s
        direction = GetSpreadedDirection();
        // If its allow Camera Shake movment hten apply trauma movment
        if (AddCameraShake) fpsCamera.GetComponent<CameraShake>().Trauma = CameraShakeForce;
        if (Physics.Raycast(fpsCamera.transform.position, direction, out hit, fireArmRange))
        {
            // Add bullet Trail Render
            if (AddBulletTrail)
            {
                TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);
                StartCoroutine(BulletManager(trail, hit.point, hit.normal, bounceDistance, true));
            }
            else
            {
                SetDamage(hit.transform.GetComponent<PlayerStats>(), hit.rigidbody, hit.normal);
                
                GameObject impactEffectOBJ = Instantiate(impactEffect, hit.point,Quaternion.LookRotation(hit.normal));

            }
        }
    }
    private Vector3 GetSpreadedDirection()
    {
        Vector3 direction = fpsCamera.transform.forward;

        if (AddBulletSpread)
        {
            direction += new Vector3(
                Random.Range(-fireSpreadVariance.x, fireSpreadVariance.x),
                Random.Range(-fireSpreadVariance.y, fireSpreadVariance.y),
                Random.Range(-fireSpreadVariance.z, fireSpreadVariance.z)
            );

            direction.Normalize();
        }

        return direction;
    }
    private void SetDamage(PlayerStats target, Rigidbody enemyRB, Vector3 normal)
    {
        //PlayerStats target = hit.transform.GetComponent<PlayerStats>();
        if (target != null)
        {
            target.TakeDamage(bulletDamage);
        }
        if (AddImpactForeceBullets && enemyRB != null)
            enemyRB.AddForce(-normal * impactForce);
    }
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
            remainingDistance -= bulletSpeed * Time.deltaTime;
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
                if (bounceHit.transform.GetComponent<PlayerStats>() != null)
                {
                    SetDamage(bounceHit.transform.GetComponent<PlayerStats>(), bounceHit.rigidbody, bounceHit.normal);
                    yield return StartCoroutine(BulletManager(
                        Trail,
                        bounceHit.point,
                        bounceHit.normal,
                        0,
                        true
                        ));
                }
                //
                //-----------------
                // Submit new movement processed //
                // Specifying with contact case//
                //-----------------
                //
                else if (wasCollision)
                {
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
                    Debug.Log("hitpoint of bounce: " + HitPoint);
                    yield return StartCoroutine(BulletManager(
                        Trail,
                        HitPoint + bounceDirection * BounceDistance,
                        Vector3.zero,
                        0,
                        false
                    ));
                }
            }
            // Instanciate impactEfect
            Instantiate(impactEffect, HitPoint, Quaternion.LookRotation(HitNormal));
        }
        //
        //-----------------
        // Destroy Function //
        //-----------------
        //
        // Destroys bullet when its run ends
        Destroy(Trail.gameObject, Trail.time);
    }
    

    //--------------------------- Draw
    private void OnDrawGizmos()
    {
        Debug.DrawRay(fpsCamera.transform.position, fpsCamera.transform.forward, Color.green, fireArmRange);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(fpsCamera.transform.position, (direction != new Vector3() ? direction : fpsCamera.transform.forward) * fireArmRange);
    }
}
