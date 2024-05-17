using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(SteeringBlender_Merodeadores))]



public class Cazador_Merodeadores : MonoBehaviour
{
    private FSM_Merodeadores fms_Merodeadores;
    private Animator animatior_clase;

    //lista de estados
    public Estado_Merodeadores estadoBuscarPelota;
    public Estado_Merodeadores estadoBuscarAro;
    public Estado_Merodeadores estadoEspera;


    public float ThrowStrength;
    public float distanceToShoot;

    // Start is called before the first frame update
    void Start()
    {
        fms_Merodeadores = new FSM_Merodeadores(this);

        estadoBuscarPelota = new Cazador_BuscarPelotaState(fms_Merodeadores, animatior_clase, this);
        estadoBuscarAro = new Cazador_BuscarAroState(fms_Merodeadores, animatior_clase, this);
        estadoEspera = new Merodeadore_EsperarState(fms_Merodeadores, animatior_clase, this);

        fms_Merodeadores.Iniciar(estadoEspera);
    }

    // Update is called once per frame
    void Update()
    {
        fms_Merodeadores.UpdateFSM();
    }
}
