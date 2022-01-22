using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit_Yes_Button : MonoBehaviour
{
    public void OnClickTitle_Quit_Yes()
    {
        Invoke("SceneMove", 0.15f);
    }
    void SceneMove()
    {
        Application.Quit();
        Debug.Log("Á¾·á");
    }
}
