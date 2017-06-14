using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AttackEffect : MonoBehaviour {
    public GameObject _player;
    public Enemy _enemy;
    public bool _attack = false;
    private SpriteRenderer _imageRenderer;

    // Use this for initialization
    void Start () {
        _imageRenderer = gameObject.GetComponent<SpriteRenderer>();
        _imageRenderer.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(_enemy.fire)
        { 
            _imageRenderer.enabled = true;
            LeanTween.scale(gameObject, new Vector2(2f, 2f), .5f).setOnComplete(() => {
                _attack = true;
            }); 
          }
        else
        {
            _imageRenderer.enabled = false;
            _attack = false;
            this.gameObject.transform.position = new Vector2(_player.transform.position.x, _player.transform.position.y);
        }
    }
}
