using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{
    public static GameOver instance;

    public bool lerp;

    void Awake()
    {
        instance = this;
    }
}
