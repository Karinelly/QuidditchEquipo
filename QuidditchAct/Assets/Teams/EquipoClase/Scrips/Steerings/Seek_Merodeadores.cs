using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek_Merodeadores : SteeringBase_Merodeadores
{
    public Transform Target;

    // Update is called once per frame
    public override Vector3 CalcularSteering()
    {
        if(Target !=null)
        {
            //calculamos el vector de fuerza
            Vector3 velocicidadDeseada = (Target.position - transform.position).normalized * MaxSpeed;

            //regresamos el vector de fuerza calculado
            return velocicidadDeseada - rigi.velocity;
        }
        //si no hay target, que no se mueva
        return Vector3.zero;
    }
}
