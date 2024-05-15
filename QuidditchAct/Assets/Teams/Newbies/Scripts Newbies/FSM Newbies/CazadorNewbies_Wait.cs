using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CazadorNewbies_Wait : FMSEstadoNewbies
{
    private CazadorNewbies cazador;
    private bool esperando = false;
    private Coroutine coroutine;

    public CazadorNewbies_Wait(
        FSMNewbies fsm, Animator animator, CazadorNewbies cazador)
        : base(fsm, animator)
    {
        this.cazador = cazador;
    }

    public override void Enter()
    {
        base.Enter();
        esperando = true;
        coroutine = fsm.mono.StartCoroutine(CorutinaEspera());
    }

    public override void UpdateEstado()
    {
        if(!esperando && GameManager.instancia.isGameStarted() && 
            GameManager.instancia.IsRecovering() == 0)
        {
            fsm.CambiarDeEstado(cazador.estadoPerseguirPelota);
        }
    }

    public override void Exit()
    {
        esperando = true;
        fsm.mono.StopCoroutine(CorutinaEspera());
    }

    private IEnumerator CorutinaEspera()
    {
        yield return new WaitForSeconds(5f);
        esperando = false;
    }
}
