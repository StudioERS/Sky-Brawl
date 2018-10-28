using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkBodyMouvment : MonoBehaviour {

    Animator anim;

    public float ikWeight = 1;


    // Spine 
    public Transform lookTarget;
    public float lookWeight;
    public float bodyWeight;
    public float headWeight;
    public float eyesWeight;
    public float clampWeight;


    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK()
    {
        // Spine handler
        // Set our Look Weights for this pass
        anim.SetLookAtWeight(lookWeight, bodyWeight, headWeight, eyesWeight, clampWeight);
        //Now set our position to look at for this pass
        anim.SetLookAtPosition(lookTarget.position);

    }
}
