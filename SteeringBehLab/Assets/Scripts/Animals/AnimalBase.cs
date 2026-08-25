using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public abstract class AnimalBase : SteeringBehaviour, IKillable
{
    public event Action<AnimalBase> OnDeath;

    public void Kill()
    {
        OnDeath?.Invoke(this);
        Destroy(gameObject);
    }

    protected override List<UnityMovingState> GetMovingStates()
    {
        throw new System.NotImplementedException();
    }
}