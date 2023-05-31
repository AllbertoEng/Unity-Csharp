using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void CalculateDanage(ref int damage);
    public void ApplyDamage(int damage);
    public void CheckState();
}
