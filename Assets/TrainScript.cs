using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainScript : MonoBehaviour {

    public Transform Train;
    public Collider TrainIndicator;

    string direction = "xNord";
    bool wait;
    int i = 0;

    int count = 0;

    Vector3 initial;

    public float speedf;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        count++;        
        if(count > 200)
        {
            if (initial == Train.transform.position)
            {
                Train.transform.position = new Vector3(-1.9f, 11.4f, -25f);
                direction = "xNord";
                Train.transform.rotation = Quaternion.Euler(-90f, 0, 90);
                count = 0;
            }
            else count = 0;
        }
        else if (count > 100)
            {
                initial = Train.transform.position;
            }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (direction == "xNord")
            Train.transform.position += new Vector3(speedf, 0, 0);
        if (direction == "zNord")
            Train.transform.position += new Vector3(0, 0, speedf);
        if (direction == "xSud")
            Train.transform.position += new Vector3(-speedf, 0, 0);
        if (direction == "zSud")
            Train.transform.position += new Vector3(0, 0, -speedf);

    }

    private void OnTriggerExit(Collider other)
    {

    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Rails")
        {
            if (i == 0)
            {
                i = 1;
                if (direction == "xNord")
                {
                    direction = "zNord";
                    Train.transform.rotation = Quaternion.Euler(-90, -90f, 90);
                    wait = true;
                }
                else if (direction == "zNord")
                {
                    direction = "xSud";
                    Train.transform.rotation = Quaternion.Euler(-90f, -180f, 90);
                    wait = true;

                }
                else if (direction == "xSud")
                {
                    direction = "zSud";
                    Train.transform.rotation = Quaternion.Euler(-90f, -270f, 90); wait = true;

                }
                else if (direction == "zSud" )
                {
                    direction = "xNord";
                    Train.transform.rotation = Quaternion.Euler(-90f, 0, 90); wait = true;

                }
            }
            
        }
            
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Rails")
            i = 0;
    }
}
