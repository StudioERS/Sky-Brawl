using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamaging {


    [SerializeField] float DamageValue { get; }
    [SerializeField] float BaseKnockback { get; }
    [SerializeField] float UpwardModifier { get; }
    [SerializeField] float ExplosionRadius { get; }





}
