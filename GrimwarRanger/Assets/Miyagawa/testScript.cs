using UnityEngine;
using System.Collections;

public class testScript : MonoBehaviour {

    GameObject bookEnd;

	// Use this for initialization
	void Start () {
        bookEnd = GameObject.Find("BookEnd");
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            BookEnd be = bookEnd.GetComponent<BookEnd>();
            be.Spread();
        }
	}
}
