using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour 
{
    private int        _money;
    private float      _food = 6;
    private Ship       _ship = new Ship(500, 2.5f, 50, 2, 500);
    private List<UnitStats> _team = new List<UnitStats> { new UnitStats("Jacson", 15, 2, 1.3f) };
    private Dictionary<UnitStats, int> _unitShop;

    public Ship ship
    {
        get
        {
            return _ship;
        }
    }

    public List<UnitStats> team
    {
        get
        {
            return _team;
        }
    }

    private void Awake()
    {
        _money = 1000000;
        //_ship = new Ship(10000, 5, 5, 3, 1000);
        //_team = new List<Unit>();    

        GenerateUnitShop();
    }

    private void GenerateUnitShop()
    {
        _unitShop = new Dictionary<UnitStats, int>
        {
            {new UnitStats("Lucas", 10, 1, 1), 10},
            {new UnitStats("Jacson", 15, 2, 1.3f), 20},
            {new UnitStats("Micle", 15, 3, 2), 25 },
            {new UnitStats("Jordge", 100, 50, 10), 1000 }
        };
    }

    //private void OnGUI()
    //{
    //    GUI.Label(new Rect(0, Screen.height - 30, 100, 30), _money.ToString());
    //
    //    for(int i = 0; i < _team.Count;i++)
    //    {
    //        GUI.Box(new Rect(30, 30 * i, 100, 30), _team[i].ToString());
    //    }
    //
    //    var shop = _unitShop.GetEnumerator();
    //    shop.MoveNext();
    //
    //    for(int i = 0; i < _unitShop.Count; i++)
    //    {
    //        if (GUI.Button(new Rect(Screen.width / 2, 30 * i, 100, 30), shop.Current.Key.ToString()))
    //        {
    //            _team.Add(shop.Current.Key);
    //            _money -= shop.Current.Value;
    //            _unitShop.Remove(shop.Current.Key);
    //        }
    //
    //        shop.MoveNext();
    //    }
    //}
}

public struct Ship
{
    public Ship(short hitPoints, float speed,float space, byte places, int price)
    {
        _hitPoints   =  hitPoints;
        _speed       =  speed;
        _space       =  space;
        _places      =  places;
        _price       =  price;
    }

    private short _hitPoints;
    private float _speed;
    private float _space;
    private byte  _places;
    private int   _price;

    public short hitPoints
    {
        get
        {
            return _hitPoints;
        }

        set
        {
            _hitPoints = value;
        }
    }

    public float speed
    {
        get
        {
            return _speed;
        }

        set
        {
            _speed = value;
        }
    }

    public float space
    {
        get
        {
            return _space;
        }

        set
        {
            _space = value;
        }
    }

    public byte places
    {
        get
        {
            return _places;
        }

        set
        {
            _places = value;
        }
    }

    public int price
    {
        get
        {
            return _price;
        }

        set
        {
            _price = value;
        }
    }
}

public struct UnitStats
{
    public UnitStats(string name, short hitPoints, short damage, float speed)
    {
        _name       = name;
        _hitPoinsts = hitPoints;
        _damage     = damage;    
        _speed      = speed;     
    }

    private string _name;
    private short  _hitPoinsts;
    private short  _damage;
    private float  _speed;

    public string name
    {
        get 
        {
            return _name;
        }
        set 
        {
            _name = value;    
        }
    }

    public short hitPoinsts
    {
        get
        {
            return _hitPoinsts;
        }

        set
        {
            _hitPoinsts = value;
        }
    }

    public short damage
    {
        get
        {
            return _damage;
        }

        set
        {
            _damage = value;
        }
    }

    public float speed
    {
        get
        {
            return _speed;
        }

        set
        {
            _speed = value;
        }
    }

    public override string ToString()
    {
        return name + ": " + hitPoinsts + ", " + _damage + ", " + _speed;
    }
}
