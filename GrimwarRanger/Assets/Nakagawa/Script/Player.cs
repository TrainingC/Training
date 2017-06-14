using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int damege = 0;
    public Text pAttack;
    public float m_interval = 1.0f;
    public Enemy enemy;
    public Enemy2 enemy2;
    public int attackCount = 0;
    private bool bookChange = false;
    public PActionGauge aGauge;
    public Hp hp;
    private bool m_i = false, m_Attack = false;
    private float count = 0.0f;
    public GameOver flag;
    private float m_nextTime;
    public bool fire = false; //攻撃をしたか
    public SpriteRenderer _ImageRenderer;
    private int m_MoveCount;

    //追加
    public int maxAtack = 0, kuraiDamage = 0, men = 0;
    public bool clearFlag = true;
   // public GameObject book;


    // Use this for initialization
    void Start()
    {
      //  hp = transform.parent.FindChild("Ehp").GetComponent<Hp>();
      //  aGauge = transform.parent.FindChild("Pgauge").GetComponent<ActionGauge>();
        m_nextTime = Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        Boss1Special();

        Destroy();
    }

    private void Fire()
    {
            //if文追加
            if (maxAtack < damege)
            {
                maxAtack = damege;
                kuraiDamage = (int)hp.GetDamage();
                if (bookChange)
                {
                    men += 1;
                }
            }
            for (int n = 0; n < m_MoveCount; n++)
            {
                hp.Damage(damege);
            }
            pAttack.text = damege.ToString();
            if (damege > 0)
            {
                fire = true;
            }
            damege = 0;
        
    }

    private void BossSpecialFire()
    {
        if (bookChange)
        {
            //if文追加
            if (maxAtack < damege)
            {
                maxAtack = damege;
                kuraiDamage = (int)hp.GetDamage();
                if (bookChange)
                {
                    men += 1;
                }
            }

            for (int n = 0; n < m_MoveCount; n++)
            {
                hp.Damage(damege);
            }
            pAttack.text = damege.ToString();
            if (damege > 0)
            {
                fire = true;
                bookChange = false;
            }
            damege = 0;
        }
        else
        {
            hp.Damage(0);
            damege = 0;
        }
    }

    private void Destroy()
    {
        //条件式後半追加
        if (hp.m_ResidueHP <= 0 && clearFlag)
        {
            print("ステージクリアおめでとう！！");

            //追加
            flag.ClearSet(maxAtack, kuraiDamage, men / 3);
            clearFlag = false;

            flag.GameOverColl(true);
            //   Time.timeScale = 0.0f;
        }
    }

    public void SetDamage(int i)
    {
        damege = i;
    }

    public bool GetBoolAttack()
    {
        return m_Attack;
    }

    public void SetBoolAttack(bool i)
    {
        m_Attack = i;
    }

    public void BookChange()
    {
        bookChange = true;
        men++;
    }
    public void SetAttackCount(int i)
    {
        m_MoveCount = i;
    }

    void Boss1Special()
    {
        if (enemy != null && enemy._specialFire)
        {
            _ImageRenderer.color = new Color(1.0f, 0.0f, 0.0f);
            if (aGauge.attack == true && attackCount <= 3)
            {
                BossSpecialFire();
               // aGauge.attack = false;
                aGauge.End();
                m_i = true;
                m_Attack = true;
                attackCount++;
            }
            if (aGauge.attack == true && attackCount == 4)
            {
                enemy._specialFire = false;
                Fire();
              //  aGauge.attack = false;
                aGauge.End();
                m_i = true;
                m_Attack = true;
                attackCount = 0;
            }

            if (m_i == true)
            {
                count += Time.deltaTime;
                if (count > 1.0)
                {
                    fire = false;
              //      aGauge.gaugeClear = true;
                    pAttack.text = "0";
                    m_i = false;
                    count = 0.0f;
                }
            }
        }
        else
        {
            _ImageRenderer.color = new Color(1.0f, 1.0f, 1.0f);
            if (aGauge.attack == true)
            {
                Fire();
            //    aGauge.attack = false;
                aGauge.End();
                m_i = true;
                m_Attack = true;
            }

            if (m_i == true)
            {
                count += Time.deltaTime;
                if (count > 1.0)
                {
                    fire = false;
            //        aGauge.gaugeClear = true;
                    pAttack.text = "0";
                    m_i = false;
                    count = 0.0f;
                }
            }
        }
    }
}
