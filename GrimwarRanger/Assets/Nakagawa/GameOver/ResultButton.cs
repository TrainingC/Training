using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultButton : MonoBehaviour {

    public Fade fade;
    public string selectSecne;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ButtonPush()
    {
        //一秒フェードイン後、シーン遷移
        fade.FadeIn(1, () =>
        {
            SceneManager.LoadScene(selectSecne);
        });
    }
}
