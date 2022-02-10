using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Notification : MonoBehaviour
{
    [SerializeField] TMP_Text Notification_TMP;
    void Start() => Scale_Zero();


    void Update()
    {
        
    }

    public void Show(string Message)
    {
        Notification_TMP.text = Message;
        Sequence sequence = DOTween.Sequence()
            // �� ���� �˸��� ���� �˸�â�� ũ�⸦ 1�� �Ѵ���, 0.9�� ����ϰ� ũ�⸦ 0���� �ٿ��ش�.
            .Append(transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutQuad))
            .AppendInterval(0.9f)
            .Append(transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutQuad));
    }


    // Inspectorâ���� ��� �ϱ����� ContextMeneu�� ����Ѵ�.
    [ContextMenu("Scale_One")]
    // Scale_One�� ��� ũ�⸦ 1���Ѵ�.
    private void Scale_One() => transform.localScale = Vector3.one;

    [ContextMenu("Scale_Zero")]
    // Scale_Zero�� ��� ũ�⸦ 0���� �Ѵ�.
    public void Scale_Zero() => transform.localScale = Vector3.zero;
}
