using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : Projectile {

    [SerializeField] protected float explosionRadius;
    new Rigidbody rigidbody;

    protected override void Start()
    {
        base.Start();
        rigidbody = GetComponent<Rigidbody>();
        hitParticle.transform.localScale = new Vector3(explosionRadius, explosionRadius, explosionRadius);
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        rigidbody.detectCollisions = false;
        RaycastHit[] hits = Physics.SphereCastAll(rigidbody.centerOfMass, explosionRadius, rigidbody.centerOfMass);
        if (hits.Length != 0)
        {

            foreach (RaycastHit hit in hits)
            {
                DamageHandler targetDH = hit.transform.GetComponent<DamageHandler>();

                if (targetDH == null) { continue; }

                Rigidbody otherRigidbody = hit.transform.GetComponent<Rigidbody>();

                targetDH.damage += damageValue;

                //Building the knockback formula
                float sqrtOfDamage = Mathf.Sqrt(damageValue);
                float linearKBAmp = (1 + damageValue / 10);
                float quadraticKBAmp = Mathf.Pow(targetDH.exponentialBase, targetDH.exponentialCoefficient * sqrtOfDamage);

                float knockbackAmplification = Mathf.Clamp(quadraticKBAmp, 1f, 100f);

                //Calculating effective knockback
                float effectiveKnockback = baseKnockback * knockbackAmplification;

                otherRigidbody.AddExplosionForce(effectiveKnockback, rigidbody.centerOfMass, explosionRadius, upwardModifier, ForceMode.Impulse);
            }
        }
        CancelInvoke();
        SelfDestruct();
    }
}
