using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merodeadores_Iniciar : Estado_Merodeadores
{
    private Keeper_Merodeador Keeper;
    private Seek_Merodeadores seek;
    private Team_Merodeadores claseref;

    private Coroutine rutina;
    private float temporizador;

    public Merodeadores_Iniciar(FSM_Merodeadores fsm, Animator animator, Keeper_Merodeador keeper) : base(fsm, animator)
    {
        this.Keeper = keeper;
        seek = this.Keeper.GetComponent<Seek_Merodeadores>();
        claseref = GameObject.Find("Merodeadores").GetComponent<Team_Merodeadores>();
    }

    public override void Enter()
    {
        //ejecutamos en enter de la clase base
        base.Enter();

        //asignamos la pelota como target 
        seek.active = true;
        seek.Target = claseref.SeekerPos();
        //seek.speed = Keeper.blenderBase.normalSpeed;//para poder modificar la velocidad desde el steering blender
        Debug.Log("target" + seek.Target);

    }

    public override void UpdateEstado()
    {
        if (seek.Target != null)
        {
            if (Vector3.Distance(
                    Keeper.transform.position,
                    Keeper.GetComponent<Seek_Merodeadores>().Target.position) < 6f)
            {
                if (GameManager.instancia.isGameStarted())
                {
                    Debug.Log("Cambia de edo");
                    fsm.CambiarEstado(Keeper.estadoPorteria);
                }
            }
        }
        else
        {
            seek.Target = claseref.SeekerPos();
        }
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
