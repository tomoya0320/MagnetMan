using UnityEngine;
using DG.Tweening;

public class JumpMagnet : Magnet
{
    public float JumpRate;
    public bool movable;
    private bool isPlayerStay;
    private DOTweenCotroller dotweenCotroller;
    private AudioSource audioSource;
    private PlayerController playerController;
    private Rigidbody2D rigid2D;
    private void Start()
    {
        isPlayerStay = false;
        dotweenCotroller = GetComponent<DOTweenCotroller>();
        audioSource = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        if (playerController)
        {
            if (isPlayerStay)
            {
                playerController.extraVel = dotweenCotroller.dotSpeed;
            }
            else
            {
                playerController.extraVel = Vector2.zero;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController = collision.GetComponent<PlayerController>();
            rigid2D = collision.GetComponent<Rigidbody2D>();
            if (playerController.DownMagneticPole != UpMagneticPole)
            {
                makePlayerJump();
                Vector3 scale = transform.localScale;
                scale.y *= 0.8f;
                transform.localScale = scale;
                Invoke("recover", 0.1f);
            }
            else if (movable)
            {
                isPlayerStay = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (movable && collision.CompareTag("Player"))
        {
            isPlayerStay = false;
        }
    }
    private void recover()
    {
        Vector3 scale = transform.localScale;
        scale.y /= 0.8f;
        transform.localScale = scale;
    }
    private void makePlayerJump()
    {
        playerController.playerState = PlayerState.Jumping;
        Vector2 vel = rigid2D.velocity;
        vel.y = playerController.JumpSpeed * JumpRate;
        rigid2D.velocity = vel;
        StartCoroutine(playerController.tailEffect());
        audioSource.Play();
    }
}