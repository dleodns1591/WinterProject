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
        Debug.Log("����ȭ�鿡�� Ÿ��Ʋȭ������ �̵�");
    }
}
