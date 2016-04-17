using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
    public float height = 10;


    void Update()
    {
        transform.position = new Vector3(Player.instance.transform.position.x, Player.instance.transform.position.y + height, Player.instance.transform.position.z + -height);
    }
}