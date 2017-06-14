using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public GameObject gameOver, gameCler;
    public Text damageText, kuraiText, narabeText,hosiText;

    //追加
    public PActionGauge playerVer;
    public ActionGauge enemyVer;
    public int hosi = 0;
    public int damageHantei = 100, kuraiHantei = 100, narabeHantei = 10;


    // Use this for initialization
    void Start () {
        gameOver.GetComponent<Canvas>().enabled = false;
        gameCler.GetComponent<Canvas>().enabled = false;
    }


    public void GameOverColl(bool flag)
    {
        if(flag)
            gameCler.transform.parent.FindChild("ClearCanvas").GetComponent<Canvas>().enabled = true;
        else
            gameOver.transform.parent.FindChild("GameOverCanvas").GetComponent<Canvas>().enabled = true;

    }

    public void ClearSet(int damage, int kurai, int narabe)
    {
        playerVer.slowSpeed = 0;
        playerVer.speed = 0;
        enemyVer.slowSpeed = 0;
        enemyVer.speed = 0;

        damageText.text = damage.ToString();
        kuraiText.text = kurai.ToString();
        narabeText.text = narabe.ToString();

        if (damage > damageHantei)
            hosi += 1;
        if (kurai < kuraiHantei)
            hosi += 1;
        if (narabe > narabeHantei)
            hosi += 1;
        Debug.Log(hosi);
        for (int i = 0; i < hosi; i++)
        {
            hosiText.text = hosiText.text + " ★";
        }
    }
}
