using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PreviewTweet : MonoBehaviour
{
    public GameObject glowPrefab;

    private Sidebar sidebar;

    private static PreviewTweet instance;

    private void Start()
    {
        sidebar = GameObject.Find("Sidebar").GetComponent<Sidebar>();

        instance = this;
    }

    public static void Preview(GameObject tweet, Vector3 center, Action onFinish)
    {
        RectTransform rect = tweet.GetComponent<RectTransform>();
        Vector3 scale = rect.localScale;

        Sequence s = DOTween.Sequence();

        RectTransform glow = Instantiate(instance.glowPrefab).GetComponent<RectTransform>();

        glow.position = center;
        glow.transform.parent = instance.transform;

        s.Append(rect.DOPunchScale(Vector3.Scale(new Vector3(0.15f, 0.15f, 0), scale), 1f, 6, 0f).SetEase(Ease.InOutExpo));
        s.Join(glow.DOScale(Vector3.Scale(new Vector3(2, 2, 1), glow.localScale), 1f).SetEase(Ease.OutExpo));
        s.Join(glow.GetComponent<Image>().DOFade(0, 0.7f).SetEase(Ease.OutExpo));

        s.Append(rect.DOScale(scale, 0));

        Destroy(glow.gameObject, 3f);

        instance.StartCoroutine(instance.WaitForSidebar(tweet, 2f, onFinish));
    }

    IEnumerator WaitForSidebar(GameObject obj, float delay, Action onFinish)
    {
        yield return new WaitForSeconds(delay);

        sidebar.AddTweet(obj);

        onFinish();
    }
}
