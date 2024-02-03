using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PopupMovement : MonoBehaviour
{
    public float swayAmount = 25;
    public float delay = 0.1f;
    public float duration = 2f;

    float xVelocity = 0;

    private RectTransform rect;

    private void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
        Vector3 scale = rect.localScale;

        duration *= Random.Range(0.5f, 1.2f);

        rect.DOAnchorPos(new Vector2(Random.Range(-850, 850), -620), 0);
        rect.DOScale(0, 0);

        rect.DOAnchorPosY(650, duration).SetEase(Ease.InSine);
        Sequence s = DOTween.Sequence();
        s.Append(rect.DOScale(scale, duration * 0.5f).SetEase(Ease.OutSine));
        s.Append(rect.DOScale(0, duration * 0.5f).SetEase(Ease.InSine));

        InvokeRepeating("ChangeDir", 0.1f, delay);
        Destroy(gameObject, duration);
    }

    void ChangeDir()
    {
        xVelocity += Random.Range(-1 * swayAmount, swayAmount);
        rect.DOAnchorPosX(Mathf.Clamp(rect.anchoredPosition.x + xVelocity, -900, 900), delay * 2f);
    }
}
