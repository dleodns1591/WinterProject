using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PRS
{
    // PRS��? Position Rotation Scale�� �����̴�.
    public Vector3 Pos;
    public Quaternion Rot;
    public Vector3 Scale;

    public PRS(Vector3 Pos, Quaternion Rot, Vector3 Scale)
    {
        this.Pos = Pos;
        this.Rot = Rot;
        this.Scale = Scale;
    }
}

public class Utill : MonoBehaviour
{
    // Quaternion.identity�� ���� ���ֱ� ������ QI�� �ٿ��ش�.
    public static Quaternion QI = Quaternion.identity;

    public static Vector3 Mouse_Pos
    {
        get
        {
            Vector3 Result = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Result.z = -10;
            return Result;
        }
    }
}
