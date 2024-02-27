using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class chaosMeter : MonoBehaviour {

    public int chaosScore = 0;
    public int maxChaos = 100;

    public float animSpeed = 0.2f;

    private static chaosMeter instance;

    private RectTransform rTransform;
    private float fullWidth;

    void Start()
    {
        rTransform = GetComponent<RectTransform>();
        fullWidth = rTransform.rect.width;

        instance = this;

        instance.UpdateMeter();
    }
    public static void AddScore(int x)
    {
        instance.chaosScore += x;

        if (instance.chaosScore > instance.maxChaos || instance.chaosScore < 0)
        {
            instance.chaosScore -= x;
        }

        instance.UpdateMeter(instance.animSpeed);
    }

    private void UpdateMeter(float time = 0)
    {

        float width = ((float)chaosScore / maxChaos) * fullWidth;

        rTransform.DOSizeDelta(new Vector2(width, rTransform.rect.height), time).SetEase(Ease.InOutElastic);
        ;
    }
}

