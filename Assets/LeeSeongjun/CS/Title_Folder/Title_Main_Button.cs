using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Title_Main_Button : MonoBehaviour
{
    public AudioSource audioSource;
    void start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnClickTitle_Main()
    {
        audioSource.Play();
        Invoke("SceneMove",0.15f);
    }

    void SceneMove()
    {

        SceneManager.LoadScene("Test_Ingame");
        Debug.Log("Ÿ��Ʋȭ�鿡�� ����ȭ������ �̵�");
    }
}