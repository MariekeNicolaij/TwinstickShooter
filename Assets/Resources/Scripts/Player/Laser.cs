using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    LineRenderer laser;

    [Header("Laser color")]
    public Color laserColor = new Color(255, 0, 0, 255);

    [Header("Laser width")]
    [Range(0, 10)]
    public float beginWidth = 4;
    [Range(0, 10)]
    public float endWidth = 4;
    float widthDivider = 100;

    [Header("Laser length")]
    public float laserLength = 100;


    void Start()
    {
        laser = GetComponentInChildren<LineRenderer>();

        SetLaser();
    }

    void SetLaser()
    {
        laser.material.color = laserColor;
        laser.SetWidth(beginWidth/widthDivider, endWidth/widthDivider);
    }

    public void BeamLaser(Vector3[] laserPositions)
    {
        laser.SetPositions(laserPositions);
    }
}