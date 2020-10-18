using UnityEngine;

namespace HuaYang.Battle {
  public class Flame : MonoBehaviour {
    private AudioSource m_audioSource;

    private void Start() {
      m_audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionStay2D(Collision2D collision) {
      PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
      if (collision.gameObject.tag == "Player" && playerController) {
        Animator anim = collision.gameObject.GetComponent<Animator>();
        if (playerController.life > 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Break")) {
          if (playerController.magnetList.Count > 0) {
            GameObject tmpgo = playerController.magnetList[playerController.life - 2];
            tmpgo.GetComponent<Animator>().Play("Reproduce");
            playerController.magnetList.Remove(tmpgo);
            m_audioSource.Play();
          }
          if (playerController.life <= 1) {
            playerController.actionable = false;
          }
          anim.SetTrigger("break");
        }
      }
    }
  }
}