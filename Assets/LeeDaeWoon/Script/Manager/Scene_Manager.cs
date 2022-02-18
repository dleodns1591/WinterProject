using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{

    public void Title_Scene()
    {
        SceneManager.LoadScene(0);
        Destroy(GameObject.Find("Stage_test"));
        Destroy(GameObject.Find("Enemy_test"));
        Destroy(GameObject.Find("AudioSound_test"));
    }
}
