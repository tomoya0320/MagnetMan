using UnityEngine;
using UnityEngine.UI;

namespace HuaYang.UI {
  public class ResumeController : MonoBehaviour {
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
      PasueBoard.SetActive(false);
      PauseButton.SetActive(true);
      Time.timeScale = 1.0f;
    }
  }
}