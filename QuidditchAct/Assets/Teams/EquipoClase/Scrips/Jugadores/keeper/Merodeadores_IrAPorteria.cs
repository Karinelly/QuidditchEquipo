using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Merodeadores_IrAPorteria : Estado_Merodeadores
{
    private Keeper_Merodeador Keeper;
    private Seek_Merodeadores seek;
    private Team_Merodeadores claseref;
    private Equipo_keeperMerodeadores KeepConexionArbol;

    private Coroutine rutina;
    private float temporizador;

    

    public Merodeadores_IrAPorteria(FSM_Merodeadores fsm, Animator animator, Keeper_Merodeador keeper) : base(fsm, animator)
    {
        this.Keeper = keeper;
        seek = this.Keeper.GetComponent<Seek_Merodeadores>();
        claseref = GameObject.Find("Merodeadores").GetComponent<Team_Merodeadores>();
        KeepConexionArbol = GameObject.Find("keeper").GetComponent<Equipo_keeperMerodeadores>();
    }

    public override void Enter()
    {
        //ejecutamos en enter de la clase base
        base.Enter();

        if (KeepConexionArbol.EnPosPorteria == false)
        {
            //asignamos la pelota como target 
            seek.active = true;
            seek.Target = claseref.Porteria();
            //seek.speed = Keeper.blenderBase.normalSpeed;//para poder modificar la velocidad desde el steering blender
            Debug.Log("target" + seek.Target);
        }
    }

    public override void UpdateEstado()
    {
        if (KeepConexionArbol.EnPosPorteria == false)
        {
            if (seek.Target != null)
            {
                if (Vector3.Distance(
                        Keeper.transform.position,
                        Keeper.GetComponent<Seek_Merodeadores>().Target.position) < 6f)
                {

                    Debug.Log("Cambia de edo");
                    KeepConexionArbol.EnPorteria(true);

                    if (KeepConexionArbol.Intercept == true)
                    {
                        KeepConexionArbol.EnMovIdle = false;
                        Debug.Log("cambio a mov intercept");
                        fsm.CambiarEstado(Keeper.estadoIntercept);
                    }
                    else if (KeepConexionArbol.EnMovIdle == true)
                    {
                        KeepConexionArbol.Intercept = false;
                        Debug.Log("cambio a mov idle");
                        fsm.CambiarEstado(Keeper.estadoMoverIdle);
                    }

                    //fsm.CambiarEstado(cazador.estadoBuscarAro);
                }

            }
            else
            {
                seek.Target = claseref.SeekerPos();
            }
        }
        else
        {
            if (KeepConexionArbol.Intercept == true)
            {
                KeepConexionArbol.EnMovIdle = false;
                Debug.Log("cambio a mov intercept");
                fsm.CambiarEstado(Keeper.estadoIntercept);
            }
            else if (KeepConexionArbol.EnMovIdle == true)
            {
                KeepConexionArbol.Intercept = false;
                Debug.Log("cambio a mov idle");
                fsm.CambiarEstado(Keeper.estadoMoverIdle);
            }
        }
        return;
    }

    public override void Exit()
    {
        Keeper.GetComponent<Seek_Merodeadores>().active = false;
        Keeper.GetComponent<Seek_Merodeadores>().Target = null;

        //tengo que apagar los steerings para que se quede quieto
        SteeringBase_Merodeadores[] steerings = Keeper.GetComponents<SteeringBase_Merodeadores>();
        foreach (SteeringBase_Merodeadores steer in steerings)
            steer.active = false;
    }
}
