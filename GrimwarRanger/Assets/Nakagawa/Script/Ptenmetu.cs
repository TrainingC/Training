using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ptenmetu : MonoBehaviour {

    public AttackEffect m_attack;
    public Boss1Special m_boss1;

    private float nextTime;
    public float interval = 1.0f;

    private SpriteRenderer _imageRenderer;

    // Use this for initialization
    void Start () {
        _imageRenderer = gameObject.GetComponent<SpriteRenderer>();
        nextTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        if (m_attack._attack || m_boss1._boalAttack)
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
