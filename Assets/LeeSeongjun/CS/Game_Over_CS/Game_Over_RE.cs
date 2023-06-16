using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Over_RE : MonoBehaviour
{
    public AudioSource audioSource;
    void start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void OnClickTitle_Main()
    {
        Destroy(GameObject.Find("Stage_test"));
        Destroy(GameObject.Find("Enemy_test"));
        Destroy(GameObject.Find("AudioSound_test"));
        audioSource.Play();
        Invoke("SceneMove", 0.15f);
    }
    void SceneMove()
    {

        SceneManager.LoadScene("Title_Scene");
        Debug.Log("재시작, 타이틀화면으로 이동");
    }
}
