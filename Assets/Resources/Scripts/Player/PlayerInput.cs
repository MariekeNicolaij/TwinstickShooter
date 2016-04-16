using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    void Update()
    {
        Move();
        Rotate();
        Shoot();
    }

    void Move()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            Player.instance.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void Rotate()
    {
        if (Input.GetAxis("Rotate X") != 0 || Input.GetAxis("Rotate Y") != 0)
            Player.instance.Rotate(Input.GetAxis("Rotate X"), Input.GetAxis("Rotate Y"));
        else if (Input.GetAxis("Rotate Mouse") != 0)
            Player.instance.Rotate(Input.mousePosition.x, Input.mousePosition.y);
    }

    void Shoot()
    {
        if (Input.GetAxis("Shoot Left") != 0 || Input.GetAxis("Shoot Right") != 0)
            Player.instance.Shoot(Input.GetAxis("Shoot Left"), Input.GetAxis("Shoot Right"));
    }
}