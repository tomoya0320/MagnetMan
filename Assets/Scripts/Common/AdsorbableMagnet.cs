using UnityEngine;
using DG.Tweening;

namespace HuaYang.Battle {
  public class AdsorbableMagnet : Magnet {
    private AudioSource m_audioSource;
    private Vector3 m_oPos;
    private GameObject m_head;

    private void Start() {
      m_audioSource = GetComponent<AudioSource>();
      m_head = transform.GetChild(0).gameObject;
      m_oPos = m_head.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
      PlayerController playerController = collision.GetComponent<PlayerController>();
      if (collision.tag == "Player" && playerController) {
        if (playerController.DownMagneticPole == UpMagneticPole && playerController.life < 3) {
          playerController.magnetList.Add(gameObject);
          playerController.DownMagneticPole = DownMagneticPole;
          playerController.ChangeAnim(++playerController.life);
          foreach (BoxCollider2D boxCollider2D in GetComponents<BoxCollider2D>()) {
            boxCollider2D.enabled = false;
          }
          GetComponent<Animator>().Play("NoMagnet");
          m_head.transform.position = m_oPos;
          m_head.transform.DOMoveY(m_oPos.y - 0.5f, 0.5f);
          m_head.GetComponent<SpriteRenderer>().color = Color.white;
          m_head.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 0.5f);
          m_audioSource.Play();
        }
      }
    }
    public void EnableCollision() {
      foreach (BoxCollider2D boxCollider2D in GetComponents<BoxCollider2D>()) {
        boxCollider2D.enabled = true;
      }
    }
  }
}