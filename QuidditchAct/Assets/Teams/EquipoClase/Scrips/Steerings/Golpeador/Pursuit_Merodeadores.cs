using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuit_Merodeadores : SteeringBase_Merodeadores
{
    public Transform Target;
    private Vector3 seek = Vector3.zero;


    public override Vector3 CalcularSteering()
    {
        if (Target != null) //mientras haya un target y aqui puedo poner cierta distancia
        {
            //for the evader's current position.
            // Vector2D ToEvader = evader->Pos() - m_pVehicle->Pos()
            Vector3 ToEvader = (Target.position - transform.position); //vector de distancia entre el agente y el target ()

            //double RelativeHeading = m_pVehicle->Heading().Dot(evader->Heading());
            float RelativeHeading = (transform.forward - Target.forward).magnitude;
            //establecer la direccion de cada cosa (como magnitudes)
            float distance = ToEvader.magnitude;


            //calcular la velocidad requerida para alcanzaer el target
            //float sentidoTarget = //evader

            if (distance > 0 && distance < -0.95)
            {
                Vector3 Seek =
                    (Target.transform.position - transform.position).normalized * MaxSpeed;

                return Seek - GetComponent<Rigidbody>().velocity;
            }
            else
            {
                Vector3 TargetSpeed = Target.GetComponent<Rigidbody>().velocity;

                //double LookAheadTime = ToEvader.Length() / (m_pVehicle->MaxSpeed() + evader->Speed())
                float LookAheadTime = ToEvader.magnitude / (MaxSpeed + TargetSpeed.magnitude);

                //return Seek(evader->Pos() + evader->Velocity() * LookAheadTime);
                //Vector3 TargetPos = Target.position + Target.transform.forward;

                Vector3 seek = Target.position + TargetSpeed * LookAheadTime;
                Vector3 direccion = (seek - transform.position).normalized;
                direccion *= MaxSpeed;
                return direccion - rigi.velocity;
            }


        }
        //si no hay target no se mueve
        return Vector3.zero;
    }

}
