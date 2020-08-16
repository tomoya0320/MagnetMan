using UnityEngine;

public class Fail : MonoBehaviour
{
    public GameObject menu;
    public GameObject pause;

    private void OnEnable()
    {
        pause.SetActive(false);
        GetComponent<AudioSource>().Play();
        Invoke("showMenu", 3.0f);
    }
    private void showMenu()
    {
        menu.SetActive(true);
        gameObject.SetActive(false);
    }
}
