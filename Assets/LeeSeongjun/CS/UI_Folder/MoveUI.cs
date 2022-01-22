using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUI : MonoBehaviour
{
    public int Spot = 0;
    public GameObject rightArrow;
    public GameObject leftArrow;
    
    void Start()
    {
        rightArrow.SetActive(true); //시작시 오른쪽 열림
        leftArrow.SetActive(false); // 왼쪽은 비활성화
    }

    public void UI_Move()
    {
        Main_Number oc = GameObject.Find("바깥 표지 기본x좌표 -540 , -1380").GetComponent<Main_Number>();
        if (oc.Spot == 0 )
        {
            Debug.Log("UI열림");
            rightArrow.SetActive(false); //오른쪽 열림 , 비활성화
            leftArrow.SetActive(true); // 왼쪽은 활성화
            oc.Spot++;
        }
        else if (oc.Spot == 1 )
        {
            Debug.Log("UI열림");
            rightArrow.SetActive(true); //UI닫힘 , 활성화
            leftArrow.SetActive(false); // 왼쪽은 비활성화
            oc.Spot = 0;
        }
    }
}
