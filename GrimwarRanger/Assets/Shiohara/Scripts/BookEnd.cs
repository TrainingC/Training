using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class BookEnd : MonoBehaviour
{
    public Image m_mGauge;
    public GameObject m_PairBookEnd;        //ブックエンド片割れ
    public Transform BookEndPos, BookEndPos2;
    public List<GameObject> m_Books;        //取得した本
    public float m_value;//魔力メータの数値
    public float m_Zposition = -2.2f;       //rayの微調整用数値
    public Player m_Player;                 //プレイヤー
    public GameObject m_Enemy;
    public GameController m_GC;             //ゲームコントローラー
    public bool _attack = false;            //攻撃の検知
    public Text m_Text;
    public GameObject m_Particle;
    private float m_Distance, m_Magnification = 1, m_OldMagnification = 1, m_Damage = 0, m_TextTime = 0.0f;        //距離、並べ順による倍率、ダメージ
    public float m_Spread = 0;               //ブックエンドの移動
    //何回while文を回したかのカウント、本とだけraycastさせるためのレイマスク、強化魔法の効果カウント小、強化魔法の効果カウント中
    private int m_Count = 0, m_layerMask = 1 << 8, m_SStrCount = 0, m_MStrCount = 0, m_MoveCount = 0;
    private Vector3 m_Direction, m_BeforePosition, m_PairBookEndBeforePosition;     //方向、前のポジション、片割れの前のポジション
    private bool m_NewMove = true, m_MP = false;            //動いたかどうかのbool、ブックエンド広げるかのbool
    private Ray m_Ray;                                      //bookEndからのray
    private RayState m_State;                               //bookEndのRayの状態
    private RaycastHit m_Hit;
    private List<BooksType> m_Type;                         //本のタイプ
    private bool m_BookNull = false;
    public PActionGauge _aGauge;
    private Vector2 m_pos;
    public float pushPower = 0.2f;
    public GameObject m_Arrow;
    public SpriteRenderer m_AttackBoal;
    public ParticleSystem m_Attackparticle;
    public bool m_GOattack = false, _boalAttack = false;
    private bool m_BookDesfrag = false ,m_fAttackBoalPos = false;
    private float m_destroyCount = 0.0f;
    private int m_value2 = 50;
    private float boal_count = 0.0f;
    private float m_bookDestroy = 0.0f;


    // Use this for initialization
    void Start()
    {
        ///////////////
        m_Text.text = "";
        m_OldMagnification = m_Magnification;
        m_Zposition += transform.parent.position.z;
        m_BeforePosition = transform.position;
        Mete(transform.position, m_PairBookEnd.transform.position);
        m_pos = transform.position;
        m_AttackBoal.enabled = false;
        m_Arrow.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(m_Ray.origin, m_Direction, Color.red);
        RayUpdate();
        if (m_value2 <= m_value)
        {
            m_mGauge.fillAmount += 0.1f;
            m_value2 += 50;
        }
        if (_aGauge.attack == true)
        {
            m_Arrow.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            if (Input.GetMouseButtonDown(0))//Androidの場合は Input.touchCount>0
            {
                // スクリーン座標を三次元のRayに変換する
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // Raycastの結果格納用オブジェクト
                RaycastHit hitInfo;
                // RaycastでRayを飛ばして何かに当たったか調べる
                bool hit = Physics.Raycast(ray, out hitInfo);
                if (hit && hitInfo.collider.tag == "Arrow")
                {
                    BookEndMove();
                }
            }
        }
        else
        {
            m_Arrow.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        }
        if (m_BookDesfrag == true)
        {
            _aGauge.attack = false;
            Invoke("BookDestry", 2.0f);
            Invoke("AttackBoalPos", 2.0f);
            m_GOattack = true;
            if (m_Player.fire)
            {
                LeanTween.move(m_AttackBoal.gameObject, new Vector2(m_Enemy.gameObject.transform.position.x, m_Enemy.gameObject.transform.position.y), .5f)
                    .setDelay(4f)
                .setOnComplete(() => {
                    _boalAttack = true;
                    m_Player.fire = false;
                    _aGauge.m_stop = false;
                });
            }
            // m_BookDesfrag = false;
        }

        // m_minute.transform.eulerAngles = new Vector3(0, 0, -m_value);
    }

    void RayUpdate()
    {
        switch (m_State)
        {
            case RayState.Idel: Idel(); break;
            case RayState.Move: Move(); break;
            case RayState.Hit: Hit(); break;
        }
    }

    void Idel()
    {
        ////////////////
        if (m_TextTime > 1.0f) m_Text.text = "";
        if ((m_Magnification > 1.0f || m_MoveCount > 0) && m_TextTime <= 1.0f)
        {
            m_TextTime += Time.deltaTime;
            if (m_MoveCount == 0) m_Text.text = "特定の並べ方により本の攻撃力の" + m_Magnification + "倍になります\n攻撃回数は1です";
            else m_Text.text = "特定の並べ方により本の攻撃力の" + m_Magnification + "倍になります\n攻撃回数は" + m_MoveCount + "です";
        }
        if (m_NewMove)
        {
            m_State = RayState.Move;
            m_NewMove = false;
        }
        m_BeforePosition = transform.position;
        m_PairBookEndBeforePosition = m_PairBookEnd.transform.position;
    }

    void Move()
    {
        Mete(transform.position, m_PairBookEnd.transform.position);
        m_State = RayState.Hit;
    }

    void Hit()
    {
        m_Books.Clear();
        while (Physics.Raycast(m_Ray, out m_Hit, m_Distance, m_layerMask))
        {
            if (m_Hit.collider != null)
            {
                m_Books.Add(m_Hit.transform.gameObject);
                Mete(m_Books[m_Count].transform.position, m_PairBookEnd.transform.position);
                m_Count++;
                if (m_Count >= 100) break;
            }
        }
        Decision();
        m_Count = 0;
        m_State = RayState.Idel;
    }

    void Decision()
    {
        m_Damage = 0f;
        m_Magnification = 1.0f;
        m_MoveCount = 0;
        for (int i = 0; i < m_Books.Count; i++)
        {
            //本の攻撃力判定
            switch (m_Books[i].GetComponent<Book>().m_BookParameter.m_Books)
            {
                case Books.Small:
                    m_Damage += 20 * ((m_Books[i].GetComponent<Book>().GetBookChange()) ? 2 : 1);
                    if ((m_Books[i].GetComponent<Book>().GetBookChange())) m_Player.BookChange();
                    break;
                case Books.SmallStr:
                    m_SStrCount = 3;
                    if ((m_Books[i].GetComponent<Book>().GetBookChange())) m_Player.BookChange();
                    break;
                case Books.Medium:
                    m_Damage += 30 * ((m_Books[i].GetComponent<Book>().GetBookChange()) ? 2 : 1);
                    if ((m_Books[i].GetComponent<Book>().GetBookChange())) m_Player.BookChange();
                    break;
                case Books.MediumStr:
                    m_MStrCount = 3;
                    if ((m_Books[i].GetComponent<Book>().GetBookChange())) m_Player.BookChange();
                    break;
                case Books.MediumMPStr:
                    m_MP = true;
                    if ((m_Books[i].GetComponent<Book>().GetBookChange())) m_Player.BookChange();
                    break;
                case Books.Large:
                    m_Damage += 60 * ((m_Books[i].GetComponent<Book>().GetBookChange()) ? 2 : 1);
                    if ((m_Books[i].GetComponent<Book>().GetBookChange())) m_Player.BookChange();
                    break;
                case Books.Large2:
                    m_Damage += 80 * ((m_Books[i].GetComponent<Book>().GetBookChange()) ? 2 : 1);
                    if ((m_Books[i].GetComponent<Book>().GetBookChange())) m_Player.BookChange();
                    break;
                default:
                    m_BookNull = true;
                    break;
            }
        }
        int n = 0;
        while (n < m_Books.Count)
        {
            //順序判定
            if (m_Books.Count - n > 1)
            {
                if (m_Books[n].GetComponent<Book>().m_BookParameter.m_HeightDivide >
                    m_Books[n + 1].GetComponent<Book>().m_BookParameter.m_HeightDivide)
                {
                    if (m_Books.Count - n > 2)
                    {
                        if (m_Books[n + 1].GetComponent<Book>().m_BookParameter.m_HeightDivide <
                            m_Books[n + 2].GetComponent<Book>().m_BookParameter.m_HeightDivide)
                        {
                            ParticlePlay(n, n + 2);
                            m_MoveCount++;
                            n += 3;
                        }
                        else if (m_Books[n + 1].GetComponent<Book>().m_BookParameter.m_HeightDivide >
                            m_Books[n + 2].GetComponent<Book>().m_BookParameter.m_HeightDivide)
                        {
                            if (m_Books.Count - n > 3)
                            {
                                if (m_Books[n + 2].GetComponent<Book>().m_BookParameter.m_HeightDivide <
                                m_Books[n + 3].GetComponent<Book>().m_BookParameter.m_HeightDivide)
                                {
                                    ParticlePlay(n + 1, n + 3);
                                    m_MoveCount++;
                                    n += 4;
                                }
                                else {
                                    m_Magnification += 0.06f;
                                    ParticlePlay(n, n + 2);
                                    n += 3;
                                }
                            }
                            else {
                                m_Magnification += 0.04f;
                                ParticlePlay(n, n + 1);
                                n += 2;
                            }
                        }
                        else {
                            m_Magnification += 0.04f;
                            ParticlePlay(n, n + 1);
                            n += 2;
                        }
                    }
                    else {
                        m_Magnification += 0.04f;
                        ParticlePlay(n, n + 1);
                        n += 2;
                    }
                }
                else n++;
            }
            else n++;
        }
        m_Damage *= m_Magnification;
        if (m_SStrCount > 0 && m_Books.Count > 0)
        {
            m_Damage += 10;
        }
        if (m_MStrCount > 0 && m_Books.Count > 0)
        {
            m_Damage += 20;
        }
        m_BookNull = false;
        //最終的なダメージ値の反映
        m_Player.SetDamage((int)m_Damage);
        if (m_MoveCount == 0) m_Player.SetAttackCount(1);
        else m_Player.SetAttackCount(m_MoveCount);
        m_value = m_Damage;
        m_TextTime = 0.0f;                 /////////////
        print(m_Damage);
    }

    public void NewMove()
    {
        m_NewMove = true;
    }


    void Mete(Vector3 v1, Vector3 v2)
    {
        m_Distance = Vector3.Distance(new Vector3(v1.x, v1.y, m_Zposition),
            new Vector3(v2.x, v2.y, m_Zposition));
        m_Direction = new Vector3(v2.x, v2.y, m_Zposition)
            - new Vector3(v1.x, v1.y, m_Zposition);
        m_Ray = new Ray(new Vector3(v1.x, v1.y, m_Zposition), m_Direction);
        m_Ray.GetPoint(m_Distance);
    }

    public bool GetMP()
    {
        return m_MP;
    }

    public void Spread()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 3.0f);
        RaycastHit hitInfo;
        bool hit = Physics.Raycast(ray, out hitInfo);
       /* if (hit && hitInfo.collider.tag == "Wall2")
        {
            m_coll = hitInfo.collider.gameObject;
            var pos = m_coll.transform.position;
            pos = new Vector3(pos.x, transform.position.y, transform.position.z);
            transform.position = pos;
        }*/
        if (m_Spread <= 4)
        {
            transform.position += new Vector3(-2, 0, 0);
            m_Spread++;
        }
        m_MP = false;
    }
    //////////
    private void SetParticlePosition(int i, int n)
    {
        m_Particle.transform.position = (m_Books[i].transform.position + m_Books[n].transform.position) / 2;
        m_Particle.transform.position = new Vector3(m_Particle.transform.position.x, transform.position.y - transform.localScale.y / 2, m_Particle.transform.position.z);
    }

    private void ParticlePlay(int i, int n)
    {
        SetParticlePosition(i, n);
        float radius = m_Particle.GetComponent<ParticleSystem>().shape.radius;
        radius = Vector3.Distance(m_Books[i].transform.position, m_Books[n].transform.position) / 2;
        m_Particle.GetComponent<ParticleSystem>().Play();
    }

    public void BookEndMove()
    {
        m_MoveCount = 0;
            m_value = 0;
            m_SStrCount--;
            m_MStrCount--;
            m_Player.SetBoolAttack(false);
            float bbbbb = m_PairBookEnd.transform.position.x;
            for (int i = 0; i < m_Books.Count; i++)
            { 
                    print(i);
                    bbbbb -= m_Books[i].transform.localScale.x;
                
            }
            bbbbb -= transform.localScale.x * 1.5f;
           LeanTween.move(this.gameObject, new Vector2(bbbbb, this.gameObject.transform.position.y), 0.5f)
                .setOnComplete(() =>
                {
                    LeanTween.move(this.gameObject, new Vector2(BookEndPos.position.x, this.gameObject.transform.position.y), 0.5f)
                             .setDelay(1f);
                    m_Attackparticle.Play();
                    m_BookDesfrag = true;
                    _aGauge.m_stop = true;
                    _aGauge.gaugeClear = true;
                    m_AttackBoal.enabled = true;
                    m_fAttackBoalPos = true;
                });
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Book")
        {
            hit.gameObject.layer = 10;
            hit.rigidbody.AddForce(
            hit.controller.velocity * pushPower,
            ForceMode.Impulse);
        }
    }

    void BookDestry()
    {
        m_value = 0;
        if (m_Books.Count != 0)
        {
            if (0 <= m_Books.IndexOf(m_GC.GetBook()))
            {
                m_GC.BookClear();
            }
            for (int i = 0; i < m_Books.Count; i++)
            {
                Destroy(m_Books[i]);
            }
        }
        m_SStrCount--;
        m_MStrCount--;
    }

    public void AttackBoalPos()
    {
        if(m_AttackBoal.enabled == true && m_fAttackBoalPos == true)
        {
            iTween.MoveTo(m_AttackBoal.gameObject, iTween.Hash("y",14f,"time",2.5f 
                , "oncompletetarget", this.gameObject
                , "oncomplete", "AttackBoalMove"));
        }
    }

    void AttackBoalMove()
    {
        m_fAttackBoalPos = false;
        var Ppos = m_Player.gameObject.transform.position;
        m_AttackBoal.gameObject.transform.position = Ppos;
    }
}
