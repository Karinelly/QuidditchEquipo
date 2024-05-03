using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(SteeringBlender_Clase))]



public class Cazador_Clase : MonoBehaviour
{
    private FSM_Clase fms_Clase;
    private Animator animatior_clase;

    //cuando tengamos el nuevo scrip de player se lo cambiamos ahi para que todos los jugadores hereden
    //el maxforce y maxspeed
    public SteeringBlender_Clase blenderBase;

    //lista de estados
    public Estado_Clase estadoBuscarPelota;
    public Estado_Clase estadoBuscarAro;
    public Estado_Clase estadoEspera;


    public float ThrowStrength;
    public float distanceToShoot;

    // Start is called before the first frame update
    void Start()
    {
        fms_Clase = new FSM_Clase(this);

        estadoBuscarPelota = new Cazador_BuscarPelotaState(fms_Clase, animatior_clase, this);
        estadoBuscarAro = new Cazador_BuscarAroState(fms_Clase, animatior_clase, this);
        estadoEspera = new Clase_EsperarState(fms_Clase, animatior_clase, this);

        fms_Clase.Iniciar(estadoEspera);

        //de momento lo heredara solo el cazador
        blenderBase = GetComponent<SteeringBlender_Clase>();
    }

    // Update is called once per frame
    void Update()
    {
        fms_Clase.UpdateFSM();
    }
}
