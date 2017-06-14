using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BooksAppearance : MonoBehaviour
{
    public GameObject m_PrefabBook;
    public int m_BooksNum;
    public BookEnd m_BookEnd;
    private int m_Count = 0;
    private GameObject m_NewBook;
    private GameObject[] m_DeleteBook;
    private BooksType m_BooksParameter;
    private Vector3 m_AppearancePosition;
    private BoxCollider[] m_Colliders;
    private float[] m_XPosition;
    private AppearanceState m_State = AppearanceState.Idel;
    private bool m_Change = true;
    private bool[] m_Full;

    // Use this for initialization
    void Start()
    {
        m_BooksParameter = BooksType.Medium();
        m_Colliders = transform.GetComponentsInChildren<BoxCollider>();
        m_Full = new bool[m_Colliders.Length];
        m_XPosition = new float[m_Colliders.Length];
        for (int i = 0; i < m_XPosition.Length; i++)
        {
            m_XPosition[i] = transform.position.x - m_Colliders[i].size.x / 2;
            m_Full[i] = false;
        }
        print("a" + m_Colliders.Length);
        print("b" + m_Full.Length);
        print("c" + m_XPosition.Length);
    }

    // Update is called once per frame
    void Update()
    {
        AppearanceUpdate();
    }

    void AppearanceUpdate()
    {
        switch (m_State)
        {
            case AppearanceState.Idel: Idel(); break;
            case AppearanceState.Operate: Operate(); break;
        }
    }

    //待機
    void Idel()
    {
        if (m_Change == true) m_State = AppearanceState.Operate;
    }

    //出現の一連の動作
    void Operate()
    {
        Clear();
        int rand;
        rand = Random.Range(0, 100);
        for (int i = 0; i < m_BooksNum; i++)
        {
            if (rand < 39)
            {
                m_BooksParameter = BooksType.Small();
            }
            else if (rand < 58)
            {
                m_BooksParameter = BooksType.Medium();
            }
            else if (rand < 77)
            {
                m_BooksParameter = BooksType.SmallStr();
            }
            else if (rand < 90)
            {
                m_BooksParameter = BooksType.MediumStr();
            }
            else
            {
                m_BooksParameter = BooksType.MediumMPStr();
            }
            GushBook(Random.Range(0, m_Colliders.Length));
            rand = Random.Range(0, 100);
            for (int n = 0; n < m_Full.Length; n++)
            {
                m_Full[n] = false;
            }
        }
        for (int i = 0; i < m_XPosition.Length; i++)
        {
            m_XPosition[i] = transform.position.x - m_Colliders[i].size.x / 2;
            m_Full[i] = false;
        }

        m_Change = false;
        m_State = AppearanceState.Idel;
    }

    //本の消去
    void Clear()
    {
        if (m_DeleteBook != null) m_DeleteBook.Initialize();
        m_DeleteBook = GameObject.FindGameObjectsWithTag("Book");
        foreach (GameObject clone in m_DeleteBook)
        {
            Destroy(clone);
        }
        if (m_BookEnd.GetMP()) m_BookEnd.Spread();
    }

    //出現
    void GushBook(int i)
    {
        if ((m_XPosition[i] + Book.GetThickness(m_BooksParameter.m_ThicknessDivide)) > 
            (transform.position.x + m_Colliders[i].size.x / 2))
        {
            m_Full[i] = true;
            for (int n = m_Full.Length - 1; n >= m_Full.Length; n--)
            {
                if (!m_Full[n])
                {
                    if ((m_XPosition[n] + Book.GetThickness(m_BooksParameter.m_ThicknessDivide)) >
                        (transform.position.x + m_Colliders[n].size.x / 2))
                    {
                        m_Full[n] = true;
                    }
                    else
                    {
                        GushBookPosition(n);
                    }
                }
            }
        }
        else
        {
            GushBookPosition(i);
        }
        for (int m = 0; m < m_Full.Length; m++)
        {
            if (m_Full[m])
            {
                m_Count++;
            }
        }
        if (m_Count >= m_Colliders.Length)
        {
            m_Count = 0;
            return;
        }
        m_NewBook = (GameObject)Instantiate(m_PrefabBook, m_AppearancePosition, Quaternion.identity);
        m_NewBook.transform.parent = transform;
        m_NewBook.GetComponent<Book>().m_BookParameter = m_BooksParameter;
    }

    void GushBookPosition(int i)    //本の出現位置
    {
        m_XPosition[i] += Book.GetThickness(m_BooksParameter.m_ThicknessDivide);
        m_AppearancePosition = new Vector3(m_XPosition[i] - Book.GetThickness(m_BooksParameter.m_ThicknessDivide) / 2f,
            m_Colliders[i].transform.position.y + Book.GetHeight(m_BooksParameter.m_HeightDivide) / 2.0f - 0.5f,
            m_Colliders[i].transform.position.z + Book.GetWidth(m_BooksParameter.m_WidthDivide) / 2.0f - 0.5f);
    }

    public void SetChange()
    {
        m_Change = true;
    }
}
