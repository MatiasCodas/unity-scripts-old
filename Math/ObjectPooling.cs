using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    private Transform trans;

    [SerializeField]
    private GameObject prefabToPool;
    [SerializeField]
    private int initialAmount;

    public List<GameObject> Objects { get; private set; }

    private void Awake()
    {
        trans = transform;
        Objects = new List<GameObject>();
        GeneratePool();
    }

    private void GeneratePool()
    {
        for (int i = 0; i < initialAmount; i++)
        {
            GrowPool(1);
        }
    }

    private void GrowPool(int ammountToGrow)
    {
        //Instantiates a new object and sets this pool as its parent
        for (int i = 0; i < ammountToGrow; i++)
        {
            GameObject newObj = Instantiate(prefabToPool, this.transform);
            ReturnToPool(newObj);
            Objects.Add(newObj);
        }
    }

    public GameObject GetAvailable()
    {
        //returns the first available object in pool
        for (int i = 0; i < Objects.Count; i++)
        {
            if (!Objects[i].activeInHierarchy)
            {
                RemoveFromPool(Objects[i]);
                return Objects[i];
            }
        }

        //if it doesn't find any available object
        //instantiate a new one in the pool and returns it.

        //ReturnToPool(pool[0]);
        //GameObject newObj = pool[0];

        //pool.Remove(newObj);
        //pool.Add(newObj);

        //RemoveFromPool(newObj);

        GrowPool(1);
        GameObject newObj = Objects[Objects.Count - 1];
        RemoveFromPool(newObj);
        return newObj;
    }

    public GameObject[] GetAvailable(int ammountToGet)
    {
        //returns an array of available objects in pool
        List<GameObject> returnedList = new List<GameObject>();
        for (int i = 0; i < ammountToGet; i++)
        {
            //Get available objects until the list has enought to return
            returnedList.Add(GetAvailable());
        }

        //returns the list in the form of an array
        return returnedList.ToArray();
    }

    public void ReturnToPool(GameObject objToPool)
    {
        objToPool.transform.SetParent(trans);
        objToPool.SetActive(false);
    }

    public void RemoveFromPool(GameObject objToRemove)
    {
        objToRemove.SetActive(true);
        bool asdasd = gameObject.IsInPool(this);
    }
}

public static class PoolExtensions
{
    //for ease of writing/reading

    //returns true if the object is in the pool
    public static bool IsInPool(this GameObject objToVerify, ObjectPooling pool)
    {
        if (pool.Objects.Contains(objToVerify))
            return true;
        return false;
    }

    //returns true if the pool cantains the object
    public static bool Contains(this ObjectPooling pool, GameObject objToVerify)
    {
        if (pool.Objects.Contains(objToVerify))
            return true;
        return false;
    }

    public static void ReturnToPool(this GameObject objToReturn, ObjectPooling pool)
    {
        if (!objToReturn.IsInPool(pool))
        {
            Debug.LogError($"The pool {pool} doesn't contain the object {objToReturn}.");
            return;
        }


        pool.ReturnToPool(objToReturn);
    }
}
