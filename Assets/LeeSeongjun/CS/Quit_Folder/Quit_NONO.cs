using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit_NONO : MonoBehaviour
{
    public GameObject QuitConsol;


    public void OnClickTitle_Quit_No()
    {
        Invoke("SceneMove", 0.15f);
        Debug.Log("����â ����");
    }
    void SceneMove()
    {
        QuitConsol.SetActive(false);
        Debug.Log("����â �ݱ�");

    }
}
