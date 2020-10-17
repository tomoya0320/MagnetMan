using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace HuaYang.UI {
  public class ReturnMenu : MonoBehaviour {
    [SerializeField]
    private GameObject PasueBoard;
    [SerializeField]
    private AudioSource ClickSound;

    private void Start() {
      Button btn = GetComponent<Button>();
      //添加点击侦听
      btn.onClick.AddListener(_Click);
    }

    private void _Click() {
      ClickSound.Play();
      PasueBoard.SetActive(false);
      SceneManager.LoadScene("MainScene");
      Time.timeScale = 1.0f;
    }
  }
}