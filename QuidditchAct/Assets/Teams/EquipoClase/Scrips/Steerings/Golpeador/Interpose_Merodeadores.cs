using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpose_Merodeadores : SteeringBase_Merodeadores
{
    public Transform AgenteA;
    public Transform AgenteB;

    //para hacer arrive
    public float distR = 0.1f;

    public override Vector3 CalcularSteering()
    {
        if (AgenteA || AgenteB != null)
        {
            //calcular punto medio entre los dos agentes
            //Vector2D MidPoint = (AgentA->Pos() + AgentB->Pos()) / 2.0;
            Vector3 MidPoint = (AgenteA.position + AgenteB.position) / 2;

            //double TimeToReachMidPoint = Vec2DDistance(m_pVehicle->Pos(), MidPoint) / m_pVehicle->MaxSpeed();
            //se aproxima el tiempo que tomara para llegar al punto medio en el momento actual a máxima velocidad
            float TimeReachMidPoint = Vector3.Distance(transform.position, MidPoint) / MaxSpeed;

            //Vector2D APos = AgentA->Pos() + AgentA->Velocity() * TimeToReachMidPoint;
            //guardamos las velocidades antes de cada agente
            Vector3 AgenteAspeed = AgenteA.GetComponent<Rigidbody>().velocity;
            Vector3 AgenteBspeed = AgenteB.GetComponent<Rigidbody>().velocity;
            // se calcula la posicion futura de los agentes
            Vector3 Apos = AgenteA.position + AgenteAspeed * TimeReachMidPoint;
            Vector3 Bpos = AgenteB.position + AgenteBspeed * TimeReachMidPoint;

            //calculamos/actualizamos el punto medio de esas posiciones futuras 
            MidPoint = (Apos + Bpos) / 2;
            Vector3 direccion = (MidPoint - transform.position).normalized;

            //return Arrive(MidPoint, fast);
            //hacemos el codigo para arrive adaptado para el proyecto
            if (MidPoint.magnitude <= distR)
            {
                float velocidad = MaxSpeed * (MidPoint.magnitude + distR);

                Vector3 velocidadDeseada = direccion.normalized * velocidad;
                Vector3 velocidadSteering = velocidadDeseada - GetComponent<Rigidbody>().velocity;
                return velocidadSteering;
            }
            else
            {
                Vector3 velocidadDeseada = direccion.normalized * (MaxSpeed + TimeReachMidPoint);
                Vector3 velocidadSteering = velocidadDeseada - GetComponent<Rigidbody>().velocity;
                return velocidadSteering;
            }
        }

        return Vector3.zero;
    }
}
