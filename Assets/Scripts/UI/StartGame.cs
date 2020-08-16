using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource ClickSound;
    void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        //添加点击侦听
        btn.onClick.AddListener(Click);
    }

    void Click()
    {
        ClickSound.Play();
        SceneManager.LoadScene("Scene_1");
    }
}