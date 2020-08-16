using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Success : MonoBehaviour
{
    public float DurationTime;
    public string NextScene;
    public GameObject successLogo;
    public GameObject player;
    private DOTweenPath playerDotPath;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.GetComponent<PlayerController>().actionable)
        {
            //通关界面
            player = collision.gameObject;
            player.GetComponent<PlayerController>().actionable = false;
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            player.GetComponent<Rigidbody2D>().gravityScale = 0;          
            player.GetComponent<BoxCollider2D>().enabled = false;
            Vector3 position = player.transform.position;
            position.y = transform.position.y;
            player.transform.position = position;
            position.x = transform.position.x;
            player.transform.DOMove(position, DurationTime);
            Invoke("gotoNextScene", DurationTime);
        }
    }
    private void gotoNextScene()
    {
        if (NextScene != "")
        {
            SceneManager.LoadScene(NextScene);
        }
        else
        {
            foreach(AudioSource audio in Camera.main.GetComponents<AudioSource>())
            {
                audio.Stop();
            }
            successLogo.SetActive(true);
            successLogo.GetComponent<AudioSource>().Play();
            NextScene = "MainScene";
            Invoke("gotoNextScene", 5.0f);
        }
    }
}