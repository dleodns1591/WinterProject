using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Quit_Button : MonoBehaviour
{
    public GameObject QuitConsol;
    // Start is called before the first frame update
    void Start()
    {
        QuitConsol.SetActive(false);
    }
    public void OnClickTitle_Quit()
    {
        Invoke("SceneMove", 0.15f);
    }
    void SceneMove()
    {
        QuitConsol.SetActive(true);
        Debug.Log("종료창 열기");
    }
}
