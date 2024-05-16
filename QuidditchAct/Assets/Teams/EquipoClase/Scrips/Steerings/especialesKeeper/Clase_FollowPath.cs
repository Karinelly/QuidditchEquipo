using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Clase_FollowPath : SteeringBase_Merodeadores
{
    public Transform[] Waypoint = new Transform[4];
    int i = 0;
    public float DistCambio = 1f;

    public float speed;
    private Seek_Merodeadores seek;

    private void Start()
    {
        seek = gameObject.GetComponent<Seek_Merodeadores>();
    }
    public override Vector3 CalcularSteering()
    {

        Debug.Log("en sterring follow");
        print("en sterring follow");
        
        if (Vector3.Distance(Waypoint[i].position, transform.position) < DistCambio)
        {
            print(i);
            i++;

        }
        Vector3 finalW = Waypoint[Waypoint.Length - 1].position;

        if (Waypoint[i].position != finalW)
        {
            seek.Target = Waypoint[i];
            //return gameObject.GetComponent<Clase_CombinSeek_Purs_Arra>().SeekVect(Waypoint[i].position, speed);
            return seek.CalcularSteering();
        }
        else
        {
            Vector3 FinalWPoint = Waypoint[i].position;
            i = 0;
            seek.Target = Waypoint[i];
            //return gameObject.GetComponent<Clase_CombinSeek_Purs_Arra>().ArriveVect(FinalWPoint, speed);
            return seek.CalcularSteering();
        }
        

    }

    
}
