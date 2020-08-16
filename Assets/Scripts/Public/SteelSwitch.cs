using UnityEngine;
using DG.Tweening;

public class SteelSwitch : Magnet
{
    public Sprite[] sprites;
    public Transform Steel;
    public AudioClip[] audios;
    public bool isOpen;
    private SpriteRenderer spriteRenderer;
    private Vector3 angle;
    private AudioSource steelAS;
    private AudioSource audioSource;
    private DOTweenAnimation steelDOTweenAnim;

    private void Start()
    {
        angle = Vector3.zero;
        spriteRenderer = GetComponent<SpriteRenderer>();
        steelAS = Steel.GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        steelDOTweenAnim = Steel.GetComponent<DOTweenAnimation>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();
        if (collision.tag == "Player" && playerController)
        {
            if (playerController.DownMagneticPole == UpMagneticPole)
            {
                isOpen = !isOpen;
                if (isOpen)
                {
                    steelDOTweenAnim.DOPlay();
                }
                else
                {
                    steelDOTweenAnim.DOPause();
                }
                spriteRenderer.sprite = sprites[isOpen ? 1 : 0];
                audioSource.clip = audios[0];
                audioSource.Play();
            }
            else
            {
                spriteRenderer.sprite = sprites[2];
                audioSource.clip = audios[1];
                audioSource.Play();
            }
        }
    }
}