using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek_Merodeadores : SteeringBase_Merodeadores
{
    public Transform Target;
    //public float speed = 5f; borre esa variables para que utilice la de steering base que hereda steering blender 
    // Update is called once per frame
    public override Vector3 CalcularSteering()
    {
        if(Target !=null)
        {
            //calculamos el vector de fuerza
            Vector3 velocicidadDeseada = (Target.position - transform.position).normalized * MaxSpeed; //MaxSpeed es la variable que se
                                                                                                       //declaro en steeringbase
                                                                                                       // asi que en los steerings que vayan usar
                                                                                                       // le cambian el de speed a MaxSpeed

            //regresamos el vector de fuerza calculado
            return velocicidadDeseada - rigi.velocity;
        }
        //si no hay target, que no se mueva
        return Vector3.zero;
    }
}
