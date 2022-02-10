using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurn_Btn : MonoBehaviour
{
    [SerializeField] Sprite Active;
    [SerializeField] Sprite Inactive;
    [SerializeField] Text Btn_Text;

    void Start()
    {
        Set_Up(false);
        Turn_Manager.OnTurn_Start += Set_Up;
    }

    void Update()
    {
        
    }

    void OnDestroy()
    {
        Turn_Manager.OnTurn_Start -= Set_Up;
    }

    public void Set_Up(bool isActive)
    {
        GetComponent<Image>().sprite = isActive ? Active : Inactive;
        GetComponent<Button>().interactable = isActive;
        Btn_Text.color = isActive ? new Color32(255, 195, 90, 255) : new Color32(55, 55, 55, 255);
    }
}
