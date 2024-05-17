using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CazadorMerodeadores_BuscarGol : FMSEstadoMerodeadores
{

    private ChaserMerodeadores cazador;

    public CazadorMerodeadores_BuscarGol(
        FSMMerodeadores fsm, Animator animator, ChaserMerodeadores cazador)
        : base(fsm, animator)
    {
        this.cazador = cazador;
    }

    public override void Enter()
    {
        base.Enter();

        // Se supone que tengo la quaffle, debo ir hacia algún aro
        int numAro = Random.Range(0, 2);
        cazador.steering.Target = (cazador.myTeam as TeamNewbies).rivalGoals[numAro];
        cazador.steering.seek = true;
        cazador.steering.seekWeight = 1f;
    }

    public override void UpdateEstado()
    {
        //verifico que no hay nadie en mi radio de obstaculos
        if (cazador.steering.Obstacles.Count > 0)
        {
            fsm.CambiarDeEstado(cazador.estadoEsquivarCompañero);
        }
        // Si ya estoy a cierta distancia del objetivo (aro), puedo tirar
        if (Vector3.Distance(cazador.transform.position, cazador.steering.Target.position) <
            cazador.DistanceToShoot)
        {
            GameManager.instancia.Quaffle.GetComponent<Quaffle>().Throw(
                cazador.steering.Target.position - cazador.transform.position,
                cazador.ThrowStrength
            );

            // como lanzamos la pelota, hay que liberarla
            GameManager.instancia.FreeQuaffle();

            // Cambiamos de estado
            // ChangeState(Wait)
        }

        // Podría perder el control de la pelota, hay que intentar recuperarla
        if (!GameManager.instancia.isQuaffleControlled())
        {
            fsm.CambiarDeEstado(cazador.estadoPerseguirPelota);
        }
    }

    public override void Exit()
    {
        cazador.steering.seek = false;
    }
}
