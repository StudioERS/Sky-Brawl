using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHeadFollow : MonoBehaviour {

    [SerializeField] // Need Camera
    Transform PlayerCamera;

    [SerializeField]// Need Spine of player
    Transform spinTransform;

    Quaternion spinRotation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }
}
