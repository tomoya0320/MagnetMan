using UnityEngine;
using DG.Tweening;

public class DOTweenCotroller : MonoBehaviour
{
    public float[] StayTimes;
    [HideInInspector]
    public Vector3 dotSpeed;
    private Vector3 bufferSpeed;
    private int index;
    private float duration;
    private DOTweenAnimation dotweenAnim;
    // Start is called before the first frame update
    void Start()
    {
        index = -1;
        dotweenAnim = GetComponent<DOTweenAnimation>();
        dotweenAnim.DOPause();
        duration = dotweenAnim.duration;
        bufferSpeed = (dotweenAnim.endValueV3 - transform.position) / dotweenAnim.duration;
        dotSpeed = bufferSpeed;
        index = (index + 1) % StayTimes.Length;
        Invoke("startDOTween", StayTimes[index]);
    }
    private void startDOTween()
    {
        dotSpeed = bufferSpeed;
        dotweenAnim.DOPlay();
        Invoke("pauseDOTween", duration);
    }
    private void pauseDOTween()
    {
        dotSpeed = Vector3.zero;
        bufferSpeed = -bufferSpeed;
        dotweenAnim.DOPause();
        index = (index + 1) % StayTimes.Length;
        Invoke("startDOTween", StayTimes[index]);
    }
}