using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cohesion_Merodeadores : SteeringBase_Merodeadores
{
    Golpeador_Merodeadores agenteGolpeador;
    

    private void Awake()
    {
        agenteGolpeador = GetComponent<Golpeador_Merodeadores>();
    }

    public override Vector3 CalcularSteering()
    {
        //first find the center of mass of all the agents, Vector2D CenterOfMass, SteeringForce;
        Vector3 CenterOfMass = Vector3.zero;
        Vector3 SteeringForce = Vector3.zero;

        int numVecinos = 0;

        //iterate through the neighbors and sum up all the position vector
        Collider[] vecinosCerca =                                                      
            Physics.OverlapSphere(transform.position, agenteGolpeador.radio, agenteGolpeador.capaTeamMates);
        
        foreach (Collider vecino in vecinosCerca)
        {
            //make sure *this* agent isn't included in the calculations and that
            if (vecino.gameObject != gameObject)
            {
                //the agent being examined is a neighbor
                //if((neighbors[a] != m_pVehicle) && neighbors[a]->IsTagged()
                if (vecino.transform.position != transform.position)
                {
                    //CenterOfMass += neighbors[a]->Pos();
                    CenterOfMass += vecino.transform.position;
                    numVecinos++;
                }
            }
        }
        if (numVecinos > 0)
        {
            CenterOfMass /= vecinosCerca.Length;

            //now seek toward that position
            Vector3 seek = (CenterOfMass - transform.position).normalized * MaxSpeed;

            return seek -= GetComponent<Rigidbody>().velocity;
        }

        return Vector3.zero;
       
    }
}
