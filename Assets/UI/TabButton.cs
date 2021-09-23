using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TabGroup tabGroup;

    public Image tabIcon;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    private void Start()
    {
        tabIcon = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    public static void Select(out TabButton selectedTab, TabButton button)
    {
        selectedTab = button;
    }

    public static void Deselect(out TabButton selectedTab)
    {
        selectedTab = null;
    }
}
