using System.Collections;
using UnityEngine;

public class JumpJudge : MonoBehaviour
{
    private Coroutine coroutine; 
    private PlayerController pc;
    private Animator anim;
    private void Start()
    {
        anim = transform.parent.GetComponent<Animator>();
        pc = transform.parent.GetComponent<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pc.playerState != PlayerState.StoreForce)
        {
            JumpMagnet jumpMagnet = collision.GetComponent<JumpMagnet>();
            Magnet magnet = collision.GetComponent<Magnet>();
            if (jumpMagnet && magnet)
            {
                if (magnet.UpMagneticPole == pc.DownMagneticPole)
                {
                    pc.playerState = PlayerState.IsGrounded;
                }
            }
            else
            {
                pc.playerState = PlayerState.IsGrounded;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (pc.playerState != PlayerState.StoreForce)
        {
            pc.playerState = PlayerState.Jumping;
        }
    }
}