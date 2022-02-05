using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Notification_Panel : MonoBehaviour
{
    [SerializeField] TMP_Text Notification_TMP;

    [ContextMenu("ScaleOne")]
    void ScaleOne() => transform.localScale = Vector3.one;
    [ContextMenu("ScaleZero")]
    public void ScaleZero() => transform.localScale = Vector3.zero;

    void Start() => ScaleZero();

    void Update()
    {
        
    }

    public void Show(string message)
    {
        Notification_TMP.text = message; // Notification_TMP �ؽ�Ʈ���ٰ� message�� �����Ѵ�.
        Sequence sequence = DOTween.Sequence()
            .Append(transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutQuad)) // Scale�� 0���� Ŀ���ٰ�
            .AppendInterval(0.9f) // 0.9�ʸ� ����ϰ�
            .Append(transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutQuad)); // Scale�� �ٽ� �۾�����.
    }
}
