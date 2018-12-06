﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBehaviors : MonoBehaviour {

    [SerializeField]
    List<Transform> RespawnPoint = new List<Transform>();

    [SerializeField]
    Transform car;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = RespawnPoint[Random.Range(0, RespawnPoint.Count)].position;
            DamageHandler playerdamage = other.GetComponent<DamageHandler>();
            playerdamage.ResetDamage();
        }
        
        
       
        if (other.name == "FreeCar")
            other.transform.position = car.position;
    }
}
