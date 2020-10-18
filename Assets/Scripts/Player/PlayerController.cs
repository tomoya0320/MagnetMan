using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HuaYang.Battle {
  public enum MagneticPole {
    YELLOW, RED, BLUE, GREEN
  }

  public enum PlayerState {
    JUMPING, IS_GROUNDED, STORE_FORCE
  }

  public class PlayerController : Magnet {
    public float JumpSpeed;
    [SerializeField]
    private GameObject Fail;
    [SerializeField]
    private float MoveSpeed;
    [SerializeField]
    private AnimatorOverrideController[] aocs;
    [SerializeField]
    private float LongJumpTime;
    [SerializeField]
    private float LongJumpRate;
    [SerializeField]
    private GameObject[] Tails;
    [SerializeField]
    private Transform[] TailPos;
    [SerializeField]
    private float tailDeltaTime;
    public PlayerState playerState { get; set; }
    public int life { get; set; } = 1;
    public List<GameObject> magnetList { get; private set; } = new List<GameObject>();
    public bool actionable { get; set; }
    public Vector2 extraVel { get; set; } = Vector2.zero;
    private float m_h;
    private bool m_jumpAction;
    private bool m_jumpClicked;
    private int m_hDirClicked;
    private Vector2 m_colliderSize;
    private float m_oGravity;
    private Rigidbody2D m_rig2D;
    private Animator m_anim;
    private BoxCollider2D m_boxCollider2D;
    private AudioSource m_audioSource;

    private void Start() {
      m_rig2D = GetComponent<Rigidbody2D>();
      m_anim = GetComponent<Animator>();
      m_boxCollider2D = GetComponent<BoxCollider2D>();
      m_audioSource = GetComponent<AudioSource>();
      m_colliderSize = m_boxCollider2D.size;
      m_oGravity = m_rig2D.gravityScale;
      m_rig2D.gravityScale = 0;
    }

    private void OnDestroy() {
      if (Fail) {
        Fail.SetActive(true);
      }
    }

    public void ChangeAnim(int num) {
      life = num;
      m_anim.runtimeAnimatorController = aocs[num - 1];
      Vector2 newSize = m_colliderSize;
      newSize.y *= num;
      m_boxCollider2D.size = newSize;
      Vector2 newOffset = newSize / 2.0f;
      newOffset.x = 0;
      m_boxCollider2D.offset = newOffset;
    }

    public void ChangeMagnet(MagneticPole magneticPole) {
      DownMagneticPole = magneticPole;
    }

    public void DestroyPlayer() {
      Destroy(gameObject);
    }

    private void Update() {
      m_h = _GetHDirButton();
      m_jumpAction = _GetJumpButton();
    }

    private void FixedUpdate() {
      if (actionable) {
        Vector3 angle = transform.eulerAngles;
        if (m_h > 0) {
          angle.y = 0;
        } else if (m_h < 0) {
          angle.y = 180;
        }
        transform.eulerAngles = angle;
        Vector2 vel = m_rig2D.velocity;
        vel.x = m_h * MoveSpeed;
        m_rig2D.velocity = vel + extraVel;
        if (m_jumpAction && playerState == PlayerState.IS_GROUNDED) {
          m_jumpAction = false;
          playerState = PlayerState.STORE_FORCE;
          StartCoroutine(_Jump());
        }
      }
    }

    private IEnumerator _Jump() {
      float startTime = Time.fixedTime;
      while (Time.fixedTime - startTime < LongJumpTime) {
        if (!_GetJumpButton()) {
          break;
        }
        yield return null;
      }
      float rate = Time.fixedTime - startTime < LongJumpTime ? 1 : LongJumpRate;
      Vector3 vel = m_rig2D.velocity;
      vel.y = JumpSpeed * rate;
      m_rig2D.velocity = vel;
      playerState = PlayerState.JUMPING;

      if (!m_anim.GetCurrentAnimatorStateInfo(0).IsName("Break")) {
        Vector3 scale = transform.localScale;
        scale.y *= 0.7f;
        transform.localScale = scale;
        Invoke(nameof(_Recover), 0.1f);
      }

      StartCoroutine(TailEffect());
      m_audioSource.Play();
    }

    public IEnumerator TailEffect() {
      float startTime = Time.fixedTime;
      int index = 0;
      while (index < Tails.Length) {
        if (Time.fixedTime - startTime > tailDeltaTime) {
          Tails[index].transform.position =
              TailPos[m_rig2D.velocity.x == 0 ? 0 : 1].position;
          Tails[index++].GetComponent<Animator>().Play("Tail");
          startTime = Time.fixedTime;
        }
        yield return null;
      }
    }

    public void CanAct() {
      m_rig2D.gravityScale = m_oGravity;
      actionable = true;
    }

    private void _Recover() {
      Vector3 scale = transform.localScale;
      scale.y /= 0.7f;
      transform.localScale = scale;
    }

    public void SetJumpClicked(bool state) {
      m_jumpClicked = state;
    }

    public void SetHDirClicked(int state) {
      m_hDirClicked = state;
    }

    private bool _GetJumpButton() {
      return m_jumpClicked || Input.GetButton("Jump");
    }

    private float _GetHDirButton() {
      if (m_hDirClicked == 0) {
        return Input.GetAxisRaw("Horizontal");
      } else {
        return m_hDirClicked;
      }
    }
  }
}