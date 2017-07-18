using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;

public class TeamWindow : UnitList
{
    private Inventory _inventory;

    protected override int maxPanelsOnWindow
    {
        get
        {
            return 3;
        }
    }

    protected override GameObject panel
    {
        get
        {
            return (GameObject)Resources.Load("Prefabs/UnitPanel");
        }
    }
    public override List<UnitStats> units
    {
        get
        {
            return _inventory.team;
        }
    }


    private void Awake()
    {
        _inventory = FindObjectOfType<Inventory>();
        _scrollbar = transform.GetComponentInChildren<Scrollbar>();
        _list      = transform.GetComponentsInChildren<Transform>()
                              .Where(component => component.name == "List")
                              .First();
    }
}