using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CazadorNewbies_EscortTeammate : FMSEstadoNewbies
{
    private CazadorNewbies cazador;

    public CazadorNewbies_EscortTeammate(
        FSMNewbies fsm, Animator animator, CazadorNewbies cazador)
        : base(fsm, animator)
    {
        this.cazador = cazador;
    }

    public override void Enter()
    {
        base.Enter();

        // Si un compañero tiene la quaffle, podemos acompañarlo en grupo
        cazador.steering.teamCohesion = true;
        cazador.steering.cohesionWeight = 1f;

        cazador.steering.teamAlignment = true;
        cazador.steering.alignmentWeight = 1f;

        cazador.steering.teamSeparation = true;
        cazador.steering.separationWeight = 1f;
    }

    public override void UpdateEstado()
    {
        // Podrían perder el control de la pelota, hay que intentar recuperarla
        if( ! GameManager.instancia.isQuaffleControlled())
        {
            fsm.CambiarDeEstado(cazador.estadoPerseguirPelota);
        }
    }

    public override void Exit()
    {
        cazador.steering.teamCohesion = false;
        cazador.steering.teamAlignment = false;
        cazador.steering.teamSeparation = false;
    }
}
