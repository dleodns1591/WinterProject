using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit_NONO : MonoBehaviour
{
    public GameObject QuitConsol;


    public void OnClickTitle_Quit_No()
    {
        Invoke("SceneMove", 0.15f);
        Debug.Log("종료창 열기");
    }
    void SceneMove()
    {
        QuitConsol.SetActive(false);
        Debug.Log("종료창 닫기");

    }
}
