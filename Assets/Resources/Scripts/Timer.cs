//using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine;

public struct Timer
{
    public Timer (System.Action action, float time)
    {
        this.action = action;
        this.timeLeft = time;
    }

    private readonly System.Action action;
    private float timeLeft;
    public float TimeLeft
    {
        get
        {
            return timeLeft;
        }
        set
        {
            timeLeft = value;

            if (timeLeft < 0)
                action();
        }
    }

    public void Reset(float time)
    {
        TimeLeft = time;
    }

    public static Timer operator -(Timer left, float right)
    {
        Timer replace = left;
        replace.TimeLeft -= right;
        return replace;
    }
}