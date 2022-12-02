using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public AudioSource ting;
    public AudioSource ting1;

    public Camera mainCamera;
    public Color newColor;
    public Color newColor1;

   public void Camerating()
    {
        mainCamera.backgroundColor = newColor;
    }
    public void Camerating1()
    {
        mainCamera.backgroundColor = newColor1;
    }
    public void Sound()
    {
        ting.Play();
    }
    public void Sound1()
    {
        ting1.Play();
    }

}
