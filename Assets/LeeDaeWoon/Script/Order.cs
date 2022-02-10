using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] Renderer[] Back_Renderers; // ���ʿ� �ִ� Renderers�� �����´�.
    [SerializeField] Renderer[] Middle_Renderers; // �߰��� �ִ� Renderers�� �����´�.
    [SerializeField] string Sorting_LayerName; // SortingLayer�� �̸��� �����ش�.
    private int Origin_Order;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void Set_Order(int Order)
    {
        // �ܺο��� order�� �Է��ϸ� x10�� �Ѵ�. 
        // ī���� order�� 1, 2, 3 �����̸� �������� ������ 10�� ���Ͽ� 10���� ������ �ش�.
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
