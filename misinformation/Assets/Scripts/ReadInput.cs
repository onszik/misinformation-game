using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadInput : MonoBehaviour
{
    private string input;

    private static ReadInput instance;

    private void Start()
    {
        instance = this;
    }

    public void ReadStringInput(string s)
    {
        input = s;
    }

    public static string text
    {
        get
        {

            return instance.input;
        }
    }
}
