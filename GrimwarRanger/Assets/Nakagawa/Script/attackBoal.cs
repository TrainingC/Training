using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class attackBoal : MonoBehaviour {

    public Player _player;
    public Enemy _enemy;
    public bool _boalAttack = false;
    private SpriteRenderer _imageRenderer;

    // Use this for initialization
    void Start () {
      //  this.gameObject.transform.position = new Vector2(_player.transform.position.x, _player.transform.position.y);
        _imageRenderer = gameObject.GetComponent<SpriteRenderer>();
        _imageRenderer.enabled = false;
	}

    // Update is called once per frame
    void Update()
    {
        if (_player.fire)
        {
            _imageRenderer.enabled = true;
            LeanTween.move(gameObject, new Vector2(_enemy.gameObject.transform.position.x, _enemy.gameObject.transform.position.y), .5f)
                .setOnComplete(() => {
                    _boalAttack = true;
                });
        }
        else
        {
            _imageRenderer.enabled = false;
            _boalAttack = false;
            this.gameObject.transform.position = new Vector2(_player.transform.position.x, _player.transform.position.y);
        }
    }
}
