using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Resources.Scripts.Battle
{
    public interface IAI
    {
        IUnit FindPriorityTarget();

        bool ShouldAttack();

        bool ShouldHide();

        State WhatToDo();
    }
}
