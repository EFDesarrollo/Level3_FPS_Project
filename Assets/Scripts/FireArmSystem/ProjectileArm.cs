using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileArm : MonoBehaviour
{
    public GameObject bullet;

    // Bullet Force
    public float shootForce;
    public float upwardForce;

    // Gun Stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShoots;
    public int magazineSize, bulletPerTap;
    public bool allowButtonHold;

    public int bulletLeft, bulletsShot;

    // Bools
    public bool shooting, readyToShoot, reloading;

    // References
    public Camera fpscam;
    public Transform attackPoint;

    // Bug Fixing
    public bool allowInvoke = true;

    public void Awake()
    {
        // make sure magazine is full
        bulletLeft = magazineSize;
        readyToShoot = true;

    }

    private void Update()
    {
        MyInput();
    }
    private void MyInput()
    {
        // check if alowed to hold down button and take coresponding input
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        // Reloading
        if (Input.GetKeyDown(KeyCode.R) && bulletLeft < magazineSize && !reloading)
        {
            Debug.Log("Reload Input");
            Reload();
        }
        // Reload automatically wen trying to shoot without ammo
        if (readyToShoot && shooting && !reloading && bulletLeft <= 0)
        {
            Debug.Log("Reload no te quedan balas");
            Reload();
        }

        // Shoting
        if (readyToShoot && shooting && !reloading && bulletLeft > 0)
        {
            Debug.Log("Shoot input");
            bulletsShot = 0;
            Shoot();
        }

    }
    private void Shoot()
    {
        readyToShoot = false;

        // find the exact position using a raicast
        Ray ray = fpscam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Just a ray throug the middle of your
        RaycastHit hit;

        // Check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
            Debug.Log(hit.transform.name);
        }
        else targetPoint = ray.GetPoint(75); // just a pont faraway from the player

        // calculate direcction froma attack point to target point
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        // Calculate Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); // just add spread to last dir

        // instantiate bullet/projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

        // Rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        // Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        //currentBullet.GetComponent<Rigidbody>().AddForce(fpscam.transform.up * upwardForce, ForceMode.Impulse);

        bulletLeft--;
        bulletsShot++;

        // Invoke reseetShot Function (if not already invoked)
        if (allowInvoke)
        {
            Invoke("ResetShoot", timeBetweenShooting);
            allowInvoke = false;
        }

        // if more than one bullet  perTap make sure to repeat shoot function
        if (bulletsShot < bulletPerTap && bulletLeft > 0)
            Invoke("Shoot", timeBetweenShoots);
    }
    private void ResetShoot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletLeft = magazineSize;
        reloading = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Ray ray = fpscam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Just a ray throug the middle of your
        Gizmos.DrawRay(ray);
    }
}
