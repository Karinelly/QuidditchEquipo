using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(SteeringBlender_Merodeadores))]



public class Keeper_Merodeador : MonoBehaviour
{
    private FSM_Merodeadores fms_Clase;
    private Animator animatior_clase;

    //cuando tengamos el nuevo scrip de player se lo cambiamos ahi para que todos los jugadores hereden
    //el maxforce y maxspeed
    public SteeringBlender_Merodeadores blenderBase;

    //lista de estados
    
    public Estado_Merodeadores estadoIniciar;
    public Estado_Merodeadores estadoPorteria;
    public Estado_Merodeadores estadoMoverIdle;
    public Estado_Merodeadores estadoIntercept;


    public float ThrowStrength;
    public float distanceToShoot;

    // Start is called before the first frame update
    void Start()
    {
        fms_Clase = new FSM_Merodeadores(this);

        //estadoBuscarPelota = new Cazador_BuscarPelotaState(fms_Clase, animatior_clase, this);
        //estadoBuscarAro = new Cazador_BuscarAroState(fms_Clase, animatior_clase, this);
        //estadoEspera = new Clase_EsperarState(fms_Clase, animatior_clase, this);
        estadoIniciar = new Merodeadores_Iniciar(fms_Clase, animatior_clase, this);
        estadoPorteria = new Merodeadores_IrAPorteria(fms_Clase, animatior_clase, this);
        estadoMoverIdle = new Merodeadores_Edo_MovIdle(fms_Clase, animatior_clase, this);
        estadoIntercept = new Merodeadores_Edo_Interceptar(fms_Clase, animatior_clase, this);

        fms_Clase.Iniciar(estadoIniciar);

        //de momento lo heredara solo el cazador
        blenderBase = GetComponent<SteeringBlender_Merodeadores>();
    }

    // Update is called once per frame
    void Update()
    {
        fms_Clase.UpdateFSM();
    }
}
