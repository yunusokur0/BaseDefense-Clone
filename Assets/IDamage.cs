using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage 
{
    int Heal { get; set; }
    void TakeDamage(int damage);
}
