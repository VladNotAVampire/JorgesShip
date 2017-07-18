using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Resources.Scripts.Battle
{
    public interface IWeapon
    {
        short Damage { get; } 
        float Distance { get; }
        float Speed { get; }
        bool ForTwoHands { get; }
    }

    public interface IMelee : IWeapon
    {
    }

    public interface IRangeWeapon : IWeapon
    {
        float Accuracy { get; }
    }

    public interface IThrowable : IRangeWeapon
    {
        Vector2[] Traecktory { get; }
    }

    public interface IBoomable
    {
        float BoomRange { get; }
    }

    public interface IAmmo
    {
        float Damage { get; }
    }
}
