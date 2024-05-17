using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separation_Merodeadores : SteeringBase_Merodeadores
{
    Golpeador_Merodeadores agenteGolpeador;
    private void Awake()
    {
        agenteGolpeador = GetComponent<Golpeador_Merodeadores>();
    }

    public override Vector3 CalcularSteering()
    {
        Vector3 steerindForce = Vector3.zero;

        //creamos un detector esferico 
        Collider[] agentesCercanos = Physics.OverlapSphere(transform.position, agenteGolpeador.radio, agenteGolpeador.capaTeamMates);

        foreach(Collider agente in agentesCercanos)
        {
            //ignoramos el colisionador de este agente
            if(agente.gameObject != gameObject)
            {
                //vector que apunta a direccion contraria de otro agente
                Vector3 direccionSeparacion = transform.position - agente.transform.position;
                //que tan cerca estoy del otro agente
                float distancia = direccionSeparacion.magnitude;
                //la fuerxa debe ser inversamente proporcional a la distancia
                steerindForce += direccionSeparacion.normalized / distancia;
            }
        }

        return steerindForce;
    }
}
