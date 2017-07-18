using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Resources.Scripts.Battle
{
    public interface IUnit
    {
        UnitStats Stats { get; set; }
        IAI AI { get; set; }
        Transform transform { get; }
        IUnit Target { get; set; }
        Vector3 Destination { get; set; }
        State state { get; set; }
        short hp { get; set; }
        IWeapon Weapon { get; }

        void Die();
    }

    public enum State
    {
        None, Attack, Hide, Going
    }
}
