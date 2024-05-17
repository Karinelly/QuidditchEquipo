using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMGOAP
{
    private Stack<FSMState_Merodeadores> pilaEstados = new Stack<FSMState_Merodeadores>();

    public void pushState(FSMState_Merodeadores estado)
    {
        pilaEstados.Push(estado);
    }

    public void popState()
    {
        pilaEstados.Pop();
    }

    public void Update(GameObject gameObject)
    {
        if(pilaEstados.Peek() != null)
        {
            // Ejecuta el estado actual
            pilaEstados.Peek().Invoke(this, gameObject);
        }
    }

    public delegate void FSMState_Merodeadores(FSMGOAP fsm, GameObject gameObject);
}
