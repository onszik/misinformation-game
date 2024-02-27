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
        tweet.tag = "Untagged";

        RectTransform rect = tweet.GetComponent<RectTransform>();
        Vector3 scale = rect.localScale;
        RectTransform glow = Instantiate(instance.glowPrefab, instance.glowPrefab.transform.position, instance.glowPrefab.transform.rotation)
                            .GetComponent<RectTransform>();
        glow.transform.SetParent(instance.transform.parent, false);
        glow.localScale = new Vector2(0.75f, 0.75f);

        Sequence s = DOTween.Sequence();

        glow.position = center;
        glow.transform.parent = instance.transform;

        s.Append(rect.DOPunchScale(Vector3.Scale(new Vector3(-0.1f, -0.1f, 0), scale), 0.5f, 7, 3f).SetEase(Ease.InOutExpo));
        s.Join(glow.DOScale(Vector3.Scale(new Vector3(1.5f, 1.5f, 1), glow.localScale), 1f).SetEase(Ease.OutExpo));
        s.Join(glow.GetComponent<Image>().DOFade(0, 1f).SetEase(Ease.OutExpo));

        //s.Append(rect.DOScale(scale, 0));

        Destroy(glow.gameObject, 1.5f);

        instance.StartCoroutine(instance.WaitForSidebar(tweet, 2f, onFinish));
    }

    IEnumerator WaitForSidebar(GameObject obj, float delay, Action onFinish)
    {
        yield return new WaitForSeconds(delay);

        sidebar.AddTweet(obj);

        onFinish();
    }
}
