using UnityEngine;
using System.Collections;

public class BookOpen : MonoBehaviour {

    public GameObject m_transform, m_cameraPos;
    public GameObject m_mainCamera;
    private Vector3 m_rotate;
    private float m_roteTimer = 6.0f;
    private bool flag = false;

    // Use this for initialization
    void Start() {
        m_rotate = new Vector3(0, 160, 0);
    }

    // Update is called once per frame
    void Update() {
        Rotate();


    }

    void Rotate()
    {
        LeanTween.rotate(m_transform, m_rotate, m_roteTimer)
                 .setOnComplete(() =>
                {
                    flag = true;
                });
    }
}
