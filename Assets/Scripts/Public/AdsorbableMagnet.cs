using UnityEngine;
using DG.Tweening;

public class AdsorbableMagnet : Magnet
{
    private AudioSource audioSource;
    private Vector3 oPos;
    private GameObject head;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        head = transform.GetChild(0).gameObject;
        oPos = head.transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();
        if (collision.tag == "Player" && playerController)
        {       
            if (playerController.DownMagneticPole == UpMagneticPole && playerController.life < 3)
            {
                playerController.magnetList.Add(gameObject);
                playerController.DownMagneticPole = DownMagneticPole;
                playerController.changeAnim(++playerController.life);
                foreach(BoxCollider2D boxCollider2D in GetComponents<BoxCollider2D>())
                {
                    boxCollider2D.enabled = false;
                }
                GetComponent<Animator>().Play("NoMagnet");
                head.transform.position = oPos;
                head.transform.DOMoveY(oPos.y - 0.5f, 0.5f);
                head.GetComponent<SpriteRenderer>().color = Color.white;
                head.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 0.5f);
                audioSource.Play();
            }
        }
    }
    public void EnableCollision()
    {
        foreach (BoxCollider2D boxCollider2D in GetComponents<BoxCollider2D>())
        {
            boxCollider2D.enabled = true;
        }
    }
}
