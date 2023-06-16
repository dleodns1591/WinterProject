using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Sound : MonoBehaviour
{
    public static Test_Sound instance;
    public int sound = 1;

    void Start()
    {

    }

    void Update()
    {

    }

    private void Awake()
    {
        if (instance != null)
        {
            sound = instance.GetComponent<Test_Sound>().sound;
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }
}
