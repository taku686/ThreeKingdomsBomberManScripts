using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    public int DamageCaused = 10;
    public float InvincibilityDuration = 0.5f;
    public int DamageTakenEveryTime = 0;
    public int DamageTakenDamageable = 0;
    public float DamageTakenInvincibilityDuration = 0.5f;
    public LayerMask TargetLayerMask;
}
