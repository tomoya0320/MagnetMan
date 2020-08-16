using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{

    public GameObject PasueBoard;
    public AudioSource ClickSound;
    public GameObject PauseButton;
    void Start()
    {
        Button btn = this.gameObject.GetComponent<Button>();
        //添加点击侦听
        btn.onClick.AddListener(Click);
    }

    void Click()
    {
        ClickSound.Play();
        PasueBoard.SetActive(true);
        PauseButton.SetActive(false);
        Time.timeScale = 0.0f;
    }
}
