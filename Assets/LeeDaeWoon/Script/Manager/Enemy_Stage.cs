using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stage : MonoBehaviour
{
    public int Enemy;

    public void Awake()
    {
        Enemy = GameObject.Find("Enemy_test").GetComponent<Test_Enemy>().Enemy;
        this.transform.GetChild(Enemy - 1).gameObject.SetActive(true);
    }

    public void Update()
    {

    }

    public void Next_Stage()
    {
        GameObject.Find("Enemy_test").GetComponent<Test_Enemy>().Enemy += 1;
        Destroy(this.gameObject);
    }
}
