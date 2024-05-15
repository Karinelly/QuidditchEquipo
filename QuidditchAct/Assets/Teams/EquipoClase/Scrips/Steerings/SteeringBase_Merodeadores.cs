using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBase_Merodeadores : MonoBehaviour
{
    public bool active = false;

    protected Rigidbody rigi;

    public float weigth = 1f;

    public float Force;
    public float MaxSpeed;

    private void Awake()
    {
        MaxSpeed = GetComponent<SteeringBlender_Merodeadores>().maxSpeed;
        rigi = GetComponent<SteeringBlender_Merodeadores>().rb;
    }

    public abstract Vector3 CalcularSteering();


    protected void Update()
    {
        if (active)
        {
            Vector3 steer = CalcularSteering();

            GetComponent<Rigidbody>().AddForce(steer);
        }
        if (GetComponent<Rigidbody>().velocity.magnitude > 0.01f)
        {
            transform.forward = GetComponent<Rigidbody>().velocity.normalized;
        }
    }

}