using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMMerodeadores //: MonoBehaviour
{
    private FMSEstadoMerodeadores estadoActual;
    public MonoBehaviour mono;

    public FSMMerodeadores(MonoBehaviour mono)
    {
        this.mono = mono;
    }

    public void Iniciar(FMSEstadoMerodeadores inicial)
    {
        estadoActual = inicial;
        // tan pronto sabemos el estado, ejecutamos su función de enter
        estadoActual.Enter();
    }

    public void Update()
    {
        estadoActual.UpdateEstado();
    }

    public void CambiarDeEstado(FMSEstadoMerodeadores siguienteEstado)
    {
        if(siguienteEstado != estadoActual)
        {
            estadoActual.Exit();
            siguienteEstado.Enter();
            estadoActual = siguienteEstado;
        }
    }
}
