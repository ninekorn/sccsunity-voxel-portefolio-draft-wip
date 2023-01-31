//FROM THE UNITY3D OFFICIAL OBJECT POOL TUTORIAL

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planetdivobjectpool : MonoBehaviour {

    public static new planetdivobjectpool current;
    public GameObject pooledObject;
    public int pooledAmount = 20;
    public bool willGrow = true;
    List<GameObject> pooledObjects;

    void Awake()
    {
        current = this;

        pooledObjects = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

	// Use this for initialization
	void Start () {
        /*pooledObjects = new List<GameObject>();
        for(int i = 0;i <pooledAmount;i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }*/
	}
	
	// Update is called once per frame
	public GameObject GetPooledObject()
    {
        if (pooledObjects!= null)
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    return pooledObjects[i];
                }
            }
        }
      

        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            pooledObjects.Add(obj);
            return obj;
        }
        return null;
    }
}
