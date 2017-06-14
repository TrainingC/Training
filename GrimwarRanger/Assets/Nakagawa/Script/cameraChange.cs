using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cameraChange : MonoBehaviour {
    public Camera m_Camera;
    public GameObject m_Player, m_Npc;
    public GameObject m_house;
    public GameObject m_Tubo;
    public GameObject m_bookMove;
    public SpriteRenderer m_selif1, m_selif2, m_self3, m_self4, m_self5,
        m_self6, m_self7, m_self8, m_self9, m_self10, m_self11, m_self12,
        m_self13, m_self14, m_self15, m_self16, m_self17, m_self18, m_self19,
        m_self20, m_self21, m_self22, m_self23, m_self24, m_self25, m_self26;
    public SpriteRenderer m_image;
    public float m_Duration = 1f;
    public AnimationCurve m_Curve;
    public GameObject m_NPCmovePoint;

    Vector3 m_StartPosition;
    Vector3 m_Destination;

    Quaternion m_StartRotation;
    Quaternion m_DestRotation;

    bool m_Interporating;
    bool m_ftime = false, m_housef = false;
    float m_StartTime;
    float m_timer = 0.0f;
    private float m_alpa, m_alpa2,m_alpa3;
    private float m_alpaTime = .11f;
    private float m_speed = 1.0f;
    private float m_deffTime = 0;
    private float m_fadeTime = 0.0f,m_fadeTime2 = 0.0f,m_fadeTime3 = 0.0f,m_fadeTime4 = 0.0f,m_fadeTime5 = 0.0f,
        m_fadeTime6 = 0.0f, m_fadeTime7 = 0.0f, m_fadeTime8 = 0.0f, m_fadeTime9 = 0.0f, m_fadeTime10 = 0.0f, m_fadeTime11 = 0.0f,
        m_fadeTime12 = 0.0f, m_fadeTime13 = 0.0f, m_fadeTime14 = 0.0f, m_fadeTime15 = 0.0f, m_fadeTime16 = 0.0f, m_fadeTime17 = 0.0f;
    private int m_count = 0;

    void Start()
    {
        m_Destination = m_Camera.transform.position;
        m_DestRotation = m_Camera.transform.rotation;
        m_house.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        m_Tubo.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        m_Player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        m_Npc.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        m_image.enabled = false;
        m_selif1.enabled = false;
        m_selif2.enabled = false;
        m_self3.enabled = false;
        m_self4.enabled = false;
        m_self5.enabled = false;
        m_self6.enabled = false;
        m_self7.enabled = false;
        m_self8.enabled = false;
        m_self9.enabled = false;
        m_self10.enabled = false;
        m_self11.enabled = false;
        m_self12.enabled = false;
        m_self13.enabled = false;
        m_self14.enabled = false;
        m_self15.enabled = false;
        m_self16.enabled = false;
        m_self17.enabled = false;
        m_self18.enabled = false;
        m_self19.enabled = false;
        m_self20.enabled = false;
        m_self21.enabled = false;
        m_self22.enabled = false;
        m_self23.enabled = false;
        m_self24.enabled = false;
        m_self25.enabled = false;
        m_self26.enabled = false;

    }

    void OnEnable()
    {
    /*    if (time <= 0)
        {
            transform.position = endPosition;
            enabled = false;
            return;
        }

       startTime = Time.timeSinceLevelLoad;
        startPosition =m_Npc.transform.position;*/
    }

    void Update()
    {
        print(m_count);

        if(m_count==0)
        m_timer += Time.deltaTime;
        if (m_timer > 11.0f||m_timer==-1)
        {
            m_count = 1;
        }
        if (m_count == 1)
        {
            if(m_fadeTime>=0)
            m_fadeTime += Time.deltaTime;
            if (m_fadeTime > 2.0f||m_fadeTime==-1)
            {
                FadeIn();
                m_timer = -1;
                m_count = 2;
            }
        }
         if (m_count==2)
        {
            if(m_fadeTime2>=0)
            m_fadeTime2 += Time.deltaTime;
            if(m_fadeTime2 > 2.0f||m_fadeTime2==-1)
            {
                PlayerFadeIn();
                m_fadeTime = -1;
                m_count = 3;
            }
        }
         if(m_count==3)
        {
            if(m_fadeTime3>=0)
            m_fadeTime3 += Time.deltaTime;
            if (m_fadeTime3 > 1.0f||m_fadeTime3==-1)
            {
                NPCFadeIn();
                iTween.MoveTo(m_Npc, iTween.Hash("position", new Vector3(m_NPCmovePoint.transform.position.x
                , m_NPCmovePoint.transform.position.y,
                m_NPCmovePoint.transform.position.z), "time", 2));
                m_fadeTime2 = -1;
                m_count = 4;
            }
        }
         if(m_count==4)
        {
            if (m_fadeTime4 >= 0)
            m_fadeTime4 += Time.deltaTime;
            if (m_fadeTime4 > 3.0f||m_fadeTime4==-1)
            {
                m_image.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
                m_selif1.enabled = true;
                m_fadeTime3 = -1;
                m_count = 5;
            }
        }
        if(m_count==5)
        {
            if(m_fadeTime5>=0)
            m_fadeTime5 += Time.deltaTime;
            if(m_fadeTime5>2.0f||m_fadeTime5==-1)
            {
                m_selif1.enabled = false;
                m_selif2.enabled = true;
                m_fadeTime4 = -1;
                m_count = 6;
            }
        }
        if (m_count == 6)
        {
            if (m_fadeTime6 >= 0)
                m_fadeTime6 += Time.deltaTime;
            if (m_fadeTime6 > 2.0f || m_fadeTime6==-1)
            {
                m_Player.GetComponent<DemoPlayer>().Jump();
                m_selif2.enabled = false;
                m_self3.enabled = true;
                m_fadeTime5 = -1;
                m_count = 7;
            }
        }
        if (m_count == 7)
        {
            if (m_fadeTime7 >= 0)
                m_fadeTime7 += Time.deltaTime;
            if (m_fadeTime7 > 2.0f || m_fadeTime7 == -1)
            {
                m_self3.enabled = false;
                m_self4.enabled = true;
                m_fadeTime6 = -1;
                m_count = 8;
            }
        }
        if (m_count == 8)
        {
            if (m_fadeTime8 >= 0)
                m_fadeTime8 += Time.deltaTime;
            if (m_fadeTime8 > 2.0f || m_fadeTime8 == -1)
            {
                m_self4.enabled = false;
                m_self5.enabled = true;
                m_fadeTime7 = -1;
                m_count = 9;
            }
        }
        if(m_count>7)
        {
            m_bookMove.GetComponent<DemoBookMove>().BookMove();
        }
        if (m_count == 9)
        {
            if (m_fadeTime9 >= 0)
                m_fadeTime9 += Time.deltaTime;
            if (m_fadeTime9 > 2.0f || m_fadeTime9 == -1)
            {
                m_image.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                m_self5.enabled = false;
                m_self6.enabled = true;
                m_fadeTime8 = -1;
                m_count = 10;
            }
        }
        if (m_count == 10)
        {
            if (m_fadeTime10 >= 0)
                m_fadeTime10 += Time.deltaTime;
            if (m_fadeTime10 > 2.0f || m_fadeTime10 == -1)
            {
                m_image.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
                m_self6.enabled = false;
                m_self7.enabled = true;
                m_fadeTime9 = -1;
                m_count = 11;
            }
        }
        if (m_count == 11)
        {
            if (m_fadeTime11 >= 0)
                m_fadeTime11 += Time.deltaTime;
            if (m_fadeTime11 > 2.0f || m_fadeTime11 == -1)
            {
                m_self7.enabled = false;
                m_self8.enabled = true;
                m_fadeTime10 = -1;
                m_count = 12;
            }
        }
        if (m_count == 12)
        {
            if (m_fadeTime12 >= 0)
                m_fadeTime12 += Time.deltaTime;
            if (m_fadeTime12 > 2.0f || m_fadeTime12 == -1)
            {
                m_Npc.GetComponent<DemoNPC>().Jump();
                m_self8.enabled = false;
                m_self9.enabled = true;
                m_fadeTime11 = -1;
                m_count = 13;
            }
        }
        if (m_count == 13)
        {
            if (m_fadeTime13 >= 0)
                m_fadeTime13 += Time.deltaTime;
            if (m_fadeTime13 > 2.0f || m_fadeTime13 == -1)
            {
                m_self9.enabled = false;
                m_self10.enabled = true;
                m_fadeTime12 = -1;
                m_count = 14;
            }
        }
        if (m_count == 14)
        {
            if (m_fadeTime14 >= 0)
                m_fadeTime14 += Time.deltaTime;
            if (m_fadeTime14 > 2.0f || m_fadeTime14 == -1)
            {
                m_self10.enabled = false;
                m_self11.enabled = true;
                m_fadeTime13 = -1;
                m_count = 15;
            }
        }
        if (m_count == 15)
        {
            if (m_fadeTime15 >= 0)
                m_fadeTime15 += Time.deltaTime;
            if (m_fadeTime15 > 2.0f || m_fadeTime15 == -1)
            {
                m_self11.enabled = false;
                m_self12.enabled = true;
                m_fadeTime14 = -1;
                m_count = 16;
            }
        }
        if (m_count == 16)
        {
            if (m_fadeTime16 >= 0)
                m_fadeTime16 += Time.deltaTime;
            if (m_fadeTime16 > 2.0f || m_fadeTime16 == -1)
            {
                m_self12.enabled = false;
                m_self13.enabled = true;
                m_fadeTime15 = -1;
                m_count = 17;
            }
        }
        if (m_count == 17)
        {
            if (m_fadeTime17 >= 0)
                m_fadeTime17 += Time.deltaTime;
            if (m_fadeTime17 > 2.0f || m_fadeTime17 == -1)
            {
                m_self13.enabled = false;
                m_self14.enabled = true;
                m_fadeTime17 = -1;
                m_count = 18;
            }
        }


        if (!m_Interporating)
            return;

        float elapsed = Time.time - m_StartTime;

        if (elapsed >= m_Duration)
        {
            m_Camera.transform.position = m_Destination;
            m_Camera.transform.rotation = m_DestRotation;
            m_Interporating = false;
            return;
        }

        float progressRate = elapsed / m_Duration;
        progressRate *= m_Curve.Evaluate(progressRate);

        m_Camera.transform.position =
            Vector3.Lerp(
                m_StartPosition,
                m_Destination,
                progressRate);

        m_Camera.transform.rotation =
            Quaternion.Slerp(
                m_StartRotation,
                m_DestRotation,
                progressRate);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (m_Interporating)
            return;

        if (other.tag == "Book")
        {
            m_Interporating = true;
            m_StartTime = Time.time;
            m_StartPosition = Camera.main.transform.position;
            m_StartRotation = Camera.main.transform.rotation;

            Camera.main.enabled = false;
            m_Camera.enabled = true;

            m_Camera.transform.position = m_StartPosition;
            m_Camera.transform.rotation = m_StartRotation;
        }
    }

    void FadeIn()
    {
            m_house.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, m_alpa);
            m_Tubo.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, m_alpa);
            m_alpa += m_speed;
    }

    void PlayerFadeIn()
    {
        m_Player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, m_alpa2);
        m_alpa2 += m_speed;
    }
    void NPCFadeIn()
    {
        m_Npc.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, m_alpa3);
        m_alpa3 += m_speed;
    }
}
