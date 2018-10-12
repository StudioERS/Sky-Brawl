using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour {

    private float damage = 0f;
    [SerializeField] float exponentialBase = 1.05f;
    [SerializeField] float exponentialCoefficient = 1f;

    Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(GameObject source, float damageValue, float knockbackValue = 0f)
    {
        float knockbackAmplification = Mathf.Pow(exponentialBase, exponentialCoefficient * damage);
        float effectiveKnockback = knockbackValue * knockbackAmplification;

        damage += damageValue;
        Vector3 vectorFromSource = Vector3.MoveTowards(source.transform.localPosition, gameObject.transform.localPosition, float.MaxValue);

        rigidbody.AddForce(vectorFromSource.normalized * effectiveKnockback, ForceMode.VelocityChange);
    }
}
