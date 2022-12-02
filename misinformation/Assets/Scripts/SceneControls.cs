using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControls : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Previous()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            ScenesTransitions.PreviousScene();

            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Next()
    {
        if (SceneManager.GetActiveScene().buildIndex < 3)
        {
            ScenesTransitions.NextScene();
        }
    }
}
