using UnityEngine;
using System.Collections;

public class DeleteSave : MonoBehaviour
{
    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Deleted All Keys!");
    }
}