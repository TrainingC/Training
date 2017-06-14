using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hp : MonoBehaviour
{
    public int m_MaxHP = 100;                       //　最大HP
    public int m_AttackPointMin, m_AttackPointMax;  //　攻撃最小値, 攻撃最大値
    private Image m_HpUI;                           //　HPのUI
    public float m_ResidueHP;                       //　残りHP
    // Use this for initialization
    void Start()
    {
        m_HpUI = transform.FindChild("HP").FindChild("Image").GetComponent<Image>();      //　値を入れる
        m_ResidueHP = m_HpUI.fillAmount;                                        //　HPの増減前準備
    }

    // Update is called once per frame
    void Update()
    {
        m_HpUI.fillAmount = m_ResidueHP;    //残りHPの反映
    }

    //残りHPの割り出し
    public void Damage(int point)
    {
        m_ResidueHP -= (float)point / m_MaxHP;
    }

    //与える攻撃のダメージ量計算
    public int GetAttackPoint()
    {
        return Random.Range(m_AttackPointMin, m_AttackPointMax + 1);
    }

    public void SetDamagePoint(int Point)
    {
        Damage(Point);
    }

    //ここから↓追加
    public float GetDamage()
    {
        return m_MaxHP - m_ResidueHP;
    }
}
