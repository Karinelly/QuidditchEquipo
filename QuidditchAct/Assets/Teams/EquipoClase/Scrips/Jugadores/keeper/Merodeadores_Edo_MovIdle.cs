using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Merodeadores_Edo_MovIdle : Estado_Merodeadores
{
    private Keeper_Merodeador Keeper;
    public Clase_FollowPath Follow;
    private Team_Merodeadores claseref;
    private Equipo_keeperMerodeadores KeepConexionArbol;

    private Coroutine rutina;
    private float temporizador;



    public Merodeadores_Edo_MovIdle(FSM_Merodeadores fsm, Animator animator, Keeper_Merodeador keeper) : base(fsm, animator)
    {
        this.Keeper = keeper;
        Follow = GameObject.Find("keeper").GetComponent<Clase_FollowPath>();
        claseref = GameObject.Find("Merodeadores").GetComponent<Team_Merodeadores>();
        KeepConexionArbol = GameObject.Find("keeper").GetComponent<Equipo_keeperMerodeadores>();

    }

    public override void Enter()
    {
        //ejecutamos en enter de la clase base
        base.Enter();


        Follow.active = true;

        if (KeepConexionArbol.NTeam == 2)
        {
            Follow.Waypoint[0] = GameObject.Find("path1").transform;
            Follow.Waypoint[1] = GameObject.Find("path2").transform;
            Follow.Waypoint[2] = GameObject.Find("path3").transform;
            Follow.Waypoint[3] = GameObject.Find("path4").transform;
        }
        else if (KeepConexionArbol.NTeam == 1)
        {
            Follow.Waypoint[0] = GameObject.Find("path1_2").transform;
            Follow.Waypoint[1] = GameObject.Find("path2_2").transform;
            Follow.Waypoint[2] = GameObject.Find("path3_2").transform;
            Follow.Waypoint[3] = GameObject.Find("path4_2").transform;
        }
        //asignamos la pelota como target 


        //seek.Target = claseref.Porteria();
        //seek.speed = Keeper.blenderBase.normalSpeed;//para poder modificar la velocidad desde el steering blender
        //Debug.Log("target" + Follow.Target);

    }

    public override void UpdateEstado()
    {
        if (KeepConexionArbol.Intercept == true)
        {
            KeepConexionArbol.EnMovIdle = false;
            //KeepConexionArbol.EnPosPorteria = true;
            Debug.Log("cambio a mov intercept");
            fsm.CambiarEstado(Keeper.estadoIntercept);
        }

        
    }

    public override void Exit()
    {
        Follow.active = false;
        Keeper.GetComponent<Seek_Merodeadores>().active = false;
        Keeper.GetComponent<Seek_Merodeadores>().Target = null;



        //tengo que apagar los steerings para que se quede quieto
        SteeringBase_Merodeadores[] steerings = Keeper.GetComponents<SteeringBase_Merodeadores>();
        foreach (SteeringBase_Merodeadores steer in steerings)
            steer.active = false;
    }
}
