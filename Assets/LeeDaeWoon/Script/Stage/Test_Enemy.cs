using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Enemy : MonoBehaviour
{
    public static Test_Enemy instnace;
    public int enemy = 1;

    void Start()
    {

    }

    void Update()
    {

    }

    private void Awake()
    {
        if (instnace != null)
        {
            enemy = instnace.GetComponent<Test_Enemy>().enemy;
            Destroy(this.gameObject);
            return;
        }
        instnace = this;

        DontDestroyOnLoad(this.gameObject);
    }
}
