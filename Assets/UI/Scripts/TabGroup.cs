﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    [SerializeField] private List<TabButton> tabButtons;
    [SerializeField] private Sprite tabHover;
    [SerializeField] private Sprite tabActive;
    [SerializeField] private Sprite tabIdle;
    [SerializeField] private List<GameObject> objectsToSwap;

    private TabButton selectedTab;

    private void Start()
    {
        foreach (GameObject page in objectsToSwap)
        {
            page.SetActive(false);
        }
    }

    public void Subscribe(TabButton button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if(selectedTab == null || button != selectedTab)
        {
            button.tabIcon.sprite = tabHover;
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        if (selectedTab != null && selectedTab == button)
        {
            selectedTab = null;
            TabButton.Deselect(out selectedTab);
        }
        else
        {
            TabButton.Select(out selectedTab, button);
        }

        button.tabIcon.sprite = tabActive;

        ResetTabs();

        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                if (objectsToSwap[i].activeSelf)
                {
                    objectsToSwap[i].SetActive(false);
                    continue;
                }
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach (TabButton button in tabButtons)
        {
            if (selectedTab != null && button == selectedTab) 
            { 
                continue; 
            }
            button.tabIcon.sprite = tabIdle;
        }
    }
}
