using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cazador_BuscarPelotaState : Estado_Merodeadores
{
    private Cazador_Clase cazador;
    private Seek_Merodeadores seek;

    //private Coroutine rutinaCazar;

    //variable de control
    // private bool cazando = false;

    public Cazador_BuscarPelotaState(FSM_Merodeadores fsm, Animator animator, Cazador_Clase cazador) : base(fsm, animator)
    {
        this.cazador = cazador;
        seek = this.cazador.GetComponent<Seek_Merodeadores>();
    }

    public override void Enter()
    {
        //ejecutamos en enter de la clase base
        base.Enter();

        //asignamos la pelota como target 
        seek.active = true;
        seek.Target = GameManager.instancia.Quaffle.transform;
        Debug.Log("target" + seek.Target);
        
    }

    public override void UpdateEstado()
    {
        //verificar si le acabo de meter puntos al equipo rival pra darle su tiempo de espera
        if (GameManager.instancia.IsRecovering() != cazador.transform.parent.GetComponent<Team_Merodeadores >().getTeamNumber() && GameManager.instancia.IsRecovering() != 0)
        {
            fsm.CambiarEstado(cazador.estadoEspera);
            return;
        }

        // Estoy tras de la pelota, checo si otro jugador tiene
        // posesión de ella
        if (!GameManager.instancia.isQuaffleControlled())
        {
            // Llegar hasta la pelota
            // Veo si estoy cerca de la Quaffle
            if (Vector3.Distance(
                cazador.transform.position,
                cazador.GetComponent<Seek_Merodeadores>().Target.position) < 6f)
            {
                // Si no esta controlada, yo puedo tomar posesión de ella
                if (GameManager.instancia.ControlQuaffle(cazador.gameObject))
                {
                    // Busco anotar porque tengo la pelota
                    fsm.CambiarEstado(cazador.estadoBuscarAro);
                }

            }
        }
    }

    public override void Exit()
    {
        seek.active = false;
        seek.Target = null;
    }
}
