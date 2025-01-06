using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public abstract Task OnDeath();
}

public enum EnemyState
{
    Idle,
    Discarding,
    Dying,
    Die
}