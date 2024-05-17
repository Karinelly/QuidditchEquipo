using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserMerodeadores : Player
{
    private FSMMerodeadores fsmMerodeadores;
    
    // Lista de estados
    public FMSEstadoMerodeadores estadoPreparacion;
    public FMSEstadoMerodeadores estadoPerseguirPelota;
    public FMSEstadoMerodeadores estadoBuscarGol;
    public FMSEstadoMerodeadores estadoEsquivarCompañero;
    public FMSEstadoMerodeadores estadoRealizarPase;
    public FMSEstadoMerodeadores estadoEsperar;
    public FMSEstadoMerodeadores estadoAcompañarCompañero;

    public float ThrowStrength = 50;
    public float DistanceToShoot = 10;
    public float DistanceToThrow = 10;

    public Animator animator;

    protected override void Start()
    {
        base.Start();
        fsmMerodeadores = new FSMMerodeadores(this);

        estadoPreparacion          = new CazadorMerodeadores_estadoPreparacion(fsmMerodeadores, animator,this);
        estadoEsperar              = new CazadorMerodeadores_EstadoEsperar(fsmMerodeadores, animator, this);
        estadoPerseguirPelota      = new CazadorMerodeadores_EstadoPerseguirPelota(fsmMerodeadores,animator,this);
        estadoAcompañarCompañero   = new CazadorMerodeadores_EstadoAcompañarCompañero(fsmMerodeadores, animator, this);
        estadoBuscarGol            = new CazadorMerodeadores_BuscarGol(fsmMerodeadores, animator, this);
        estadoEsquivarCompañero    = new CazadorMerodeadores_EstadoEsquivarCompañero(fsmMerodeadores, animator, this);

        // Cual es el estado inicial
        fsmMerodeadores.Iniciar(estadoPreparacion);
    }
    protected override void Update()
    {
        base.Update();
        fsmMerodeadores.Update();
    }
}
