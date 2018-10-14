using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour {

    /*K = b * (  (1 + d/10)    *    (x^sqrt(c*d))   )
     * K = Final knockback
     * b = Projectile's base knockback
     * x = exponentialBase
     * c = exponentialCoefficient
     * d = damage
    */
    [SerializeField] float exponentialBase = 1.04f;                 //x
    [SerializeField] float exponentialCoefficient = 1f;             //c
    public float damage = 0f;                                       //d

    new Rigidbody rigidbody;
    float upwardModifier;

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
        upwardModifier = projectileComponent.upwardModifier;

        //If it's not a collision with one of our projectile components (e.g. with terrain), return.
        if (projectileComponent == null)
        {
            return;
        }

        //Increments damage
        damage += projectileComponent.damageValue;

        //Building the knockback formula
        float sqrtOfDamage = Mathf.Sqrt(damage);
        float linearKBAmp = (1 + damage / 10);
        float quadraticKBAmp = Mathf.Pow(exponentialBase, exponentialCoefficient * sqrtOfDamage);

        //Setting a maximum, if we want one.
        float knockbackAmplification = Mathf.Clamp(quadraticKBAmp, 1f, 50f);

        //Calculating effective knockback
        float effectiveKnockback = projectileComponent.baseKnockback * knockbackAmplification;

        //Calculates first collision point. Todo: improve responsiveness by finding the virtual center.
        Vector3 firstContact = collision.contacts[0].point;
        Vector3 firstContactNormal = collision.contacts[0].normal;

        //Adds upward movement to the force so that objects don't juste slide around
        Vector3 adjustedNormal = (firstContactNormal) + new Vector3(0, upwardModifier, 0);

        //Adds force. WARNING DEPENDENT ON PLAYER RIGIDBODY'S MASS.
        rigidbody.AddForce(adjustedNormal * effectiveKnockback, ForceMode.Impulse);
    }
}
