using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    public static Manager Instance;
    public int Stage;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void Update()
    {
        if (Stage == 0)
        {
            this.transform.GetChild(Stage).gameObject.SetActive(true);
        }
        else
        {
            this.transform.GetChild(Stage - 1).gameObject.SetActive(false);
            this.transform.GetChild(Stage).gameObject.SetActive(true);
        }
    }

    public void Next_Stage()
    {
        Stage += 1;
    }
}
