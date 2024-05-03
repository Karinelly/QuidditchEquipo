using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cazador_BuscarAroState : Estado_Clase
{
    private Cazador_Clase cazador;

    private Seek_Clase seek;

    //private Coroutine rutinaCazar;

    //variable de control
   // private bool cazando = false;

    public Cazador_BuscarAroState(FSM_Clase fsm, Animator animator, Cazador_Clase cazador) : base(fsm, animator)
    {
        this.cazador = cazador;
        seek = this.cazador.GetComponent<Seek_Clase>();
    }

    public override void Enter()
    {
        //ejecutamos en enter de la clase base
        base.Enter();

        //asignamos alguno de los aros rivales como target 
        int aro = Random.Range(0, 2);
        seek.Target  = cazador.transform.parent.GetComponent<Team_Clase>().rivalGoals[aro];
        seek.speed = cazador.blenderBase.normalSpeed;//para poder modificar la velocidad desde el steering blender

        seek.active = true;
        
    }

    public override void UpdateEstado()
    {
        // Si ya me encuentro a cierta distancia el objetivo, 
        // puedo tirar al aro
        if (Vector3.Distance(
            cazador.transform.position, seek.Target.position) < (cazador.distanceToShoot)) // calibrar
        {
            // estoy a rango de disparo
            GameManager.instancia.Quaffle.GetComponent<Quaffle>().
                Throw(
                    seek.Target.position - cazador.transform.position,
                    cazador.ThrowStrength
                );

            // Ya no tengo posesión de la pelota
            GameManager.instancia.FreeQuaffle();

            // Cambiar de estado
            //fsm.CambiarEstado(cazador.estadoBuscarPelota);
        }
    }

    public override void Exit()
    {
        seek.Target = null;
        seek.active = false;
    }
}
