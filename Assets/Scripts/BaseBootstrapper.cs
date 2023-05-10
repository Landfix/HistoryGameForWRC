using System;
using UnityEngine;

public abstract class BaseBootstrapper : MonoBehaviour
{
    public abstract event Action Won;
    public abstract event Action Incorrected;
    public abstract void ShowHint();
}