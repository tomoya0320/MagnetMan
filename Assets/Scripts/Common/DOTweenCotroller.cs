using UnityEngine;
using DG.Tweening;

namespace HuaYang.Battle {
  public class DOTweenCotroller : MonoBehaviour {
    [SerializeField]
    private float[] StayTimes;
    private Vector3 m_dotSpeed;
    public Vector3 dotSpeed => m_dotSpeed;
    private Vector3 m_bufferSpeed;
    private int m_index;
    private float m_duration;
    private DOTweenAnimation m_dotweenAnim;

    private void Start() {
      m_index = -1;
      m_dotweenAnim = GetComponent<DOTweenAnimation>();
      m_dotweenAnim.DOPause();
      m_duration = m_dotweenAnim.duration;
      m_bufferSpeed = (m_dotweenAnim.endValueV3 - transform.position) / m_dotweenAnim.duration;
      m_dotSpeed = m_bufferSpeed;
      m_index = (m_index + 1) % StayTimes.Length;
      Invoke(nameof(StartDOTween), StayTimes[m_index]);
    }

    private void StartDOTween() {
      m_dotSpeed = m_bufferSpeed;
      m_dotweenAnim.DOPlay();
      Invoke(nameof(PauseDOTween), m_duration);
    }

    private void PauseDOTween() {
      m_dotSpeed = Vector3.zero;
      m_bufferSpeed = -m_bufferSpeed;
      m_dotweenAnim.DOPause();
      m_index = (m_index + 1) % StayTimes.Length;
      Invoke(nameof(StartDOTween), StayTimes[m_index]);
    }
  }
}