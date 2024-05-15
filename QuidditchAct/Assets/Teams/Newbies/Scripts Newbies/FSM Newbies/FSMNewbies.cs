using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMNewbies //: MonoBehaviour
{
    private FMSEstadoNewbies estadoActual;
    public MonoBehaviour mono;

    public FSMNewbies(MonoBehaviour mono)
    {
        this.mono = mono;
    }

    public void Iniciar(FMSEstadoNewbies inicial)
    {
        estadoActual = inicial;
        // tan pronto sabemos el estado, ejecutamos su función de enter
        estadoActual.Enter();
    }

    public void Update()
    {
        estadoActual.UpdateEstado();
    }

    public void CambiarDeEstado(FMSEstadoNewbies siguienteEstado)
    {
        if(siguienteEstado != estadoActual)
        {
            estadoActual.Exit();
            siguienteEstado.Enter();
            estadoActual = siguienteEstado;
        }
    }
}
