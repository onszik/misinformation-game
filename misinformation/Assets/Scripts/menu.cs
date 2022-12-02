using UnityEngine;

public class menu : MonoBehaviour
{

    public void PLayGame()
    {
        ScenesTransitions.NextScene();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
