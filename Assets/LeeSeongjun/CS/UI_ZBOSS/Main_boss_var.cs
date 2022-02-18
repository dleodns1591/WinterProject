using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_boss_var : MonoBehaviour
{
    public GameObject Black_Boss_1;
    public GameObject Black_Boss_2;
    public GameObject Black_Boss_3;
    public GameObject Black_Boss_4;
    public GameObject Black_Boss_5;
    public GameObject Black_Boss_6;

    public GameObject Nomal_Boss_1;
    public GameObject Nomal_Boss_2;
    public GameObject Nomal_Boss_3;
    public GameObject Nomal_Boss_4;
    public GameObject Nomal_Boss_5;
    public GameObject Nomal_Boss_6;
    // Start is called before the first frame update
    void Start()
    {
        Black_Boss_1.gameObject.SetActive(false);
        Black_Boss_2.gameObject.SetActive(false);
        Black_Boss_3.gameObject.SetActive(false);
        Black_Boss_4.gameObject.SetActive(false);
        Black_Boss_5.gameObject.SetActive(false);
        Black_Boss_6.gameObject.SetActive(false);

        Nomal_Boss_1.gameObject.SetActive(false);
        Nomal_Boss_2.gameObject.SetActive(false);
        Nomal_Boss_3.gameObject.SetActive(false);
        Nomal_Boss_4.gameObject.SetActive(false);
        Nomal_Boss_5.gameObject.SetActive(false);
        Nomal_Boss_6.gameObject.SetActive(false);
    }
}
