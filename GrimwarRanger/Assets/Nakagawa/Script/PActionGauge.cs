using UnityEngine;
using System.Collections;

public class PActionGauge : MonoBehaviour {

    private float m_max = 10.0f, m_count = 0.0f;
    public float speed = 1f;      //速い速度
    public float slowSpeed = 0.1f; //遅い速度
    public bool flag;               //速度変化のフラグ
    public bool gaugeClear;         //ゲージ初期化のフラグ
    public bool attack = false;     //攻撃感知のフラグ
    private bool end = false;

    void Start()
    {
        flag = true;
        gaugeClear = false;
    }

    void Update()
    {
       // print(m_count);
        //ゲージ初期化
        if (m_count >= m_max && gaugeClear == true)
        {
            m_count = 0;
            gaugeClear = false;
            end = false;
        }

        //速い速度のゲージ
        if (m_count <= m_max && flag == true)
        {
            m_count += speed * Time.deltaTime;
        }
        //遅い速度のゲージ
        else if (flag == false)
        {
            m_count += slowSpeed * Time.deltaTime;
        }
        //完了
        if (m_count >= m_max)
        {
            if (!end) attack = true;
        }
    }
    public void End()
    {
        end = true;
    }
}

