using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pool<T> where T:MonoBehaviour
{
    Queue<T> pool = new Queue<T>();
    public GameObject prefab;


    public bool Spawn(out T t)
    {
        bool existing;
        if (pool.Count > 0)
        {
            existing = true;
            t = pool.Dequeue();
            t.gameObject.SetActive(true);
            //t.Start();
        }
        else
        {
            existing = false;
            GameObject instantiatedObject = GameObject.Instantiate(prefab);
            t = instantiatedObject.GetComponent<T>();
            //t.bulletManager = this;
        }
        return existing;
    }

    public void Destroy(T t)
    {
        t.gameObject.SetActive(false);
        pool.Enqueue(t);
    }
}
