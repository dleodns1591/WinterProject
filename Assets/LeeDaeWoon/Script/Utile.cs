using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PRS // PRS = Position Rotation Scale ÀÇ ¾àÀÚ.
{
    public Vector3 pos; // Position
    public Quaternion rot; // Rotation
    public Vector3 Scale; // Scale

    public PRS(Vector3 pos, Quaternion rot, Vector3 Scale)
    {
        this.pos = pos;
        this.rot = rot;
        this.Scale = Scale;
    }
}

public class Utile : MonoBehaviour
{
    public static Quaternion QI => Quaternion.identity;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public static Vector3 MousePos
    {
        get
        {
            Vector3 result = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            result.z = -10;
            return result;
        }
    }
}
