/*
 * GBC Object Pool for Unity
 * Originally Developed by John Schumacher
 * http://gamesbycandlelight.com
 * 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBCObjectPooler : MonoBehaviour {

    [System.Serializable]
    public class Pool {
        public GameObject PooledObject;
        public int PooledAmount;
        public string PoolName;
    }

    public static GBCObjectPooler Instance;
    public List<Pool> Pools;

    private Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake() {
        if (Instance != null && Instance != this) Destroy(gameObject);
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in Pools) {
            // Create a game object for each pool
            // Keeps it clean in the Inspector
            GameObject go = new GameObject(pool.PoolName + " Pool");
            go.transform.SetParent(transform, false);

            // Create an object pool queue
            Queue<GameObject> objectPoolQueue = new Queue<GameObject>();

            // Populate the pool with the defined prefab
            for (int i = 0; i < pool.PooledAmount; i++) {
                GameObject pooledObject = (GameObject)Instantiate(pool.PooledObject, Vector3.zero, Quaternion.identity, go.transform);
                pooledObject.name = pool.PoolName + " (Inactive)";
                pooledObject.SetActive(false);
                objectPoolQueue.Enqueue(pooledObject);
            }

            // Add pool to dictionary
            poolDictionary.Add(pool.PoolName, objectPoolQueue);
        }
	}

    /// <summary>
    /// Gets an unused object from the specified pool
    /// </summary>
    /// <param name="PoolName">Name of Pool</param>
    /// <returns>An unused object or null if there is none available</returns>
    public GameObject GetPooledObject(string PoolName) {
        if (!poolDictionary.ContainsKey(PoolName)) return null;
        if (poolDictionary[PoolName].Count <= 0) return null;
        GameObject go = poolDictionary[PoolName].Dequeue();
        go.name = PoolName;
        return go;
    }

    /// <summary>
    /// Returns the item back to the appropriate pool
    /// </summary>
    /// <param name="go">GameObject to return to pool</param>
    /// <param name="PoolName">Name of Pool</param>
    public bool ReturnToPool(GameObject go, string PoolName) {
        if (!poolDictionary.ContainsKey(PoolName)) return false;

        go.name = PoolName + " (Inactive)";                             
        poolDictionary[PoolName].Enqueue(go);
        go.SetActive(false);
        return true;
    }

    /// <summary>
    /// Returns number of unused objects in the specified pool
    /// </summary>
    /// <param name="PoolName">Name of Pool</param>
    /// <returns>Number of object in pool, or null if pool does not exist</returns>
    public int? ItemsInPool(string PoolName) {
        if (!poolDictionary.ContainsKey(PoolName)) return null;
        return poolDictionary[PoolName].Count;
    }
}
