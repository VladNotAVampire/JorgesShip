using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Resources.Scripts.Battle
{
    public abstract class ArtificalIntelect : IAI
    {
        public IUnit unit { get; set; }

        public virtual IUnit FindPriorityTarget()
        {
            return Object.FindObjectOfType<BattleManager>()
                    .AnotherTeam(unit)
                    .OrderBy(u => Vector3.Distance(unit.transform.position, u.transform.position))
                    .FirstOrDefault();
        }

        public virtual bool ShouldAttack()
        {
            return true;
        }

        public virtual bool ShouldHide()
        {
            if (unit.hp < unit.Stats.hitPoinsts * 0.25f)
            {
                if (unit.hp <= unit.Target.Weapon.Damage && unit.Target.hp > unit.Weapon.Damage)
                {
                    return true;
                }
            }    

            return false;
        }

        public virtual State WhatToDo()
        {
            if (unit.Target != null)
            {
                return State.Attack;
            }

            return State.None;
        }

        public static IAI GetAI(IUnit unit)
        {
            //if (unit.Weapon is IMelee)
            {
                var newIntelect = new MeleeUnitAI();
                newIntelect.unit = unit;
                
                return newIntelect;
            }

            return null;
        }
    }

    public class MeleeUnitAI : ArtificalIntelect
    {
        public override State WhatToDo()
        {
            if (unit.Target != null)
            {
                if (ShouldHide())
                {
                    return State.Hide;
                }

                if (Vector3.Distance(unit.transform.position, unit.Target.transform.position) < unit.Weapon.Distance)
                {
                    return State.Attack;
                }

                return State.Going;
            }

            //if (unit.Target == null)
                unit.Target = FindPriorityTarget();

            return State.None;
        }
    }
}
