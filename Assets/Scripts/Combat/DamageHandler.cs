using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DamageHandler : NetworkBehaviour {

    /*K = b * (  (1 + d/10)    *    (x^sqrt(c*d))   )
     * K = Final knockback
     * b = Projectile's base knockback
     * x = exponentialBase
     * c = exponentialCoefficient
     * d = damage
    */
    [SerializeField] public float exponentialBase = 1.05f;                 //x
    [SerializeField] public float exponentialCoefficient = 1f;             //c

    [SyncVar]
    public float damage = 0f;                                              //d

    new Rigidbody rigidbody;
    float upwardModifier;

    public float MushForce;

    public Text DamagedHandler;
    public Color InitialColor;

    float TimeWhenPickUp;
    float Duration;

    public bool DommageBlock;

    public float DurationTime;
    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        InitialColor = DamagedHandler.color;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    //private void OnTriggerEnter(Collider other)
    //{
    //    Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
    //    Projectile projectileComponent = otherRigidbody.GetComponent<Projectile>();

    //    //If it's not a collision with one of our projectile components (e.g. with terrain), return.
    //    if (projectileComponent == null)
    //    {
    //        return;
    //    }

    //    upwardModifier = projectileComponent.upwardModifier;
    //    damage += projectileComponent.damageValue;

    //    //Building the knockback formula
    //    float sqrtOfDamage = Mathf.Sqrt(damage);
    //    float linearKBAmp = (1 + damage / 10);
    //    float quadraticKBAmp = Mathf.Pow(exponentialBase, exponentialCoefficient * sqrtOfDamage);

    //    float knockbackAmplification = Mathf.Clamp(quadraticKBAmp, 1f, 100f);

    //    //Calculating effective knockback
    //    float effectiveKnockback = projectileComponent.baseKnockback * knockbackAmplification;

    //    rigidbody.AddExplosionForce(effectiveKnockback, otherRigidbody.centerOfMass, 2f, upwardModifier, ForceMode.Impulse);
    //}

    private void OnParticleCollision(GameObject other)
    {
        Projectile projectileComponent = other.GetComponent<Projectile>();
        if (projectileComponent == null) { return; }

        ParticleSystem otherParticle = other.GetComponent<ParticleSystem>();
        ParticleSystem.CollisionModule otherPartCollisionModule = otherParticle.collision;
        Transform otherTransform = other.GetComponent<Transform>();

        //If we want particle speed to affect knockback, we can use this.
        float particleSpeed = otherParticle.velocityOverLifetime.speedModifierMultiplier;

        upwardModifier = projectileComponent.upwardModifier;

        //Increments damage
        damage += projectileComponent.damageValue;

        //Building the knockback formula
        float sqrtOfDamage = Mathf.Sqrt(damage);
        float linearKBAmp = (1 + damage / 10);
        float quadraticKBAmp = Mathf.Pow(exponentialBase, exponentialCoefficient * sqrtOfDamage);


        float knockbackAmplification = linearKBAmp * quadraticKBAmp;

        //Calculating effective knockback
        float effectiveKnockback = projectileComponent.baseKnockback *  knockbackAmplification;

        print((otherTransform.position - rigidbody.position).magnitude + " " + projectileComponent.explosionRadius);

        rigidbody.AddExplosionForce(effectiveKnockback, otherTransform.position, projectileComponent.explosionRadius, upwardModifier, ForceMode.Impulse);
    }


    private void FixedUpdate()
    {
        DamagedHandler.text = damage.ToString() + "%";

        if(TimeWhenPickUp != 0)
        {
            if(Duration < Time.time)
            {
                TimeWhenPickUp = 0;
                Duration = 0;
                DommageBlock = false;
                DamagedHandler.color = InitialColor;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherRigidbody = collision.rigidbody;

        //If it's not a collision with one of our projectile components (e.g. with terrain), return.
        if (collision.gameObject.tag == "Mushroom")
        {
            //CmdApplyKnockback(adjustedDirection, effectiveKnockback);
            rigidbody.AddForce(0f, MushForce, 0f, ForceMode.Impulse);
        }
        else if(collision.gameObject.tag == "Train")
        {
            //CmdApplyKnockback(adjustedDirection, effectiveKnockback);
            rigidbody.AddForce(0f, 300, 0f, ForceMode.Impulse);

        }
        else if (collision.gameObject.tag == "The Mushroom" && TimeWhenPickUp == 0f)
        {
            DommageBlock = true;
            DamagedHandler.color = Color.blue;
            TimeWhenPickUp = Time.time;
            Duration = TimeWhenPickUp + DurationTime;
        }
        else if(otherRigidbody.GetComponent<Projectile>() == null || !DommageBlock)
        {            
            return;
        }
        else
        {
            Projectile projectileComponent = otherRigidbody.GetComponent<Projectile>();


            upwardModifier = projectileComponent.upwardModifier;

            //Increments damage
            damage += projectileComponent.damageValue;
            //CmdApplyDamage(projectileComponent.damageValue);

            //Building the knockback formula
            float sqrtOfDamage = Mathf.Sqrt(damage);
            float linearKBAmp = (1 + damage / 10);
            float quadraticKBAmp = Mathf.Pow(exponentialBase, exponentialCoefficient * sqrtOfDamage);

            //Setting a maximum, if we want one.
            float knockbackAmplification = linearKBAmp * quadraticKBAmp;

            //Calculating effective knockback
            float effectiveKnockback = projectileComponent.baseKnockback * knockbackAmplification;

            //Calculates first collision point. Todo: improve responsiveness by finding the virtual center.
            //Vector3 firstContact = collision.contacts[0].point;
            //Vector3 firstContactNormal = collision.contacts[0].normal;

            Vector3 compositeContact = Vector3.zero;

            foreach (ContactPoint contact in collision.contacts)
            {
                compositeContact += contact.point;
            }

            compositeContact /= collision.contacts.Length;

            Vector3 direction = (otherRigidbody.position - compositeContact).normalized;

            Debug.DrawRay(rigidbody.position, direction, Color.red, 5f);

            //Adds upward movement to the force so that objects don't juste slide around
            Vector3 adjustedDirection = (direction) + new Vector3(0, upwardModifier, 0);

            //CmdApplyKnockback(adjustedDirection, effectiveKnockback);
            rigidbody.AddForce(adjustedDirection * effectiveKnockback, ForceMode.Impulse);

        }
    }

    [Command]
    private void CmdApplyDamage(float damageValue)
    {

    }

    [Command]
    private void CmdApplyKnockback(Vector3 adjustedDirection, float effectiveKnockback)
    {
        //Adds force. WARNING DEPENDENT ON PLAYER RIGIDBODY'S MASS.
        rigidbody.AddForce(adjustedDirection * effectiveKnockback, ForceMode.Impulse);
    }

    public void ResetDamage()
    {
        damage = 0f;
    }
}
