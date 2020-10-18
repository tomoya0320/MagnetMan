using UnityEngine;

namespace HuaYang.Battle {
  public class JumpMagnet : Magnet {
    [SerializeField]
    private float JumpRate;
    [SerializeField]
    private bool movable;
    private bool m_isPlayerStay;
    private DOTweenCotroller m_dotweenCotroller;
    private AudioSource m_audioSource;
    private PlayerController m_playerController;
    private Rigidbody2D m_rigid2D;

    private void Start() {
      m_isPlayerStay = false;
      m_dotweenCotroller = GetComponent<DOTweenCotroller>();
      m_audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate() {
      if (m_playerController) {
        if (m_isPlayerStay) {
          m_playerController.extraVel = m_dotweenCotroller.dotSpeed;
        } else {
          m_playerController.extraVel = Vector2.zero;
        }
      }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
      if (collision.CompareTag("Player")) {
        m_playerController = collision.GetComponent<PlayerController>();
        m_rigid2D = collision.GetComponent<Rigidbody2D>();
        if (m_playerController.DownMagneticPole != UpMagneticPole) {
          MakePlayerJump();
          Vector3 scale = transform.localScale;
          scale.y *= 0.8f;
          transform.localScale = scale;
          Invoke(nameof(Recover), 0.1f);
        } else if (movable) {
          m_isPlayerStay = true;
        }
      }
    }

    private void OnTriggerExit2D(Collider2D collision) {
      if (movable && collision.CompareTag("Player")) {
        m_isPlayerStay = false;
      }
    }

    private void Recover() {
      Vector3 scale = transform.localScale;
      scale.y /= 0.8f;
      transform.localScale = scale;
    }

    private void MakePlayerJump() {
      m_playerController.playerState = PlayerState.JUMPING;
      Vector2 vel = m_rigid2D.velocity;
      vel.y = m_playerController.JumpSpeed * JumpRate;
      m_rigid2D.velocity = vel;
      StartCoroutine(m_playerController.TailEffect());
      m_audioSource.Play();
    }
  }
}