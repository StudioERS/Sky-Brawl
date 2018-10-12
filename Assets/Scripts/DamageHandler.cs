using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour {


    [SerializeField] float exponentialBase = 1.05f;
    [SerializeField] float exponentialCoefficient = 1f;

    private float damage = 0f;
    new Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        print(rigidbody.gameObject.name);
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
}
