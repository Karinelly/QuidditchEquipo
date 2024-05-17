using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merodeadore_EsperarState : Estado_Merodeadores
{
    private Cazador_Merodeadores cazador;

    private Coroutine rutina;
    private float temporizador;

    public Merodeadore_EsperarState(FSM_Merodeadores fsm, Animator animator, Cazador_Merodeadores cazador) : base(fsm, animator)
    {
        this.cazador = cazador;
    }

    public override void Enter()
    {
        //ejecutamos en enter de la clase base
        base.Enter();

        //tengo que apagar los steerings para que se quede quieto
        SteeringBase_Merodeadores[] steerings = cazador.GetComponents<SteeringBase_Merodeadores>();
        foreach(SteeringBase_Merodeadores steer in steerings)
                steer.active = false;

        temporizador = 5f;


    }

    public override void UpdateEstado()
    {
        temporizador -= Time.deltaTime;
        if(temporizador <0)
        {
            fsm.CambiarEstado(cazador.estadoBuscarPelota);
        }
                
    }

    public override void Exit()
    {
        cazador.GetComponent<Seek_Merodeadores>().active = false;
        cazador.GetComponent<Seek_Merodeadores>().Target = null;
    }
}
