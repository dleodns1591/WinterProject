using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Stage : MonoBehaviour
{
    public static Test_Stage Instance;
    public int Stage = 1;

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
            Stage = Instance.GetComponent<Test_Stage>().Stage;
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(this.gameObject);
    }
}
