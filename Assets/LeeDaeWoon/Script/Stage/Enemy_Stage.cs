using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stage : MonoBehaviour
{
    public int enemy;

    public void Awake()
    {
        enemy = GameObject.Find("Enemy_test").GetComponent<Test_Enemy>().enemy;
        this.transform.GetChild(enemy - 1).gameObject.SetActive(true);
    }

    public void Update()
    {

    }

    public void Next_Stage()
    {
        GameObject.Find("Enemy_test").GetComponent<Test_Enemy>().enemy += 1;
        Destroy(this.gameObject);
    }
}
