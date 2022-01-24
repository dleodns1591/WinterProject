using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Long_UI_Main : MonoBehaviour
{
    public GameObject Long_UI_Profile;
    public GameObject Long_UI_Inventory;
    public GameObject Long_UI_Card;
    public GameObject Long_UI_Map;
    public GameObject Long_UI_Monster;

    public int Long_UI_Level;
    void Start()
    {
        Long_UI_Level = 0;
    }

    // Update is called once per frame
    public void Update()
    {
        //Main_Number oc = GameObject.Find("¹Ù±ù Ç¥Áö ±âº»xÁÂÇ¥ -540 , -1380").GetComponent<Main_Number>();
        switch (Long_UI_Level)
        {
            case 0:
                Long_UI_Profile.gameObject.SetActive   ( true);
                Long_UI_Inventory.gameObject.SetActive (false);
                Long_UI_Card.gameObject.SetActive      (false);
                Long_UI_Map.gameObject.SetActive       (false);
                Long_UI_Monster.gameObject.SetActive   (false);
                break;
            case 1:
                Long_UI_Profile.gameObject.SetActive   (false);
                Long_UI_Inventory.gameObject.SetActive ( true);
                Long_UI_Card.gameObject.SetActive      (false);
                Long_UI_Map.gameObject.SetActive       (false);
                Long_UI_Monster.gameObject.SetActive   (false);
                break;
            case 2:
                Long_UI_Profile.gameObject.SetActive   (false);
                Long_UI_Inventory.gameObject.SetActive (false);
                Long_UI_Card.gameObject.SetActive      ( true);
                Long_UI_Map.gameObject.SetActive       (false);
                Long_UI_Monster.gameObject.SetActive   (false);
                break;
            case 3:
                Long_UI_Profile.gameObject.SetActive   (false);
                Long_UI_Inventory.gameObject.SetActive (false);
                Long_UI_Card.gameObject.SetActive      (false);
                Long_UI_Map.gameObject.SetActive       ( true);
                Long_UI_Monster.gameObject.SetActive   (false);
                break;
            case 4:
                Long_UI_Profile.gameObject.SetActive   (false);
                Long_UI_Inventory.gameObject.SetActive (false);
                Long_UI_Card.gameObject.SetActive      (false);
                Long_UI_Map.gameObject.SetActive       (false);
                Long_UI_Monster.gameObject.SetActive   ( true);
                break;
        }
    }
}
