using DG.Tweening;
using HuaYang.Battle;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HuaYang.UI {
  public class SuccessInterface : MonoBehaviour {
    [SerializeField]
    private float DurationTime;
    [SerializeField]
    private string NextScene;
    [SerializeField]
    private GameObject successLogo;
    [SerializeField]
    private GameObject player;
    private DOTweenPath m_playerDotPath;

    private void OnTriggerEnter2D(Collider2D collision) {
      if (collision.CompareTag("Player") && collision.GetComponent<PlayerController>().actionable) {
        //通关界面
        player = collision.gameObject;
        player.GetComponent<PlayerController>().actionable = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        player.GetComponent<BoxCollider2D>().enabled = false;
        Vector3 position = player.transform.position;
        position.y = transform.position.y;
        player.transform.position = position;
        position.x = transform.position.x;
        player.transform.DOMove(position, DurationTime);
        Invoke(nameof(GotoNextScene), DurationTime);
      }
    }

    private void GotoNextScene() {
      if (NextScene != "") {
        SceneManager.LoadScene(NextScene);
      } else {
        foreach (AudioSource audio in Camera.main.GetComponents<AudioSource>()) {
          audio.Stop();
        }
        successLogo.SetActive(true);
        successLogo.GetComponent<AudioSource>().Play();
        NextScene = "MainScene";
        Invoke(nameof(GotoNextScene), 5.0f);
      }
    }
  }
}