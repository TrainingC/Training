using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour {

    public  string selectSecne;
    public SelectMode selectSystem;

	public void ButtonPush()
    {
        selectSystem.LoadScene(selectSecne);   
    }
}
