using UnityEngine;

namespace HuaYang.Battle {
  public class JumpJudge : MonoBehaviour {
    private PlayerController m_playerController;

    private void Start() {
      m_playerController = transform.parent.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
      if (m_playerController.playerState != PlayerState.STORE_FORCE) {
        JumpMagnet jumpMagnet = collision.GetComponent<JumpMagnet>();
        Magnet magnet = collision.GetComponent<Magnet>();
        if (jumpMagnet && magnet) {
          if (magnet.UpMagneticPole == m_playerController.DownMagneticPole) {
            m_playerController.playerState = PlayerState.IS_GROUNDED;
          }
        } else {
          m_playerController.playerState = PlayerState.IS_GROUNDED;
        }
      }
    }

    private void OnTriggerExit2D(Collider2D collision) {
      if (m_playerController.playerState != PlayerState.STORE_FORCE) {
        m_playerController.playerState = PlayerState.JUMPING;
      }
    }
  }
}