using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alinegment_Merodeadores : SteeringBase_Merodeadores
{
    Golpeador_Merodeadores agenteGolpeador;

    private void Awake()
    {
        agenteGolpeador = GetComponent<Golpeador_Merodeadores>();
    }
    public override Vector3 CalcularSteering()
    {
        Vector3 steerinForce = Vector3.zero;

        int numVecinos = 0;

        Collider[] agentescercanos = Physics.OverlapSphere(transform.position, agenteGolpeador.radio, agenteGolpeador.capaTeamMates);

        foreach(Collider agente in agentescercanos)
        {
            if(agente.gameObject != gameObject)
            {
                steerinForce += agente.transform.forward;
                numVecinos++;
            }
            
        }
        if (numVecinos > 0)
        {
            steerinForce /= numVecinos;
            steerinForce.Normalize();
            steerinForce -= transform.forward;
        }
        return steerinForce;

    }
}
