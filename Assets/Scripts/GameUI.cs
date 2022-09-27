using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance;
    
    private IPanel[] _panels;

    private IPanel _currentPanel;
    
    private void Awake()
    {
        Instance = this;

        _panels = GetComponentsInChildren<IPanel>(true);
    }

    public void EnablePanel(string panelID)
    {
        IPanel nextPanel = FindPanel(panelID);

        if (nextPanel != null)
        {
            if(_currentPanel != null)
                _currentPanel.Disable();
            
            _currentPanel = nextPanel;
            _currentPanel.Enable();
        }
    }

    private IPanel FindPanel(string id)
    {
        for (int i = 0; i < _panels.Length; i++)
        {
            if (id == _panels[i].PanelName)
                return _panels[i];
        }

        return null;
    }
}
