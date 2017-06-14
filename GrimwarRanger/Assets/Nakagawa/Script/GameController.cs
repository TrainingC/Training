using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // 配置するプレハブの読み込み 
    public GameObject prefab;
    public GameObject m_collsionPrefb;
    public BooksAppearance m_BooksApp;
    public Transform curtainPos1, curtainPos2;
    public bool _MenTinretu = false;
    public GameObject curtain;
    private GameObject book, book2, coll, bookEnd;
    private float count = 0, m_MouseTime = 0.0f;
    private bool change = false;
    private bool flag = false;
    private BookEnd m_bookend;   //2017/05/18 更新
    private ActionGauge m_action;
    private collision coll2;
    private int cutain_count = 0;
    private float f_cutain = 0.0f;

    private Vector2 m_2DPos;
    private RectTransform m_BookTextPos;
    private Text m_BookText;

    public Fade fade;

    protected int _sceneTask;

    // Use this for initialization
    void Start()
    {
        m_BookText = GameObject.Find("Canvas").transform.FindChild("BookText").GetComponent<Text>();
        m_BookTextPos = GameObject.Find("Canvas").transform.FindChild("BookText").GetComponent<RectTransform>();
        m_BookText.gameObject.SetActive(false);

        bookEnd = GameObject.Find("BookEnd");
        //   bookSize = GameObject.Find("Book");
        m_bookend = GameObject.FindGameObjectWithTag("BookShelf").GetComponentInChildren<BookEnd>();
        //   widhX = bookSize.gameObject.GetComponent<Renderer>().bounds.size.x;
        //    widhZ = bookSize.gameObject.GetComponent<Renderer>().bounds.size.z;
    }

    // Update is called once per frame
    void Update()
    {
        OnClick();
    }
    void OnClick()
    {
        if (Input.GetMouseButton(0)) m_MouseTime += Time.deltaTime;
        if (Input.GetMouseButtonUp(0))
        {
            m_MouseTime = 0.0f;
            m_BookText.gameObject.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0))//Androidの場合は Input.touchCount>0
        {
          //  print(Input.mousePosition);
            //170529更新
            m_2DPos = RectTransformUtility.WorldToScreenPoint(Camera.main, Camera.main.ScreenToWorldPoint(Input.mousePosition));
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
                    m_bookend.NewMove();
                }

                else if (hit && hitInfo.collider.tag == "Wall2")
                {
                    coll = hitInfo.collider.gameObject;
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
                    m_bookend.NewMove();
                }

                else if (hit && hitInfo.collider.tag == "BookEnd" || hit && hitInfo.collider.tag == "Wall")
                {
                    count = 1;
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
                StartCoroutine(DelaySetChange(2.5f));
                LeanTween.move(curtain, new Vector2(curtain.transform.position.x, curtainPos2.position.y), 2f)
                    .setOnComplete(() =>
                    {
                        LeanTween.move(curtain,
                            new Vector2(curtain.transform.position.x, curtainPos1.position.y), 2f)
                            .setDelay(1f);
                    });
                BookClear();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (book == null)
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
                    book = hitInfo.collider.gameObject;
                    Book bookChange = book.GetComponent<Book>();
                    if (bookChange.GetBookChange())
                    {
                        book.gameObject.transform.Rotate(0, 90, 0);
                        bookChange.SetBookChange(false);
                        book = null;
                        _MenTinretu = true;
                    }
                    else
                    {
                        book.gameObject.transform.Rotate(0, -90, 0);
                        bookChange.SetBookChange(true);
                        book = null;
                        _MenTinretu = false;
                    }
                    m_bookend.NewMove();
                }
            }
        }
        if (m_MouseTime >= 0.5f && book != null)
        {
            m_BookTextPos.position = m_2DPos + new Vector2(10f, 10f);
            if (Vector3.Distance(m_2DPos, RectTransformUtility.WorldToScreenPoint(Camera.main, Camera.main.ScreenToWorldPoint(Input.mousePosition))) <= 1.0f)
            {
                m_BookText.gameObject.SetActive(true);
                switch (book.GetComponent<Book>().m_BookParameter.m_Books)
                {
                    case Books.Small: m_BookText.text = "攻撃魔法書・小\n効果：20のダメージ"; break;
                    case Books.SmallStr: m_BookText.text = "強化魔法書・小\n効果:攻撃時に10ダメージ上乗せする\n3回行動すると効果が切れる"; break;
                    case Books.Medium: m_BookText.text = "攻撃魔法書・小\n効果：30のダメージ"; break;
                    case Books.MediumStr: m_BookText.text = "強化魔法書・中\n効果:攻撃時に20ダメージ上乗せする\n3回行動すると効果が切れる"; break;
                    case Books.MediumMPStr: m_BookText.text = "魔力強化書\n効果:「攻撃魔法書・小」の2冊分ブックエンドを広げる\nカーテンを下すと適応される"; break;
                    case Books.Large: m_BookText.text = "攻撃魔法書・大\n効果：60のダメージ"; break;
                    case Books.Large2: m_BookText.text = "攻撃魔法書・特大\n効果：100のダメージ"; break;
                }
            }
            else
            {
                m_BookText.gameObject.SetActive(false);
                m_MouseTime = 0.0f;
            }
        }
    }
    public void BookClear()
    {
        print("a");
        book = null;
        count = 0;
    }

    public GameObject GetBook()
    {
        return book;
    }

    IEnumerator DelaySetChange(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        m_BooksApp.SetChange();
    }
}
