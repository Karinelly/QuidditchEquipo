using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CazadorNewbies_ChaseRival : FMSEstadoNewbies
{
    private CazadorNewbies cazador;

    public CazadorNewbies_ChaseRival(
        FSMNewbies fsm, Animator animator, CazadorNewbies cazador)
        : base(fsm, animator)
    {
        this.cazador = cazador;
    }

    public override void Enter()
    {
        base.Enter();

        // Buscamos al jugador rival que tiene la Quaffle
        cazador.steering.Target =
            GameManager.instancia.
                Quaffle.GetComponent<Quaffle>().CurrentBallOwner().transform;

        // Usamos seek porque quiero que llegue lo antes posible al rival
        cazador.steering.seek = true;
        cazador.steering.seekWeight = 1f;
    }

    public override void UpdateEstado()
    {
        // si la quaffle ya no tiene dueño
        if( ! GameManager.instancia.isQuaffleControlled())
        {
            fsm.CambiarDeEstado(cazador.estadoPerseguirPelota);
        }
    }

    public override void Exit()
    {
        cazador.steering.seek = false;
    }
}
