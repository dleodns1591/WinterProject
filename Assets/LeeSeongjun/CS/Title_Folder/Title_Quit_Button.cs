using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Quit_Button : MonoBehaviour
{
    public GameObject QuitConsol;
    public AudioSource audioSource;
    void Start()
    {
        QuitConsol.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }
    public void OnClickTitle_Quit()
    {
        audioSource.Play();
        Invoke("SceneMove", 0.15f);
    }
    void SceneMove()
    {
        QuitConsol.SetActive(true);
        Debug.Log("종료창 열기");
    }
}
