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
        Debug.Log("종료창 열기");
    }
    void SceneMove()
    {
        QuitConsol.SetActive(false);
        Debug.Log("종료창 닫기");

    }
}
