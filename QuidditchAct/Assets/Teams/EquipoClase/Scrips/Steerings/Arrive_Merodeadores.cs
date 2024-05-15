using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive_Merodeadores : SteeringBase_Merodeadores
{
    public Transform target;

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
                float velocidad = MaxSpeed * (dist / distR);

                Vector3 velocidadDeseada = direccion.normalized * velocidad;
                Vector3 velocidadSteering = velocidadDeseada - rigi.velocity;
                return velocidadSteering;
            }
            else
            {
                Vector3 velocidadDeseada = direccion * MaxSpeed;
                Vector3 velocidadSteering = velocidadDeseada - rigi.velocity;
                return velocidadSteering;
            }
        }
        else
        {
            return Vector3.zero;
        }
        

    }
}
