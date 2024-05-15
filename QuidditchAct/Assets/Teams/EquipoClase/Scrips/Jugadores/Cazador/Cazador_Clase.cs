using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
//[RequireComponent(typeof(SteeringBlender_Clase))]



public class Cazador_Clase : MonoBehaviour
{
    private FSM_Merodeadores fms_Clase;
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
        fms_Clase = new FSM_Merodeadores(this);

        estadoBuscarPelota = new Cazador_BuscarPelotaState(fms_Clase, animatior_clase, this);
        estadoBuscarAro = new Cazador_BuscarAroState(fms_Clase, animatior_clase, this);
        estadoEspera = new Clase_EsperarState(fms_Clase, animatior_clase, this);

        fms_Clase.Iniciar(estadoEspera);
    }

    // Update is called once per frame
    void Update()
    {
        fms_Clase.UpdateFSM();
    }
}
