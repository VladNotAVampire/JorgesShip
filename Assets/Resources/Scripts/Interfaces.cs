using UnityEngine;
using System.Collections.Generic;

public interface IOnClickSubscribed
{
    void OnClick();
    //void OnClick(Vector3 mousePosition);
}

public interface IUnitList
{
    List<UnitStats> units {get;}
    void UpdateList(int scrolled);
}

public interface ISpawner
{
    GameObject Spawn(GameObject prefab);
}
