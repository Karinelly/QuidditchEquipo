using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolpeadorNewbies : Player
{
    // mi fsm personalizada
    private FSMNewbies fsmNewbies;

    // Lista de estados
    

    public float ThrowStrength = 50;
    public float DistanceToShoot = 10;

    public Animator animator;

    protected override void Start()
    {
        base.Start();

        fsmNewbies =  new FSMNewbies(this);
        // Agregamos los estados del agente
       
        // Cual es el estado inicial
        //fsmNewbies.Iniciar(estadoPreparacion);
    }

    protected override void Update()
    {
        base.Update();
        //fsmNewbies.Update();
    }
}
