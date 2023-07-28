using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject itemDescription;

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemDescription.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemDescription.SetActive(false);
    }
}
