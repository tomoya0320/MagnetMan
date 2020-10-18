using UnityEngine;
using DG.Tweening;

namespace HuaYang.Battle {
  public class SteelSwitch : Magnet {
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private Transform Steel;
    [SerializeField]
    private AudioClip[] audios;
    [SerializeField]
    private bool isOpen;
    private SpriteRenderer m_spriteRenderer;
    private AudioSource m_audioSource;
    private DOTweenAnimation m_steelDOTweenAnim;

    private void Start() {
      m_spriteRenderer = GetComponent<SpriteRenderer>();
      m_audioSource = GetComponent<AudioSource>();
      m_steelDOTweenAnim = Steel.GetComponent<DOTweenAnimation>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
      PlayerController playerController = collision.GetComponent<PlayerController>();
      if (collision.CompareTag("Player") && playerController != null) {
        if (playerController.DownMagneticPole == UpMagneticPole) {
          isOpen = !isOpen;
          if (isOpen) {
            m_steelDOTweenAnim.DOPlay();
          } else {
            m_steelDOTweenAnim.DOPause();
          }
          m_spriteRenderer.sprite = sprites[isOpen ? 1 : 0];
          m_audioSource.clip = audios[0];
          m_audioSource.Play();
        } else {
          m_spriteRenderer.sprite = sprites[2];
          m_audioSource.clip = audios[1];
          m_audioSource.Play();
        }
      }
    }
  }
}