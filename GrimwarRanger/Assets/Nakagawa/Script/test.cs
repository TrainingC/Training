using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public attackBoal m_boal;
    private float nextTime;
    public float interval = 1.0f;

    private SpriteRenderer _imageRenderer;

    void Start()
    {
        _imageRenderer = gameObject.GetComponent<SpriteRenderer>();
        nextTime = Time.time;
    }

    void Update()
    {
        if (m_boal._boalAttack)
        {
            if (Time.time > nextTime)
            {
                _imageRenderer.enabled = !_imageRenderer.enabled;
                nextTime += interval;
            }
        }
        else
        {
            _imageRenderer.enabled = true;
        }
    }
}