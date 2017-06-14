using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class mGauge : MonoBehaviour
{
    GameObject bookEnd;
    public float m_gauge;
    public Image magicGauge;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_gauge >= 50)
        {
            magicGauge.fillAmount = 0.1f;
        }
        Gauge();
    }

    void Gauge()
    {
        BookEnd be = GetComponent<BookEnd>();
        m_gauge = be.m_value;
    }
}
