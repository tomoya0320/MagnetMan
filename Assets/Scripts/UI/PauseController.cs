using UnityEngine;
using UnityEngine.UI;

namespace HuaYang.UI {
  public class PauseController : MonoBehaviour {
    [SerializeField]
    private GameObject PasueBoard;
    [SerializeField]
    private AudioSource ClickSound;
    [SerializeField]
    private GameObject PauseButton;

    private void Start() {
      Button btn = GetComponent<Button>();
      //添加点击侦听
      btn.onClick.AddListener(_Click);
    }

    private void _Click() {
      ClickSound.Play();
      PasueBoard.SetActive(true);
      PauseButton.SetActive(false);
      Time.timeScale = 0.0f;
    }
  }
}