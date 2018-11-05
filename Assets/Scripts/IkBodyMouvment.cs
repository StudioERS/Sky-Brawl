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


    //To hold the pistol with hand
    public Transform rightHandObj = null;

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

        // Hand gun holdin handler
        // Set the right hand target position and rotation, if one has been assigned
        if (rightHandObj != null)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
            anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
        }
    }
}
