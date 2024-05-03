using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Clase
{
    //saber en que estado se esta ejecutando en un instante determinado
    private Estado_Clase estadoActual;

    //para poder usar funciones de monobehavior
    public MonoBehaviour mono;


    public FSM_Clase(MonoBehaviour mono)
    {
        this.mono = mono;
    }

    //funcion para uqe  el agente idenque cuando inicia su fsm
    public void Iniciar(Estado_Clase inicial)
    {
        estadoActual = inicial;
        //tan pronto sabemos el estado, ejecutamos su funcion de entrada
        estadoActual.Enter();

    }

    public void UpdateFSM()
    {
        //Le digo a su estado actual que se ejecute
        estadoActual.UpdateEstado();
    }

    public void CambiarEstado(Estado_Clase estadoSiguiente)
    {
        //cuidamo de no transicionar al estado en que ya estamos
        if(estadoSiguiente != estadoActual)
        {
            //antes de cambiar de estado ejecuto la salida del actual
            estadoActual.Exit();
            //despues ya se puede ejecutar la entrada del nuevo estado
            estadoSiguiente.Enter();
            //ya cambio de estado actual
            estadoActual = estadoSiguiente;
        }
    }
}
