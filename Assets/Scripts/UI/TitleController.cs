using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HuaYang.UI {
  public class TitleController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField]
    private Sprite normal;
    [SerializeField]
    private Sprite hold;
    private Image _image;
    public Image image {
      get {
        if (_image == null) {
          _image = GetComponent<Image>();
        }
        return _image;
      }
    }

    public void OnPointerEnter(PointerEventData eventData) {
      image.sprite = hold;
    }

    public void OnPointerExit(PointerEventData eventData) {
      image.sprite = normal;
    }
  }
}