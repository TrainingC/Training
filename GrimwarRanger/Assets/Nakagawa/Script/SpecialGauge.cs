using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpecialGauge : MonoBehaviour {
    public Image m_specialGauge;
    public Enemy m_enemy;
    public bool _clear = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	    //ゲージの初期化
        if( _clear ==true)
        {
            m_specialGauge.fillAmount = 0;
            _clear = false;
        }
        if(m_enemy.attackCount == 2)
        {
            m_specialGauge.fillAmount = 0.35f;
        }
        if(m_enemy.attackCount == 3)
        {
            m_specialGauge.fillAmount = .68f;
        }
        if(m_enemy.attackCount == 4)
        {
            m_specialGauge.fillAmount = 1f;
        }

	}
}