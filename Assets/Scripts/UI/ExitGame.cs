using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExitGame : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource ClickSound;
    void Start()
    {
        Button btn = this.gameObject.GetComponent<Button>();
        //添加点击侦听
        btn.onClick.AddListener(Click);
    }

    void Click()
    {
        ClickSound.Play();
        Application.Quit();
    }

}