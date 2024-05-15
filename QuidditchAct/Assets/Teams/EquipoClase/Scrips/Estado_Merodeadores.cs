using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Estado_Merodeadores
{
    protected FSM_Merodeadores fsm;
    protected Animator animator;
    protected float duracionEstado;
    protected float tiempoTranscurrido;
    protected bool accionConcluida;

    public Estado_Merodeadores(FSM_Merodeadores fsm, Animator animator)
    {
        this.fsm = fsm;
        this.animator = animator;
        tiempoTranscurrido = 0f;
        accionConcluida = false;
    }

    //se ejecuta una vez al entrar a un estado
    public virtual void Enter()
    {
        tiempoTranscurrido = 0f;
        accionConcluida = false;
    }

    //wa la que actualiza cada frame el estado
    public abstract void UpdateEstado();

    //se ejecuta una vez al salir del estado
    public abstract void Exit();
}
