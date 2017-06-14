using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DestroiOra : MonoBehaviour
{
    public Enemy _enemy;
    private SpriteRenderer _imageRenderer;

    // Use this for initialization
    void Start()
    {
        _imageRenderer = gameObject.GetComponent<SpriteRenderer>();
        _imageRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemy._destryer)
        {
            _imageRenderer.enabled = true;
        }
        else
        {
            _imageRenderer.enabled = false;
        }
    }
}
