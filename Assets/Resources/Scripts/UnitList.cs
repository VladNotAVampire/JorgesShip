using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public abstract class UnitList : MonoBehaviour
{
    protected Scrollbar _scrollbar;
    protected Transform _list;

    protected abstract int maxPanelsOnWindow {get;}
    protected abstract GameObject panel {get;}
    public abstract List<UnitStats> units {get;}

    public virtual GameObject AddUnitPanel(UnitStats unit)
    {
        var newPanel = (GameObject) Instantiate(panel, _list);
        var panelComponent = newPanel.GetComponent<UnitPanel>();
        newPanel.transform.localScale = Vector3.one;
        panelComponent.Set(unit);

        UpdateScrollbar();
        return newPanel;
    }

    protected void LoadList()
    {
        foreach(var unit in units)
        {
            AddUnitPanel(unit);
        }

        UpdateScrollbar();
    }

    protected void ClearList()
    {
        for(int i = 0; i < _list.childCount; i++)
        {
            Destroy(_list.GetChild(i).gameObject);
        }
    }

    public void UpdateScrollbar()
    {
        _scrollbar.size = maxPanelsOnWindow / units.Count;
    }

    private void Awake()
    {
        _scrollbar = transform.GetComponentInChildren<Scrollbar>();
        _list      = transform.GetComponentsInChildren<Transform>()
                              .Where(component => component.name == "List")
                              .First();
    }

    private void OnEnable()
    {
        LoadList();
    }

    private void OnDisable()
    {
        ClearList();
    }
}