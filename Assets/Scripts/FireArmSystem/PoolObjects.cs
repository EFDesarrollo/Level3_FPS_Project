using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjects : MonoBehaviour
{
    public GameObject BulletPrefab, ImpactEfect;
    public int poolSize = 50;
    private List<GameObject> bulletsPoolList = new List<GameObject>();
    private List<GameObject> ImpactPoolList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            InstantiatePoolGameObject();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject InstantiatePoolGameObject(bool returnimpact = false)
    {
        GameObject bulletObject = Instantiate(BulletPrefab);
        GameObject impactObject = Instantiate(ImpactEfect);
        bulletObject.SetActive(false);
        impactObject.SetActive(false);
        bulletsPoolList.Add(bulletObject);
        ImpactPoolList.Add(impactObject);
        return returnimpact? impactObject: bulletObject;
    }
    public GameObject GetBulletObject()
    {
        GameObject gameObject = bulletsPoolList.Find(x => x.activeInHierarchy == false);
        if (gameObject == null)
        {
            gameObject = InstantiatePoolGameObject();
        }
        gameObject.SetActive(true);
        return gameObject;
    }
    public GameObject GetImpactObject()
    {
        GameObject gameObject = ImpactPoolList.Find(x => x.activeInHierarchy == false);
        if (gameObject == null)
        {
            gameObject = InstantiatePoolGameObject(true);
            Debug.LogError("NoneObj");
        }
        gameObject.SetActive(true);
        return gameObject;
    }
}
