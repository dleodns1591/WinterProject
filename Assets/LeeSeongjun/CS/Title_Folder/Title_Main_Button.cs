using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Title_Main_Button : MonoBehaviour
{
    public void OnClickTitle_Main()
    {
        Invoke("SceneMove",0.15f);
    }
    void SceneMove()
    {
        SceneManager.LoadScene("Main_Scene");
        Debug.Log("타이틀화면에서 메인화면으로 이동");
    }
}