using UnityEngine;

namespace HuaYang.UI {
  public class FailInterface : MonoBehaviour {
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject pause;

    private void OnEnable() {
      pause.SetActive(false);
      GetComponent<AudioSource>().Play();
      Invoke(nameof(ShowMenu), 3.0f);
    }

    private void ShowMenu() {
      menu.SetActive(true);
      gameObject.SetActive(false);
    }
  }
}