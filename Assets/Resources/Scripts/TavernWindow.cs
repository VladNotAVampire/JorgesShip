using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;

public class TavernWindow : UnitList
{
    private List<UnitStats>  _units = new List<UnitStats>() {
                                                    new UnitStats("Lucas", 10, 1, 1),
                                                    new UnitStats("Randy", 10, 2, 1.5f),
                                                    new UnitStats("Jacson", 15, 2, 1.3f),
                                                    new UnitStats("Micle", 15, 3, 2),
                                                    new UnitStats("Jordge", 100, 50, 10)
                                                  };

    protected override int maxPanelsOnWindow
    {
        get
        {
            return 4;
        }
    }

    protected override GameObject panel
    {
        get
        {
            return (GameObject)Resources.Load("Prefabs/GuyPanel");
        }
    }
    public override List<UnitStats> units
    {
        get
        {
            return _units;
        }
    }

    public override GameObject AddUnitPanel(UnitStats unit)
    {
        var newPanel = base.AddUnitPanel(unit);

        var guyPanel = newPanel.GetComponent<GuyPanel>();
        guyPanel.window = this;

        return newPanel;
    }
}