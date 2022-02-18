using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Number : MonoBehaviour
{
    private RectTransform rectTransform;
    public int Spot = 0;
    public int countN = 0;
    public int Long_UI_Number = 0;
    /// <summary>
    //
    /// </summary>
    public int Boss_One_Num = 0;
    public int Boss_Two_Num = 0;
    public int Boss_Three_Num = 0;
    public int Boss_Four_Num = 0;
    public int Boss_Five_Num = 0;
    public float Boss_Sixteen_Num = 0;

    private float xMove, yMove;

    Vector2 opentarget = new Vector2(840, 0);
    Vector2 closetarget = new Vector2(0, -1380);
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Spot = 0;
        countN = 1;
        
    }

    void Update()
    {

        if (Spot == 0) //-1380 -> -540
        {
            if (countN == 0)
            {
                Vector2 vec = new Vector2(-840f, 0);
                rectTransform.Translate(vec);
                countN++;
            }
        }
        if (Spot == 1)
        {
            if (countN == 0)
            {
                Vector2 vec = new Vector2(840f, 0);
                rectTransform.Translate(vec);
                countN++;
            }
        }
    }
}
