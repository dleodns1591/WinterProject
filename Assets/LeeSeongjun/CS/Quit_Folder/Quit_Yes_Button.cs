using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit_Yes_Button : MonoBehaviour
{
    public AudioSource audioSource;
    void start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void OnClickTitle_Quit_Yes()
    {
        audioSource.Play();
        Invoke("SceneMove", 0.15f);
    }
    void SceneMove()
    {
        Application.Quit();
        Debug.Log("Á¾·á");
    }
}
