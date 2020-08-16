using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeController : MonoBehaviour
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
        PasueBoard.SetActive(false);
        PauseButton.SetActive(true);
        Time.timeScale = 1.0f;
    }
}
