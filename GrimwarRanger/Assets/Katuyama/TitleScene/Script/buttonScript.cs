using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class buttonScript : MonoBehaviour {
    
    public Fade fade = null;
    public static int select = 1;

    //6月6日追記
    [SerializeField]
    private SoundManeger soundManeger;

    public void Start()
    {
        //6月6日追記
        soundManeger = GameObject.Find("SoundManeger").GetComponent<SoundManeger>();
    }

    public void ButtonPush()
    {
        //6月6日追記
        soundManeger.SEPlay(1);

        //一秒かけてフェードイン後、シーン遷移
        fade.FadeIn(1, () =>
        {
            SceneManager.LoadScene("Select");
        });
    }
    //ゲーム終了
    public void ExsitButton()
    {
        //6月6日追記
        soundManeger.SEPlay(1);
        //エディター終了
        UnityEditor.EditorApplication.isPlaying = false;  
 
        Application.Quit();
    }
}
