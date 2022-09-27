using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PannelBase : MonoBehaviour, IPanel
{
    [SerializeField] private string panelName;

    public string PanelName
    {
        get { return panelName; }
    }

    public abstract void Enable();
    public abstract void Disable();
}
