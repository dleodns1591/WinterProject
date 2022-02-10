using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] Renderer[] Back_Renderers; // 뒤쪽에 있는 Renderers를 가져온다.
    [SerializeField] Renderer[] Middle_Renderers; // 중간에 있는 Renderers를 가져온다.
    [SerializeField] string Sorting_LayerName; // SortingLayer의 이름을 정해준다.
    private int Origin_Order;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void Set_Order(int Order)
    {
        // 외부에서 order만 입력하면 x10을 한다. 
        // 카드의 order가 1, 2, 3 간격이면 겹쳐지기 때문에 10을 곱하여 10차이 간격을 준다.
        int MulOrder = Order * 10; 

        foreach(var Renderer in Back_Renderers)
        {
            Renderer.sortingLayerName = Sorting_LayerName;
            Renderer.sortingOrder = MulOrder;
        }

        foreach(var Renderer in Middle_Renderers)
        {
            Renderer.sortingLayerName = Sorting_LayerName;
            Renderer.sortingOrder = MulOrder + 1;
        }
    }

    public void Set_OriginOrder(int Origin_Order)
    {
        this.Origin_Order = Origin_Order;
        Set_Order(Origin_Order);
    }

    public void Set_MostFrontOrder(bool isMostFront)
    {
        Set_Order(isMostFront ? 8 : Origin_Order);
    }
}
