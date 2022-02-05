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
        Notification_TMP.text = message; // Notification_TMP 텍스트에다가 message를 대입한다.
        Sequence sequence = DOTween.Sequence()
            .Append(transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutQuad)) // Scale이 0에서 커졌다가
            .AppendInterval(0.9f) // 0.9초를 대기하고
            .Append(transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutQuad)); // Scale이 다시 작아진다.
    }
}
