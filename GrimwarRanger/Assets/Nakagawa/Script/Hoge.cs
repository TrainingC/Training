using UnityEngine;
using System;
using System.Collections;

public class Hoge : MonoBehaviour
{
    public GameObject minute;
    public float value;

    void Start()
    {

    }

    void Update()
    {
        minute.transform.eulerAngles = new Vector3(0, 0, -value);


    }
}
