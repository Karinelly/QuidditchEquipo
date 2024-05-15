using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CazadorNewbies : Player
{
    // mi fsm personalizada
    private FSMNewbies fsmNewbies;

    // Lista de estados
    public FMSEstadoNewbies estadoPreparacion;
    public FMSEstadoNewbies estadoPerseguirPelota;
    public FMSEstadoNewbies estadoBuscarAro;
    public FMSEstadoNewbies estadoAcompañarCompañero;
    public FMSEstadoNewbies estadoEsperar;
    public FMSEstadoNewbies estadoPerseguirRival;

    public float ThrowStrength = 50;
    public float DistanceToShoot = 10;

    public Animator animator;

    protected override void Start()
    {
        base.Start();

        fsmNewbies =  new FSMNewbies(this);
        // Agregamos los estados del agente
        estadoPreparacion       = new CazadorNewbies_PrepareToPlayState(fsmNewbies, animator, this);
        estadoPerseguirPelota   = new CazadorNewbies_ChaseBall(fsmNewbies, animator, this);
        estadoBuscarAro         = new CazadorNewbies_SearchGoal(fsmNewbies, animator, this);
        estadoAcompañarCompañero= new CazadorNewbies_EscortTeammate(fsmNewbies, animator, this);
        estadoEsperar           = new CazadorNewbies_Wait(fsmNewbies, animator, this);
        estadoPerseguirRival    = new CazadorNewbies_ChaseRival(fsmNewbies, animator, this);

        // Cual es el estado inicial
        fsmNewbies.Iniciar(estadoPreparacion);
    }

    protected override void Update()
    {
        base.Update();
        fsmNewbies.Update();
    }
}
