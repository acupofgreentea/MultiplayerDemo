using System.Collections.Generic;
using Fusion;
using UnityEngine;

public abstract class StateControllerBase<T, T1> : NetworkBehaviour where T : System.Enum
{
    public T1 CurrentState { get; protected set; }

    protected Dictionary<T, T1> stateDictionary;
    protected abstract void CreateDictionary();

    public abstract void ChangeState(T type);
}