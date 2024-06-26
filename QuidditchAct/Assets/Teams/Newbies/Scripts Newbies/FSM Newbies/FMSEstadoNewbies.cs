using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FMSEstadoNewbies //: MonoBehaviour
{
    protected FSMNewbies fsm;
    protected Animator animator;
    protected float duracionEstado;
    protected float tiempoTranscurrido;
    protected bool accionConcluida;

    public FMSEstadoNewbies(FSMNewbies fsm, Animator animator)
    {
        this.fsm = fsm;
        this.animator = animator;
        tiempoTranscurrido = 0f;
        accionConcluida = false;
    }

    public virtual void Enter()
    {
        tiempoTranscurrido = 0f;
        accionConcluida = false;
    }
    public abstract void UpdateEstado();
    public abstract void Exit();

    protected void Esperar(float duracion)
    {
        tiempoTranscurrido += Time.deltaTime;
        if(tiempoTranscurrido >= duracion)
        {
            tiempoTranscurrido = 0f;
            accionConcluida = true;
        }
    }
}
