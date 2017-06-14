using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectMode : MonoBehaviour
{
    public Fade fade;
    public Button stage1Button, stage2Button, stage3Button, stage4Button, titleButton;


    private void Start()
    {
        fade.FadeOut(1);
        switch (buttonScript.select)
        {
            case 2:
                stage2Button.interactable = true;
                break;
            case 3:
                stage3Button.interactable = true;
                break;
            case 4:
                stage4Button.interactable = true;
                break;
        }
    }

    public void LoadScene(string i)
    {
        //一秒フェードイン後、シーン遷移
        fade.FadeIn(1, () =>
        {
            SceneManager.LoadScene(i);
        });
    }

}
