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
    public bool AddInpactForeceBullets = true;
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
    public float inpactForce = 30f;
    [Header("References")]
    public Camera fpsCamera;
    public Transform BulletSpawnPoint;
    public ParticleSystem muzzleFlash;
    public ParticleSystem bulletFlash;
    public GameObject inpactEffect;
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
                StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, bounceDistance, true));
            }
            else
            {
                SetDamage(hit);
                
                GameObject inpactEffectOBJ = Instantiate(inpactEffect, hit.point,Quaternion.LookRotation(hit.normal));

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
    private void SetDamage(RaycastHit hit)
    {
        PlayerStats target = hit.transform.GetComponent<PlayerStats>();
        if (target != null)
        {
            target.TakeDamage(bulletDamage);
        }
        if (AddInpactForeceBullets && hit.rigidbody != null)
            hit.rigidbody.AddForce(-hit.normal * inpactForce);
    }
    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 HitPoint, Vector3 HitNormal, float BounceDistance, bool MadeImpact)
    {
        //
        //-----------------
        // trail movment //
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
        // After the trail arrive meke the Inpact logic
        //
        //-----------------
        // Inpact Logic //
        //-----------------
        // Check if we said that can inpact
        if (MadeImpact)
        {
            //
            //-----------------
            // Bounce Logic //
            //-----------------
            //
            // if we said that can Bounce and the Bounce distance of the trajectory does not reach the maximum
            if (AddBounceBullets && BounceDistance > 0)
            {
                Debug.Log(HitNormal+ " : "+direction);
                Vector3 bounceDirection = Vector3.Reflect(direction, HitNormal);
                if (Physics.Raycast(HitPoint, bounceDirection, out RaycastHit bounceHit, BounceDistance))
                {
                    yield return StartCoroutine(SpawnTrail(
                        Trail,
                        bounceHit.point,
                        bounceHit.normal,
                        BounceDistance - Vector3.Distance(bounceHit.point, HitPoint),
                        true
                    ));
                }
                else
                {
                    yield return StartCoroutine(SpawnTrail(
                        Trail,
                        HitPoint + bounceDirection * BounceDistance,
                        Vector3.zero,
                        0,
                        false
                    ));
                }
            }
                Instantiate(inpactEffect, HitPoint, Quaternion.LookRotation(HitNormal));
        }

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
