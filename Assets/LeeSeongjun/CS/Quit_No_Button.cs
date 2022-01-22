using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit_No_Button : MonoBehaviour
{
    public GameObject QuitConsol;

    public void OnClickTitle_Quit_No()
    {
        Invoke("SceneMove", 0.15f);
    }
    void SceneMove()
    {
        QuitConsol.SetActive(false);
        Debug.Log("¡æ∑·√¢ ¥›±‚");
    }
}
