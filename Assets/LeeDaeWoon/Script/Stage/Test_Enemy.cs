using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Enemy : MonoBehaviour
{
    public static Test_Enemy Instance;
    public int Enemy = 1;

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
            Enemy = Instance.GetComponent<Test_Enemy>().Enemy;
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(this.gameObject);
    }
}
