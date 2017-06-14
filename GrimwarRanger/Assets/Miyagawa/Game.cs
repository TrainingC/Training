using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    //ゲージを速くする
        if(Input.GetKeyDown(KeyCode.F1))
        {
            GameObject.Find("Image").GetComponent<ActionGauge>().flag = true;
        }
        //ゲージを遅くする
        if (Input.GetKeyDown(KeyCode.F2))
        {
            GameObject.Find("Image").GetComponent<ActionGauge>().flag = false;
        }
        //ゲージ初期化
        if(Input.GetKeyDown(KeyCode.F3))
        {
            GameObject.Find("Image").GetComponent<ActionGauge>().gaugeClear = true;
        }
    }
}
