using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Merodeadores_Edo_Interceptar : Estado_Merodeadores
{
    private Keeper_Merodeador Keeper;
    //private Cls_Steer_Interpose interposeStr;
    //private Cls_Pursuit PursuitStr;

    private Team_Merodeadores claseref;
    private Equipo_keeperMerodeadores KeepConexionArbol;

    private Coroutine rutina;
    private float temporizador;

    private Seek_Otro seek;


    public Merodeadores_Edo_Interceptar(FSM_Merodeadores fsm, Animator animator, Keeper_Merodeador keeper) : base(fsm, animator)
    {
        this.Keeper = keeper;
        //interposeStr = this.Keeper.GetComponent<Cls_Steer_Interpose>();
        //PursuitStr = this.Keeper.GetComponent<Cls_Pursuit>();

        seek = this.Keeper.GetComponent<Seek_Otro>();

        claseref = GameObject.Find("Merodeadores").GetComponent<Team_Merodeadores>();
        KeepConexionArbol = GameObject.Find("keeper").GetComponent<Equipo_keeperMerodeadores>();
    }

    public override void Enter()
    {
        //ejecutamos en enter de la clase base
        base.Enter();

        seek.active = true;
        seek.Target = KeepConexionArbol.QuaffleRef.GetComponent<Transform>();

        //asignamos la pelota como target 
        //interposeStr.active = true;
        //interposeStr.AgentA = KeepConexionArbol.VistaQuaffle();
        //interposeStr.AgentB = KeepConexionArbol.QuaffleRef;

        // PursuitStr.active = true;
        //PursuitStr.EvaderTarget = KeepConexionArbol.QuaffleRef.GetComponent<Transform>();
        //PursuitStr.TargetObj = KeepConexionArbol.QuaffleRef;
        //Debug.Log(KeepConexionArbol.QuaffleRef.GetComponent<Transform>().position);


        Debug.Log("Voy a interceptar");

    }

    public override void UpdateEstado()
    {

        //si tengo el balon cambio de estado a pasar balon

        if(KeepConexionArbol.Lanzado == false)
        {

            fsm.CambiarEstado(Keeper.estadoPorteria);
        }
        /*
            if (Vector3.Distance(
                    Keeper.transform.position,
                    Keeper.GetComponent<Seek_Clase>().Target.position) < 6f)
            {

                Debug.Log("Cambia de edo");
                KeepConexionArbol.EnPorteria(true);

                if(KeepConexionArbol.EnMovIdle ==true)
                {
                    Debug.Log("cambio a mov idle");
                    fsm.CambiarEstado(Keeper.estadoMoverIdle);
                }
                if (KeepConexionArbol.Intercept == true)
                {
                    Debug.Log("cambio a mov intercept");
                    //fsm.CambiarEstado(Keeper.estadoMoverIdle);
                }
                //fsm.CambiarEstado(cazador.estadoBuscarAro);
            }*/
        
    }

    public override void Exit()
    {
        //Keeper.GetComponent<Cls_Pursuit>().active = false;
        //Keeper.GetComponent<Cls_Pursuit>().TargetObj = null;
        Keeper.GetComponent<Seek_Otro>().active = false;
        Keeper.GetComponent<Seek_Otro>().Target = null;


        //tengo que apagar los steerings para que se quede quieto
        SteeringBase_Merodeadores[] steerings = Keeper.GetComponents<SteeringBase_Merodeadores>();
        foreach (SteeringBase_Merodeadores steer in steerings)
            steer.active = false;
    }
}
