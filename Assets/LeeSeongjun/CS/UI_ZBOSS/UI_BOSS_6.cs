using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BOSS_6 : MonoBehaviour
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

    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnClickBoss6()
    {
        Main_Number oc = GameObject.Find("�ٱ� ǥ�� �⺻x��ǥ -540 , -1380").GetComponent<Main_Number>();
        if (oc.Boss_Sixteen_Num == 0)
        {
            Black_Boss_1.gameObject.SetActive(false);
            Black_Boss_2.gameObject.SetActive(false);
            Black_Boss_3.gameObject.SetActive(false);
            Black_Boss_4.gameObject.SetActive(false);
            Black_Boss_5.gameObject.SetActive(false);
            Black_Boss_6.gameObject.SetActive(true); //Ȱ��ȭ

            Nomal_Boss_1.gameObject.SetActive(false);
            Nomal_Boss_2.gameObject.SetActive(false);
            Nomal_Boss_3.gameObject.SetActive(false);
            Nomal_Boss_4.gameObject.SetActive(false);
            Nomal_Boss_5.gameObject.SetActive(false);
            Nomal_Boss_6.gameObject.SetActive(false);
            audioSource.Play();
        }
        else if (oc.Boss_Sixteen_Num == 1)
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
            Nomal_Boss_6.gameObject.SetActive(true); //�Ҽ�ȭ
            audioSource.Play();
        }
    }
}
