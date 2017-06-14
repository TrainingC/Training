using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy2 : MonoBehaviour
{

    public int damegeMin, damegeMax;
    public Text eAttack;
    public Player player;
    public SpecialGauge _SG;
    //  public GameObject enemys;
    // public Image _ImageRenderer;
    public bool _destryer = false;
    public bool _specialFire = false, fire = false, _specialBoal = false;
    private ActionGauge aGauge, pGauge;
    private Hp hp, ehp;
    private float count = 0.0f;
    private bool m_anger = false;
    private bool m_i = false;
    public GameOver flag;
    private SpriteRenderer m_renderer;
    private bool m_bEnabled;
    public int attackCount = 1, pCount = 0;
    private bool _restrictionAttack = false;
    private float _time, _fadeTime = 0.5f;

    // Use this for initialization
    void Start()
    {
        hp = transform.parent.FindChild("Php").GetComponent<Hp>();
        ehp = transform.parent.FindChild("Ehp").GetComponent<Hp>();
        aGauge = transform.parent.FindChild("Egauge").GetComponent<ActionGauge>();
        pGauge = transform.parent.FindChild("Pgauge").GetComponent<ActionGauge>();
        m_renderer = gameObject.GetComponent<SpriteRenderer>();
        _time = _fadeTime;


    }

    // Update is called once per frame
    void Update()
    {
        print(attackCount);
        if (aGauge.attack == true && attackCount <= 3)
        {
            Fire();
            Invoke("Fire", 1.0f);
            aGauge.End();
            aGauge.attack = false;
            m_i = true;
            attackCount++;
        }
        //if (aGauge.attack == true && attackCount == 4)
        //{
        //    _specialFire = true;
        //    _specialBoal = true;
        //    aGauge.End();
        //    aGauge.attack = false;
        //    _SG._clear = true;
        //    m_i = true;
        //    attackCount++;
        //}
        //if (aGauge.attack == true && attackCount >= 5)
        //{
        //    Fire();
        //    aGauge.End();
        //    aGauge.attack = false;
        //    m_i = true;
        //    attackCount++;
        //}
        //else if (attackCount == 8)
        //{
        //    Fire();
        //    aGauge.End();
        //    aGauge.attack = false;
        //    m_i = true;
        //    attackCount = 1;
        //}

        if (m_i == true)
        {
            count += Time.deltaTime;
            if (count > 1.0)
            {
                fire = false;
                _specialBoal = false;
                aGauge.gaugeClear = true;
                eAttack.text = "0";
                m_i = false;
                count = 0.0f;
            }
        }
        Destroy();
    }

    private void Fire()
    {
        if ((ehp.m_ResidueHP * ehp.m_MaxHP) >= 200)
        {
            hp.Damage(damegeMin);
            eAttack.text = damegeMin.ToString();
            if (damegeMin > 0)
            {
                fire = true;
            }
        }
        else
        {
            _destryer = true;
            hp.Damage(damegeMax);
            eAttack.text = damegeMax.ToString();
            if (damegeMax > 0)
            {
                fire = true;
            }
        }
    }
    
   

    private void Destroy()
    {
        if (hp.m_ResidueHP <= 0)
        {
            // Anime();
            flag.GameOverColl(false);
        }
    }

    /*   private void Anime()
       {
           _ImageRenderer.color = new Color(1.0f, 0.0f, 0.0f);
               _time -= Time.deltaTime;
               float a = _time / _fadeTime;
               var _color = _ImageRenderer.color;
               _color.a = a;
               _ImageRenderer.color = _color;
           }*/
}