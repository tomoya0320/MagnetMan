using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MagneticPole
{
    Yellow,
    Red,
    Blue,
    Green
}
public enum PlayerState
{
    Jumping,
    IsGrounded,
    StoreForce
}
public class PlayerController : Magnet
{
    public GameObject Fail;
    public float JumpSpeed;
    public float MoveSpeed;
    public AnimatorOverrideController[] aocs;
    public float LongJumpTime;
    public float LongJumpRate;
    public GameObject[] Tails;
    public Transform[] TailPos;
    public float tailDeltaTime;
    public PlayerState playerState;
    [HideInInspector]
    public int life;
    [HideInInspector]
    public List<GameObject> magnetList;
    [HideInInspector]
    public bool actionable;
    [HideInInspector]
    public Vector2 extraVel;
    private float h;
    private bool jumpAction;
    private bool jumpClicked;
    private int hDirClicked;
    private Vector2 colliderSize;
    private float oGravity;
    private Rigidbody2D rig2D;
    private Animator anim;
    private BoxCollider2D boxCollider2D;
    private AudioSource audioSource;
    void Start()
    {
        life = 1;
        hDirClicked = 0;
        actionable = false;
        jumpAction = false;
        jumpClicked = false;
        extraVel = Vector2.zero;
        magnetList = new List<GameObject>();
        rig2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        colliderSize = boxCollider2D.size;
        oGravity = rig2D.gravityScale;
        rig2D.gravityScale = 0;
    }
    private void OnDestroy()
    {
        if (Fail)
        {
            Fail.SetActive(true);
        }
    }
    public void changeAnim(int num)
    {
        life = num;
        anim.runtimeAnimatorController = aocs[num - 1];
        Vector2 newSize = colliderSize;
        newSize.y *= num;
        boxCollider2D.size = newSize;
        Vector2 newOffset = newSize / 2.0f;
        newOffset.x = 0;
        boxCollider2D.offset = newOffset;
    }
    public void changeMagnet(MagneticPole magneticPole)
    {
        DownMagneticPole = magneticPole;
    }
    public void destroyPlayer()
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        h = getHDirButton();
        jumpAction = getJumpButton();
    }
    void FixedUpdate()
    {
        if (actionable)
        {
            Vector3 angle = transform.eulerAngles;
            if (h > 0)
            {
                angle.y = 0;
            }
            else if (h < 0)
            {
                angle.y = 180;
            }
            transform.eulerAngles = angle;
            Vector2 vel = rig2D.velocity;
            vel.x = h * MoveSpeed;
            rig2D.velocity = vel + extraVel;
            if (jumpAction && playerState == PlayerState.IsGrounded)
            {
                jumpAction = false;
                playerState = PlayerState.StoreForce;
                StartCoroutine(Jump());
            }
        }
    }
    private IEnumerator Jump()
    {
        float startTime = Time.fixedTime;
        while(Time.fixedTime - startTime < LongJumpTime)
        {
            if (!getJumpButton())
            {
                break;
            }
            yield return null;
        }
        float rate = Time.fixedTime - startTime < LongJumpTime ? 1 : LongJumpRate;
        Vector3 vel = rig2D.velocity;
        vel.y = JumpSpeed * rate;
        rig2D.velocity = vel;
        playerState = PlayerState.Jumping;

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Break"))
        {
            Vector3 scale = transform.localScale;
            scale.y *= 0.7f;
            transform.localScale = scale;
            Invoke("recover", 0.1f);
        }

        StartCoroutine(tailEffect());
        audioSource.Play();
    }
    public IEnumerator tailEffect()
    {
        float startTime = Time.fixedTime;
        int index = 0;
        while(index < Tails.Length)
        {
            if(Time.fixedTime - startTime > tailDeltaTime)
            {
                Tails[index].transform.position = 
                    TailPos[rig2D.velocity.x == 0 ? 0 : 1].position;
                Tails[index++].GetComponent<Animator>().Play("Tail");
                startTime = Time.fixedTime;
            }
            yield return null;
        }
    }
    public void canAct()
    {
        rig2D.gravityScale = oGravity;
        actionable = true;
    }
    private void recover()
    {
        Vector3 scale = transform.localScale;
        scale.y /= 0.7f;
        transform.localScale = scale;
    }
    public void setJumpClicked(bool state)
    {
        jumpClicked = state;
    }
    public void setHDirClicked(int state)
    {
        hDirClicked = state;
    }
    private bool getJumpButton()
    {
        return jumpClicked || Input.GetButton("Jump");
    }
    private float getHDirButton()
    {
        if(hDirClicked == 0)
        {
            return Input.GetAxisRaw("Horizontal");
        }
        else
        {
            return hDirClicked;
        }
    }
}