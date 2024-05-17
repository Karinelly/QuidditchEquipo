using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CazadorMerodeadores_EstadoPerseguirPelota : FMSEstadoMerodeadores
{
    private ChaserMerodeadores cazador;

    public CazadorMerodeadores_EstadoPerseguirPelota(
        FSMMerodeadores fsm, Animator animator, ChaserMerodeadores cazador)
        : base(fsm, animator)
    {
        this.cazador = cazador;
    }

    public override void Enter()
    {
        base.Enter();

        // Buscamos la Quaffle
        cazador.steering.Target = GameManager.instancia.Quaffle.transform;
        // Usamos seek porque quiero que llegue lo antes posible a la pelota
        cazador.steering.seek = true;
        cazador.steering.seekWeight = 1f;
    }

    public override void UpdateEstado()
    {
        // Preguntamos si algún equipo se está recuperando de una anotación
        if (GameManager.instancia.IsRecovering() != 0 &&
            GameManager.instancia.IsRecovering() != cazador.myTeam.GetTeamNumber())
        {
            // esperar unos segundos antes de tratar de ir por la pelota
            fsm.CambiarDeEstado(cazador.estadoEsperar);
            return;
        }


        // Estoy tras la pelota, hay que ver si la tiene otro jugador
        if (!GameManager.instancia.isQuaffleControlled())
        {
            // Llego a cierta distancia de la pelota y la intento controlar
            if (Vector3.Distance(cazador.transform.position, cazador.steering.Target.position) < 7f)
            {
                // No está controlada y estoy cerca, puedo tomarla
                if (GameManager.instancia.ControlQuaffle(cazador.gameObject))
                {
                    // Ya que tengo la pelota, busco anotar
                    fsm.CambiarDeEstado(cazador.estadoBuscarGol);
                }
            }
        }
        else
        {
            // Si la quaffle ya la tiene tomada otro jugador
            // podemos checar si la tiene un compañero
            if (cazador.myTeam.isTeammate(
                GameManager.instancia.Quaffle.GetComponent<Quaffle>().CurrentBallOwner()
                ))
            {
                // Acompañar al jugador
                fsm.CambiarDeEstado(cazador.estadoAcompañarCompañero);
            }
            // Pero si la pelota la tiene un rival
            else
            {
                // Podemos ir tras el rival para bloquear, quitar la pelota...
                fsm.CambiarDeEstado(cazador.estadoPerseguirPelota);
            }
        }
    }

    public override void Exit()
    {
        cazador.steering.seek = false;
    }
}
