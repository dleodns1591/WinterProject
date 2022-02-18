using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    public int Player_HP = 0;
    public int Player_Money = 0;
    void Start()
    {
        //Player_HP = 100;
        Player_HP = 5;
        Player_Money = 0;
    }
    private void Awake() 
    { 
        var obj = FindObjectsOfType<Player_Control>(); 
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
            Debug.Log("삐빅! 통합 관리 생성");
        }
        else 
        {
            Destroy(gameObject);
            Debug.Log("삐빅! 통합 관리 중복으로 추가 생성 취소");

        }
    }
}
