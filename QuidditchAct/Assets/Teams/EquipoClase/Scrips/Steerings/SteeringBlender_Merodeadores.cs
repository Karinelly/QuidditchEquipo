using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBlender_Merodeadores : MonoBehaviour
{
    public Rigidbody rb;

    public float maxForce = 5f;
    public float maxSpeed = 5f;

    private List<SteeringBase_Merodeadores> comportamientos = new List<SteeringBase_Merodeadores>();

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    

    void Start()
    {
       

        //obtener todos los steering asiandos al agente
        SteeringBase_Merodeadores[] arreglo = GetComponents<SteeringBase_Merodeadores>();
        for (int a = 0; a < arreglo.Length; a++)
        {
            comportamientos.Add(arreglo[a]);
        }
    }
    //normalizar los vectores de todos los steering antes de ,andarlos
    private Vector3 WeightedTruncatedSum()
    {
        Vector3 fuerzaTotal = Vector3.zero;

        foreach(SteeringBase_Merodeadores comportamiento in comportamientos)
        {
            if(comportamiento.active)
            {
                fuerzaTotal += comportamiento.CalcularSteering() * comportamiento.weigth;
            }
        }

        if(fuerzaTotal.magnitude > maxForce)
        {
            fuerzaTotal = fuerzaTotal.normalized * maxForce;
        }

        return fuerzaTotal;
    }


    /* Base para hacer otra combinacion de steering
    private Vector3 PrioritizedDithering()
    {
        Vector3 fuerzaTotal = Vector3.zero;
        //
        return fuerzaTotal;
    }*/

    void Update()
    {
        Vector3 steering = WeightedTruncatedSum();
        //le añado esa fuerza al agente
        rb.AddForce(steering);

        if(steering.magnitude > maxSpeed)
        {
            rb.velocity = steering.normalized * maxSpeed;
        }

        //girar al agente si lleva una velocidad significativa
        if(rb.velocity.magnitude > 0.01f)
        {
            transform.forward = rb.velocity.normalized;
        }
    }
}
