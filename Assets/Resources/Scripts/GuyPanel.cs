using UnityEngine;
using UnityEngine.UI;

public class GuyPanel : UnitPanel
{
    private Inventory _inventory;

    public  UnitStats         _unit;
    public  TavernWindow window;

    public void OnClick()
    {
        _inventory.team.Add(_unit);
        window.units.Remove(_unit);
        window.UpdateScrollbar();
        //window.UpdateList();
    }

    private void Start()
    {
        _inventory = FindObjectOfType<Inventory>();
        //_unit = new Unit("Randy", 10, (short)Random.Range(1, 3.9f), 1.5f);
    }

    public override void Set(UnitStats unit)
    {
        _unit = unit;

        base.Set(unit);
    }
}
