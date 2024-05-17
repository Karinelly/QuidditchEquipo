using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Cualquier agente que quiera usar GOAP tiene que implmenetar
 * esta interfaz. Ayudará al Planeador a decidir que acciones tomar.
 * 
 * La usamos también para comunicarnos con el agente y hacerle saber
 * si una acción falla o se cumple.
 * */

public interface IGoap_Merodeadores 
{
    // La información del estado del juego o mundo
    Dictionary<string, object> GetWorldState();

    // Proporcionar al planeador una meta para construir la
    // secuencia de acciones
    Dictionary<string, object> CreateGoalState();

    void PlanFailed(Dictionary<string, object> FailedGoal);

    void PlanFound(
        Dictionary<string, object> Goal, Queue<GoapAction_Merodeadores> actions);

    void ActionFinished();

    void PlanAborted(GoapAction_Merodeadores abortedAction);

    bool moveAgent(GoapAction_Merodeadores nextAction);
}
