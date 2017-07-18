using UnityEngine;
using System.Collections.Generic;


namespace Assets.Resources.Scripts.Battle
{
    public class Sword : IMelee
    {
        public short Damage { get; private set;} 
        public float Distance { get; private set;}
        public float Speed { get; private set;}
        public bool ForTwoHands { get; private set;}

        public Sword()
        {
            Damage = 1;
            Distance = World.ONE / 2;
            Speed = 1;
        }
    }
}
