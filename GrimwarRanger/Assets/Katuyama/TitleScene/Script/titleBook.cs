using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class titleBook : MonoBehaviour {


    public float sumTime = 0;
    public float duration = 3.0f;
    public float length = 1f;

    public GameObject startButton, ExsitButton;

    //6月6日追記
    [SerializeField]
    private SoundManeger soundManeger;

    void Start()
    {
        length *= -1;
        //6月6日追記
        soundManeger = GameObject.Find("SoundManeger").GetComponent<SoundManeger>();

    }
    void Update()
    {
        sumTime += Time.deltaTime;
        transform.position = new Vector3(transform.position.x, Mathf.PingPong(sumTime * length / duration * 2, length), transform.position.z);
    }

    public void ButtonPush()
    {

        startButton.SetActive(true);
        ExsitButton.SetActive(true);
        gameObject.SetActive(false);
        //6月6日追記
        soundManeger.SEPlay(0);
    }
}
