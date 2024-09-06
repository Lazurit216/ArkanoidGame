using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BonusEffect : ScriptableObject
{
    [field: SerializeField] public float activeTime { get; protected set; }
    protected bool isActivated = false;
    public abstract void ApplyEffect();
    public abstract void DisableEffect();
    
}
