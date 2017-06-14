using UnityEngine;
using System.Collections;

public class cube : MonoBehaviour {
    Vector3 stdSize;    // 元の大きさ
    Vector3 smlSize;    // クリックされた時の大きさ
    RaycastHit hit;     // RaycastHitの構造体 Rayに最初に接触しているオブジェクト 
    int counter = 0;    // 小さくなっているフレーム数
    bool flg = false;   // true ならオブジェクトは小さくなった状態

    Vector3 scaleX,scaleY,scaleZ; //キューブの大きさ

    // Use this for initialization
    void Start()
    {
        stdSize = new Vector3(1f, 1f, 1f);
        smlSize = new Vector3(0.5f, 0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // オブジェクトがクリックされたら大きさを元に戻す
        if (flg)
        {
            if (counter <= 0)
            {
                hit.transform.localScale = stdSize;
                flg = false;
            }
            else {
                counter--;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 3.0f;

            // 画面上の位置がオブジェクトの領域にあるか調べる
            Ray ray = Camera.main.ScreenPointToRay(pos);

            if (Physics.Raycast(ray, out hit, 100))
            {
                // Rayにヒットしたオブジェクトを動かす
                hit.transform.localScale = smlSize;

                // 元の大きさに戻るまでのフレーム数
                counter = 100;
                flg = true;
            }
            else {
                // オブジェクトがクリックされてなければ、クリックした場所に移動
                Vector3 newpos = Camera.main.ScreenToWorldPoint(pos);
                transform.position = newpos;
            }
        }
    }
    }
