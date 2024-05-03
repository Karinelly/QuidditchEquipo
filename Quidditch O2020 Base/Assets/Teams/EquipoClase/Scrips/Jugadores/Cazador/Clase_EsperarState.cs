using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clase_EsperarState : Estado_Clase
{
    private Cazador_Clase cazador;

    private Coroutine rutina;
    private float temporizador;

    public Clase_EsperarState(FSM_Clase fsm, Animator animator, Cazador_Clase cazador) : base(fsm, animator)
    {
        this.cazador = cazador;
    }

    public override void Enter()
    {
        //ejecutamos en enter de la clase base
        base.Enter();

        //tengo que apagar los steerings para que se quede quieto
        SteeringBase_Clase[] steerings = cazador.GetComponents<SteeringBase_Clase>();
        foreach(SteeringBase_Clase steer in steerings)
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
        cazador.GetComponent<Seek_Clase>().active = false;
        cazador.GetComponent<Seek_Clase>().Target = null;
    }
}
