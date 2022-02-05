using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Scene_Test : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {

    }

    public void Scene()
    {
        SceneManager.LoadScene("Test_Ingame");
    }
}
