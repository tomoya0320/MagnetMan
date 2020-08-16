using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class TitleController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite normal;
    public Sprite hold;
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = hold;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = normal;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
