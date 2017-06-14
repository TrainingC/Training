using UnityEngine;
using System.Collections;

public class DemoBookMove : MonoBehaviour {
    public GameObject m_book;
    public GameObject m_bookMovePoint1, m_bookMovePoint2, m_bookMovePoint3,m_bookMovePoint4;
    public ParticleSystem m_particle;
    public bool m_On = false;
    private int m_count = 1;
    private float m_c=0.0f, m_c2=0.0f, m_c3 = 0.0f,m_c4 = 0.0f,m_c5=0.0f;
    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    public void BookMove()
    {
        if (m_count == 1)
        {
            m_c+=Time.deltaTime;
            iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(m_bookMovePoint1.transform.position.x
                , m_bookMovePoint1.transform.position.y,
                m_bookMovePoint1.transform.position.z), "time", 3, "delay", 2f));
            if (m_c > 2.0f||m_c==-1)
            {
                m_count = 2;
            }
        }
        if(m_count==2)
        {
            m_c2+=Time.deltaTime;
            iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(m_bookMovePoint2.transform.position.x
    , m_bookMovePoint2.transform.position.y,
    m_bookMovePoint2.transform.position.z), "time", 3
    ,"delay",2f));
            if (m_c2 > 2.0f||m_c2==-1)
            {
                m_count = 3;
                m_c = -1;
            }
        }
        if (m_count == 3)
        {
            m_c3 += Time.deltaTime;
            iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(m_bookMovePoint3.transform.position.x
    , m_bookMovePoint3.transform.position.y,
    m_bookMovePoint3.transform.position.z), "time", 3, "delay", 2f));
            if (m_c3 > 2.0f||m_c3==-1)
            {
                m_particle.Play();
                m_count = 4;
                m_c2 = -1;
            }
        }
        if(m_count==4)
        {
            m_c4 += Time.deltaTime;
            iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(m_bookMovePoint4.transform.position.x
                , m_bookMovePoint4.transform.position.y, m_bookMovePoint4.transform.position.z),"time", 3, "delay", 2f));
            if(m_c4 >2.0f||m_c4==-1)
            {
                m_count = 5;
                m_c3 = -1;
            }
        }
        if(m_count==5)
        {
            m_c5 += Time.deltaTime;
            if(m_c5>1.0f||m_c==-1)
            {
                m_count = 3;
                m_c4 = -1;
            }
        }
    }
}
