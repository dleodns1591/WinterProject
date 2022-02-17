using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Damage : MonoBehaviour
{
    [SerializeField] TMP_Text Damage_TMP;
    private Transform tr;

    void Start()
    {
        
    }

    void Update()
    {
        if(tr != null)
        {
            // ���� tr�� null�� �ƴҰ�� ���� ��ġ�� tr.position�� ����ִ´�.
            transform.position = tr.position;
        }
    }

    public void SetUp_Transform(Transform tr)
    {
        this.tr = tr;
    }

    public void Damaged(int Damage)
    {
        // ���� �������� 0���� �۰ų� ���ٸ� return�� ���ش�.
        if (Damage <= 0)
        {
            return;
        }

        GetComponent<Order>().Set_Order(1000);
        Damage_TMP.text = $"-{Damage}";

        Sequence sequence = DOTween.Sequence()
            // ũ�⸦ 0.5�� ���� ũ�� 1��ŭ Ű���.
            .Append(transform.DOScale(Vector3.one * 1.5f, 0.5f).SetEase(Ease.InOutBack))
            // 1.2�� ���� ũ�⸦ Ű������ �����ش�.
            .AppendInterval(1.2f)
            // 0.5�� ���� �۾�����.
            .Append(transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutBack))
            // ���� ��ɵ��� �������� OnComplete�� ���� gameObject�� �ı���Ų��.
            .OnComplete(() => Destroy(gameObject));

    }
}
