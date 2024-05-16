using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek_Otro : SteeringBase_Merodeadores
{
    public Transform Target;
    public float speed;

    // Update is called once per frame
    public override Vector3 CalcularSteering()
    {
        if(Target !=null)
        {
            //calculamos el vector de fuerza
            Vector3 velocicidadDeseada = (Target.position - transform.position).normalized * speed;

            //regresamos el vector de fuerza calculado
            return velocicidadDeseada - GetComponent<Rigidbody>().velocity;
        }
        //si no hay target, que no se mueva
        return Vector3.zero;
    }
}
