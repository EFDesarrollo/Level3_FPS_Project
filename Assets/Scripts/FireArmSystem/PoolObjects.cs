using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjects : MonoBehaviour
{
    public GameObject poolGameObject;
    public int poolSize = 50;
    private List<GameObject> poolList = new List<GameObject>();
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

    private GameObject InstantiatePoolGameObject()
    {
        GameObject gameObject = Instantiate(poolGameObject);
        gameObject.SetActive(false);
        poolList.Add(gameObject);
        return gameObject;
    }
    public GameObject GetObject()
    {
        GameObject gameObject = poolList.Find(x => x.activeInHierarchy == false);
        if (gameObject == null)
        {
            gameObject = InstantiatePoolGameObject();
        }
        gameObject.SetActive(true);
        return gameObject;
    }
}
