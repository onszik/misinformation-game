using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadInput : MonoBehaviour
{
    public static string input;
    private ReadInput instance;
    private void Start()
    {
        instance = this;
    }
    public void ReadStringInput(string s)
    {
        input = s;
        Debug.Log(input);
    }
}
