using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ScenesTransitions : MonoBehaviour {
    //public AudioSource sound;

    public float transitionSpeed = 0.5f;

    private Image transition;

    private static ScenesTransitions instance;

    private Vector3 startPos;

    [HideInInspector] public delegate void _beforeSceneUnloaded();
    [HideInInspector] public static event _beforeSceneUnloaded BeforeSceneUnload;

    [HideInInspector] public delegate void _onSceneLoaded();
    [HideInInspector] public static event _onSceneLoaded OnSceneLoad;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        transition = transform.GetChild(0).GetComponent<Image>();

        instance = this;

        startPos = transition.rectTransform.position;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        transition.DOFade(0f, transitionSpeed);

        DOTween.Sequence().AppendInterval(transitionSpeed).Append(transition.rectTransform.DOMoveY(3000, 0f));

        if (OnSceneLoad != null)
        {
            OnSceneLoad();
        }
    }

    public static void NextScene()
    {
        instance.StartCoroutine(instance.LoadSceneAfterDelay(SceneManager.GetActiveScene().buildIndex + 1, 0));
    }
    public static void NextScene(float t)
    {
        instance.StartCoroutine(instance.LoadSceneAfterDelay(SceneManager.GetActiveScene().buildIndex + 1, t));
    }

    public static void PreviousScene()
    {
        instance.StartCoroutine(instance.LoadSceneAfterDelay(SceneManager.GetActiveScene().buildIndex - 1, 0));
    }

    public static void LoadMenu()
    {
        instance.StartCoroutine(instance.LoadSceneAfterDelay(0, 0f));
    }

    public static void LoadFail()
    {
        instance.StartCoroutine(instance.LoadSceneAfterDelay(SceneManager.GetSceneByName("Lose").buildIndex, 0f));
    }
    IEnumerator LoadSceneAfterDelay(int index, float delay)
    {
        yield return new WaitForSeconds(delay);

        transition.rectTransform.DOMoveY(startPos.y, 0f);
        transition.DOFade(1f, transitionSpeed);

        //sound.Play();

        yield return new WaitForSeconds(transitionSpeed);

        if (BeforeSceneUnload != null)
        {
            BeforeSceneUnload();
        }

        SceneManager.LoadScene(index);
    }
}
