using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CazadorMerodeadores_EstadoEsquivarCompañero : FMSEstadoMerodeadores
{
    private ChaserMerodeadores cazador;

    //este va a ser el script que de e pase a los otros cazadores
    public CazadorMerodeadores_EstadoEsquivarCompañero(
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

        if (cazador.steering.NearTeammates.ElementAt(0) != null)
        {
            //lanza la pelota a el compañero mas cercano
            GameManager.instancia.Quaffle.GetComponent<Quaffle>().Throw(
                   cazador.steering.NearTeammates.ElementAt(0).transform.position - cazador.transform.position,
                   cazador.ThrowStrength);

        }

        // como lanzamos la pelota, hay que liberarla
        GameManager.instancia.FreeQuaffle();

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
