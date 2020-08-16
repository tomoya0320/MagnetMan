using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class RestartController : MonoBehaviour
{

    public GameObject PasueBoard;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }
}
