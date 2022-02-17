using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stage_Manager : MonoBehaviour
{
    public int Stage;

    public void Awake()
    {
        Stage = GameObject.Find("Stage_test").GetComponent<Test_Stage>().Stage;
        this.transform.GetChild(Stage -1).gameObject.SetActive(true);
    }

    public void Update()
    {

    }

    public void Next_Stage()
    {
        GameObject.Find("Stage_test").GetComponent<Test_Stage>().Stage += 1;
        Destroy(this.gameObject);
    }
}
