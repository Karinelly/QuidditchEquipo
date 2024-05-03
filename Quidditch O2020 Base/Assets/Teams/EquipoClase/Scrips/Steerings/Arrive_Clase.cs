using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive_Clase : SteeringBase_Clase
{
    public Transform target;
    public float speed = 5f;

    [Range(1, 5)]
    public float deceleration = 2f;

    public float distR = 0.01f;

    public override Vector3 CalcularSteering()
    {
        if(target !=null)
        {
            Vector3 direccion = target.position - transform.position;
            float dist = direccion.magnitude;

            if(dist<= distR)
            {
                float velocidad = speed * (dist / distR);

                Vector3 velocidadDeseada = direccion.normalized * velocidad;
                Vector3 velocidadSteering = velocidadDeseada - GetComponent<Rigidbody>().velocity;
                return velocidadSteering;
            }
            else
            {
                Vector3 velocidadDeseada = direccion * speed;
                Vector3 velocidadSteering = velocidadDeseada - GetComponent<Rigidbody>().velocity;
                return velocidadSteering;
            }
        }
        else
        {
            return Vector3.zero;
        }
        

    }
}
