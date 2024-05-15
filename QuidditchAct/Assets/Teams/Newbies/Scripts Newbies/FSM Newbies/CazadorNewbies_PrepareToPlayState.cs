using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CazadorNewbies_PrepareToPlayState : FMSEstadoNewbies
{
    private CazadorNewbies cazador;

    public CazadorNewbies_PrepareToPlayState(
        FSMNewbies fsm, Animator animator, CazadorNewbies cazador)
        : base(fsm, animator)
    {
        this.cazador = cazador;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateEstado()
    {
        // Tiene que ir a ubicarse en su posición inicial
        if(cazador.myStartingPosition != null)
        {
            cazador.steering.Target = cazador.myStartingPosition;
            cazador.steering.arrive = true;
            cazador.steering.arriveWeight = 1f;
        }

        // El jugador tiene que ir a sus posición inicial y esperar
        // a que inicie el juego
        if(cazador.myStartingPosition != null)
        {
            if(Vector3.Distance(
                cazador.transform.position, cazador.steering.Target.position)< 1f)
            {
                fsm.CambiarDeEstado(cazador.estadoEsperar);
            }    
        }

        // Si el juego inicia, tiene que cambiar de estado
        if(GameManager.instancia.isGameStarted())
        {
            fsm.CambiarDeEstado(cazador.estadoPerseguirPelota);
        }
    }

    public override void Exit()
    {
        cazador.steering.arrive = false;
    }
}
