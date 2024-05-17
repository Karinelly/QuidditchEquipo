using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buscador_Merodeadores : Jugador_Merodeadores
{
    //acceder al game manager
    public GameObject Manager;

    void Awake()
    {
        Manager = GameObject.FindGameObjectWithTag("Manager");
    }

    public override Dictionary<string, object> CreateGoalState()
    {
        

        Dictionary<string, object> meta = new Dictionary<string, object>();

        //meta.Add("AtrapeLaSnitch", true);
        meta.Add("AtrapeLaSnitch", true);

        //crear fin dek juego
        //Acabar el juego
        Manager.GetComponent<GameManager>().GrabSnitch(gameObject);


        Debug.Log("El buscador tiene un objetivo en el juego");

        return meta;
    }
}
