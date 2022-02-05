using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] Renderer[] Back_Renderers; // 뒤쪽에 있는 Renderer
    [SerializeField] Renderer[] Middle_Renderers; // 중간에 있는 Renderer
    [SerializeField] string Sorting_LayerName; // SortingLayer 이름을 정해준다.
    int Origin_Order;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void Set_OriginOrder(int Origin_Order) //카드가 확대되면 맨 앞으로 와야한다. 
    {
        this.Origin_Order = Origin_Order;
        Set_Order(Origin_Order); // 최초로 order을 설정할 때는 여기를 호출하면은 알아서 Set_Order가 되면서, order들이 정렬이 된다.
    }

    public void Set_MostFrontOrder(bool isMostFront)
    {
        Set_Order(isMostFront ? 100 : Origin_Order); //Set_Order에 8을 준다, isMostFront가 true라면 8 false라면 firstOrder 이다.
    }

    public void Set_Order(int order)
    {
        int mulorder = order * 10; // order가 겹칠수 있으므로 10을 곱해줘서 간격을 준다.

        foreach (var renderer in Back_Renderers) // 뒤쪽에 있는 Renderer
        {
            renderer.sortingLayerName = Sorting_LayerName;
            renderer.sortingOrder = mulorder; // 곱해진 mulorder( order * 10 )을 대입한다.
        }

        foreach (var renderer in Middle_Renderers) // 중간에 있는 Renderer
        {
            renderer.sortingLayerName = Sorting_LayerName;
            renderer.sortingOrder = mulorder + 1; // "Back_Renderers"와 겹치면 안되므로, mulorder에 +1을 하여 겹치치 않게 한다.
        }
    }
}
