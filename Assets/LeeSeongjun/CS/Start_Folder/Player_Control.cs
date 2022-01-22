using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    public int Player_HP = 0;
    public int Player_Money = 0;
    void Start()
    {
        Player_HP = 100;
        Player_Money = 0;
        DontDestroyOnLoad(gameObject);
    }
}
