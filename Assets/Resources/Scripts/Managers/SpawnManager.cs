using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
    public GameObject prefab;
    public List<GameObject> prefabs = new List<GameObject>();


    public GameObject CreateObject()
    {
        GameObject go = Instantiate(prefab);
        prefabs.Add(prefab);
        return go;
    }

    public GameObject SpawnObject(Vector3 startPosition, Vector3 lookDirection)
    {
        //if (prefabs.All(b => b.activeSelf))
        //{
        //    GameObject go = CreateBullet();
        //    go.SetActive(true);
        //    go.transform.position = startPosition;
        //    go.transform.LookAt(lookDirection);
        //    return go;
        //}

        //foreach (GameObject go in prefabs)
        //    if (!go.activeSelf)
        //    {
        //        go.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //        go.transform.position = startPosition;
        //        go.transform.LookAt(lookDirection);
        //        return go;
        //    }
        //return null;

        GameObject go = CreateObject();
        go.transform.position = startPosition;
        go.transform.LookAt(lookDirection);
        return go;
    }

    public void DeleteObject(GameObject go)
    {
        prefabs.Remove(go);
    }
}