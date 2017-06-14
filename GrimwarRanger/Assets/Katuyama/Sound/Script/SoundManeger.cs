using UnityEngine;
using System.Collections;

public class SoundManeger : MonoBehaviour {
    [Header("SizeにSEの数を入力後、鳴らしたいＳＥを入れる")]
    public AudioClip[] seBox;
    [SerializeField ,Header("指定したAudioSourceに使用中のSEが入る")]
    private AudioSource audioSource;

    public void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void Update()
    {
        
    }
    //必要な時にこの変数に使いたいSEの番号を送ると再生
    public void SEPlay(int i)
    {
        audioSource.clip = seBox[i];
        audioSource.Play();
    }
    //BGMをストップ（使うかわからないが一応）
    public void BGMStop()
    {
        gameObject.transform.FindChild("BGM").gameObject.GetComponent<AudioSource>().Stop();
    }
}
