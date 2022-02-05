using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] Renderer[] Back_Renderers; // ���ʿ� �ִ� Renderer
    [SerializeField] Renderer[] Middle_Renderers; // �߰��� �ִ� Renderer
    [SerializeField] string Sorting_LayerName; // SortingLayer �̸��� �����ش�.
    int Origin_Order;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void Set_OriginOrder(int Origin_Order) //ī�尡 Ȯ��Ǹ� �� ������ �;��Ѵ�. 
    {
        this.Origin_Order = Origin_Order;
        Set_Order(Origin_Order); // ���ʷ� order�� ������ ���� ���⸦ ȣ���ϸ��� �˾Ƽ� Set_Order�� �Ǹ鼭, order���� ������ �ȴ�.
    }

    public void Set_MostFrontOrder(bool isMostFront)
    {
        Set_Order(isMostFront ? 100 : Origin_Order); //Set_Order�� 8�� �ش�, isMostFront�� true��� 8 false��� firstOrder �̴�.
    }

    public void Set_Order(int order)
    {
        int mulorder = order * 10; // order�� ��ĥ�� �����Ƿ� 10�� �����༭ ������ �ش�.

        foreach (var renderer in Back_Renderers) // ���ʿ� �ִ� Renderer
        {
            renderer.sortingLayerName = Sorting_LayerName;
            renderer.sortingOrder = mulorder; // ������ mulorder( order * 10 )�� �����Ѵ�.
        }

        foreach (var renderer in Middle_Renderers) // �߰��� �ִ� Renderer
        {
            renderer.sortingLayerName = Sorting_LayerName;
            renderer.sortingOrder = mulorder + 1; // "Back_Renderers"�� ��ġ�� �ȵǹǷ�, mulorder�� +1�� �Ͽ� ��ġġ �ʰ� �Ѵ�.
        }
    }
}
