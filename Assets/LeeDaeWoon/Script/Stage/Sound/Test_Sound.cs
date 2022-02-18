using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Sound : MonoBehaviour
{
    public static Test_Sound Instance;
    public int Sound = 1;

    void Start()
    {

    }

    void Update()
    {

    }

    private void Awake()
    {
        if (Instance != null)
        {
            Sound = Instance.GetComponent<Test_Sound>().Sound;
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(this.gameObject);
    }
}
