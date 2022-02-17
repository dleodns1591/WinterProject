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
            // 만약 tr이 null이 아닐경우 현재 위치를 tr.position에 집어넣는다.
            transform.position = tr.position;
        }
    }

    public void SetUp_Transform(Transform tr)
    {
        this.tr = tr;
    }

    public void Damaged(int Damage)
    {
        // 만약 데미지가 0보다 작거나 같다면 return을 해준다.
        if (Damage <= 0)
        {
            return;
        }

        GetComponent<Order>().Set_Order(1000);
        Damage_TMP.text = $"-{Damage}";

        Sequence sequence = DOTween.Sequence()
            // 크기를 0.5초 동안 크기 1만큼 키운다.
            .Append(transform.DOScale(Vector3.one * 1.5f, 0.5f).SetEase(Ease.InOutBack))
            // 1.2초 동안 크기를 키운모습을 보여준다.
            .AppendInterval(1.2f)
            // 0.5초 동안 작아진다.
            .Append(transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutBack))
            // 위에 기능들이 끝나면은 OnComplete를 통해 gameObject를 파괴시킨다.
            .OnComplete(() => Destroy(gameObject));

    }
}
