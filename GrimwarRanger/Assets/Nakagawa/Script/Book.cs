using UnityEngine;
using System.Collections;

public class Book : MonoBehaviour
{
    //高さのグループ分け, 魔法陣のグループ分け, 魔法陣の位置のグループ分け
    public BooksType m_BookParameter;
    public Material[] m_Mate;
    private bool m_Change = false;
    private Transform m_BookTransform;
    public Books m_Type;
    void Start()
    {
        gameObject.layer = 8;
        m_BookTransform = gameObject.transform;
        switch (m_BookParameter.m_HeightDivide)
        {
            case Height.Small:
                m_BookTransform.localScale
                     = new Vector3(m_BookTransform.localScale.x, 3f, m_BookTransform.localScale.z); break;
            case Height.Medium:
                m_BookTransform.localScale
                    = new Vector3(m_BookTransform.localScale.x, 4f, m_BookTransform.localScale.z); break;
            case Height.Large:
                m_BookTransform.localScale
                    = new Vector3(m_BookTransform.localScale.x, 5f, m_BookTransform.localScale.z); break;
        }

        switch (m_BookParameter.m_ThicknessDivide)
        {
            case Thickness.Small:
                m_BookTransform.localScale
                     = new Vector3(1f, m_BookTransform.localScale.y, m_BookTransform.localScale.z); break;
            case Thickness.Medium:
                m_BookTransform.localScale
                    = new Vector3(2f, m_BookTransform.localScale.y, m_BookTransform.localScale.z); break;
            case Thickness.Large:
                m_BookTransform.localScale
                    = new Vector3(3f, m_BookTransform.localScale.y, m_BookTransform.localScale.z); break;
        }

        switch (m_BookParameter.m_WidthDivide)
        {
            case Width.Small:
                m_BookTransform.localScale
                     = new Vector3(m_BookTransform.localScale.x, m_BookTransform.localScale.y, 2f); break;
            case Width.Medium:
                m_BookTransform.localScale
                    = new Vector3(m_BookTransform.localScale.x, m_BookTransform.localScale.y, 3f); break;
            case Width.Large:
                m_BookTransform.localScale
                    = new Vector3(m_BookTransform.localScale.x, m_BookTransform.localScale.y, 4f); break;
        }

        switch (m_BookParameter.m_Books)
        {
            case Books.Small: transform.GetComponent<MeshRenderer>().material = m_Mate[0]; break;
            case Books.SmallStr: transform.GetComponent<MeshRenderer>().material = m_Mate[2]; break;
            case Books.Medium: transform.GetComponent<MeshRenderer>().material = m_Mate[1]; break;
            case Books.MediumStr: transform.GetComponent<MeshRenderer>().material = m_Mate[3]; break;
            case Books.MediumMPStr: transform.GetComponent<MeshRenderer>().material = m_Mate[4]; break;
        }
        m_Type = m_BookParameter.m_Books;
    }

    public float GetHeight()
    {
        float a = 0;
        switch (m_BookParameter.m_HeightDivide)
        {
            case Height.Small: a = 3f; break;
            case Height.Medium: a = 4f; break;
            case Height.Large: a = 5f; break;
        }
        return a;
    }
    static public float GetHeight(Height b)
    {
        float a = 0;
        switch (b)
        {
            case Height.Small: a = 3f; break;
            case Height.Medium: a = 4f; break;
            case Height.Large: a = 5f; break;
        }
        return a;
    }

    public float GetThickness()
    {
        float a = 0;
        switch (m_BookParameter.m_ThicknessDivide)
        {
            case Thickness.Small: a = 1f; break;
            case Thickness.Medium: a = 2f; break;
            case Thickness.Large: a = 3f; break;
        }
        return a;
    }

    static public float GetThickness(Thickness b)
    {
        float a = 0;
        switch (b)
        {
            case Thickness.Small: a = 1f; break;
            case Thickness.Medium: a = 2f; break;
            case Thickness.Large: a = 3f; break;
        }
        return a;
    }

    public float GetWidth()
    {
        float a = 0;
        switch (m_BookParameter.m_WidthDivide)
        {
            case Width.Small: a = 2f; break;
            case Width.Medium: a = 3f; break;
            case Width.Large: a = 4f; break;
        }
        return a;
    }
    static public float GetWidth(Width b)
    {
        float a = 0;
        switch (b)
        {
            case Width.Small: a = 2f; break;
            case Width.Medium: a = 3f; break;
            case Width.Large: a = 4f; break;
        }
        return a;
    }

    public void SetBookChange(bool a)
    {
        m_Change = a;
    }

    public bool GetBookChange()
    {
        return m_Change;
    }


}