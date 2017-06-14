using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mouse : MonoBehaviour
{
    private float count = 0;
    private bool change = false;
    private float Tpos, Rpos, Lpos, Bpos;
    Vector2 TopPos, RightPos, LeftPos, BottomPos, WallTopBanPos;
    public GameObject book, book2, curtain;
    [SerializeField]
    GameObject wallTop, wallRight, wallLeft, wallBottom, WallTopBan;
    public Transform curtainPos;

    // Use this for initialization
    void Start()
    {
        TopPos = wallTop.gameObject.transform.position;
        RightPos = wallRight.gameObject.transform.position;
        LeftPos = wallLeft.gameObject.transform.position;
        BottomPos = wallBottom.gameObject.transform.position;
        WallTopBanPos = WallTopBan.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Exchange();

    }

    public void Exchange()
    {

        float Ty = TopPos.y;
        float By = BottomPos.y;
        float Rx = RightPos.x;
        float Lx = LeftPos.x;
        float TBy = WallTopBanPos.y;
        if (Input.GetMouseButtonDown(0))//Androidの場合は Input.touchCount>0
        {
            print(Input.mousePosition);
            if (book == null && count == 0)
            {
                // スクリーン座標を三次元のRayに変換する
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                // Raycastの結果格納用オブジェクト
                RaycastHit hitInfo;

                // RaycastでRayを飛ばして何かに当たったか調べる
                bool hit = Physics.Raycast(ray, out hitInfo);

                // Rayが何かに当たった場合かつそれがタグ"Book"ならbookに格納する
                if (hit && hitInfo.collider.tag == "Book")
                {
                    print(hitInfo.collider.gameObject.name);
                    book = hitInfo.collider.gameObject;
                    count = 1;
                    Book bookChange = book.GetComponent<Book>();
                    if (bookChange.GetBookChange())
                    {
                        book.gameObject.transform.Rotate(0, 0, 20);

                    }
                    else
                    {
                        book.gameObject.transform.Rotate(-20, 0, 0);
                    }

                }
            }
            else
            {
                // スクリーン座標を三次元のRayに変換する
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                // Raycastの結果格納用オブジェクト
                RaycastHit hitInfo;

                // RaycastでRayを飛ばして何かに当たったか調べる
                bool hit = Physics.Raycast(ray, out hitInfo);

                // Rayが何かに当たった場合、そのオブジェクトをbook2に格納する
                if (hit && hitInfo.collider.tag == "Book")
                {

                    book2 = hitInfo.collider.gameObject;
                    var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    var Bpos = book.transform.position;
                    pos.z = book.transform.position.z;
                    book.transform.position = pos;
                    book2.transform.position = Bpos;
                    Book bookChange = book.GetComponent<Book>();
                    if (bookChange.GetBookChange())
                    {
                        book.gameObject.transform.Rotate(0, 0, -20);
                    }
                    else
                    {
                        book.gameObject.transform.Rotate(20, 0, 0);
                    }
                    book = null;
                    book2 = null;
                    count = 0;

                }

                else if (hit && hitInfo.collider.tag == "BookEnd" || hit && hitInfo.collider.tag == "Wall")
                {
                    count = 1;
                }

                else if (hit && hitInfo.collider.tag == "Wall2")
                {
                    Book bookChange = book.GetComponent<Book>();
                    if (bookChange.GetBookChange())
                    {
                        book.gameObject.transform.Rotate(0, 0, -20);
                    }
                    else
                    {
                        book.gameObject.transform.Rotate(20, 0, 0);
                    }
                    var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    pos.z = book.transform.position.z;
                    book.transform.position = pos;
                    book = null;
                    count = 0;
                }


            }

            // スクリーン座標を三次元のRayに変換する
            Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Raycastの結果格納用オブジェクト
            RaycastHit hitInfo2;

            // RaycastでRayを飛ばして何かに当たったか調べる
            bool hit2 = Physics.Raycast(ray2, out hitInfo2);
            // Rayがタグcurtainに当たった場合魔法のカーテンが動く
            if (hit2 && hitInfo2.collider.tag == "curtain")
            {
                count = 3;
                if (count == 3)
                {
                    curtain = hitInfo2.collider.gameObject;
                    LeanTween.move(curtain, new Vector2(curtain.transform.position.x, TBy), 6f)
                        .setOnComplete(()=>{
                            LeanTween.move(curtain,
            new Vector2(curtain.transform.position.x,
            curtainPos.position.y), 6f);
                        });
                    
                }
            }
        }
    }
}
