using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour {


    [SerializeField] float exponentialBase = 1.05f;
    [SerializeField] float exponentialCoefficient = 1f;

    public float damage = 0f;
    new Rigidbody rigidbody;
    [SerializeField] float upwardModifier = 1f;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherRigidbody = collision.rigidbody;
        Projectile projectileComponent = otherRigidbody.GetComponent<Projectile>();

        if (projectileComponent == null)
        {
            return;
        }

        damage += projectileComponent.damageValue;

        float rawKnockbackAmplification = Mathf.Pow(exponentialBase, exponentialCoefficient * damage);
        float knockbackAmplification = Mathf.Clamp(rawKnockbackAmplification, 1f, 100f);
        float effectiveKnockback = projectileComponent.baseKnockback + knockbackAmplification;

        Vector3 otherCenterOfMass = otherRigidbody.centerOfMass;
        Vector3 firstContact = collision.contacts[0].point;
        Vector3 firstContactNormal = collision.contacts[0].normal;

        Vector3 adjustedNormal = (firstContactNormal) + new Vector3(0, upwardModifier, 0);

        Debug.DrawRay(firstContact, adjustedNormal, Color.red, 10f);
        Debug.DrawRay(firstContact, firstContactNormal, Color.blue, 10f);
        rigidbody.AddForce(adjustedNormal * effectiveKnockback, ForceMode.Impulse);
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
