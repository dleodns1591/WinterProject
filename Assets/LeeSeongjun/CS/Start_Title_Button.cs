using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Title_Button : MonoBehaviour
{
    public void OnClickStart_Title()
    {
        Invoke("SceneMove", 0.15f);
    }
    void SceneMove()
    {
        SceneManager.LoadScene("Title_Scene");
        Debug.Log("시작화면에서 타이틀화면으로 이동");
    }
}
