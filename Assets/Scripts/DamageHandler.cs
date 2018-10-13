using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour {


    [SerializeField] float exponentialBase = 1.05f;
    [SerializeField] float exponentialCoefficient = 1f;

    public float damage = 0f;
    new Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(GameObject source, Vector3 intersection)
    {
        GunBase enemyWeapon = source.GetComponentInParent<GunBase>();

        damage += enemyWeapon.damageValue;
        float knockbackAmplification = Mathf.Pow(exponentialBase, exponentialCoefficient * damage);
        float effectiveKnockback = enemyWeapon.baseKnockback * knockbackAmplification;

        rigidbody.AddExplosionForce(effectiveKnockback,intersection, 0.1f, 0f, ForceMode.Impulse);
    }

    public void TakeDamage(Projectile source, Vector3 intersection)
    {
        damage += source.damageValue;
        float knockbackAmplification = Mathf.Pow(exponentialBase, exponentialCoefficient * damage);
        float effectiveKnockback = source.baseKnockback * knockbackAmplification;

        rigidbody.AddForce(intersection * effectiveKnockback, ForceMode.Impulse);
    }
}
