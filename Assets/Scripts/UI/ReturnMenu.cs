using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ReturnMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PasueBoard;
    public AudioSource ClickSound;
    void Start()
    {
        Button btn = this.gameObject.GetComponent<Button>();
        //添加点击侦听
        btn.onClick.AddListener(Click);
    }

    // Update is called once per frame
    void Click()
    {
        ClickSound.Play();
        PasueBoard.SetActive(false);
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1.0f;
    }
}
