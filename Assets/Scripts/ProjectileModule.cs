using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileModule : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public interface ICanDamage
{
    float DamageValue { get; }
}

public interface ICanKnockback
{
    float BaseKnockback { get; }
}

public abstract class Projectile
{
    int ammoCost;
}

public class HitScanDamageProjectile : ICanDamage
{
    private float _damageValue;
    public float DamageValue
    {
        get
        {
            return _damageValue;
        }
    }
}

public class LineDamageKnockBackProjectile : Projectile, ICanDamage, ICanKnockback
{
    public float _damageValue;
    public float _baseKnockback;
    public float DamageValue
    {
        get
        {
            return _damageValue;
        }
    }

    public float BaseKnockback
    {
        get
        {
            return _baseKnockback;
        }
    }
}
