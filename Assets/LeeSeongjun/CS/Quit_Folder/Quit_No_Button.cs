using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit_No_Button : MonoBehaviour
{
    public GameObject QuitConsol;
    public AudioSource audioSource;
    void start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnClickTitle_Quit_No()
    {
        audioSource.Play();
        Invoke("SceneMove", 0.15f);
        Debug.Log("����â ����");
    }
    void SceneMove()
    {
        QuitConsol.SetActive(false);
        Debug.Log("����â �ݱ�");

    }
}
