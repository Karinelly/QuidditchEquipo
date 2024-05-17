using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoapAgent_Merodeadores : MonoBehaviour
{
    private FSMGOAP MaquinaDeEstados;

    private FSMGOAP.FSMState_Merodeadores IdleState; // lo ocuparemos para pensar
    private FSMGOAP.FSMState_Merodeadores ActState;
    private FSMGOAP.FSMState_Merodeadores MoveState;

    private List<GoapAction_Merodeadores> AccionesDisponibles;
    private Queue<GoapAction_Merodeadores> AccionesActuales;

    // Para que el agente conozca el estado del mundo y actualizarlo
    private IGoap_Merodeadores datosPlaneador;

    private GoapPlanner_Merodeadores Planeador;

    public void CrearEstadoIdle()
    {
        // Este estado lo usará el agente para planear
        IdleState = (fsm, gameObj) =>
        {
            // Planeación goap
            // Obtener el estado del mundo
            Dictionary<string, object> worldState =
                datosPlaneador.GetWorldState();
            // Obtener la meta del agente
            Dictionary<string, object> goal =
                datosPlaneador.CreateGoalState();
            // Calcular un plan
            Queue<GoapAction_Merodeadores> plan = Planeador.ElPlan(
                gameObject, AccionesDisponibles, worldState, goal);
            // ¿Logramos encontrar un plan?
            if (plan != null)
            {
                Debug.Log("Encontró un plan");
                AccionesActuales = plan;
                datosPlaneador.PlanFound(goal, plan);
                // estoy en idle, tengo que salir de este estado
                fsm.popState();
                // Paso a actuar
                fsm.pushState(ActState);
            }
            else
            {
                Debug.Log("No encontró un plan.");
                datosPlaneador.PlanFailed(goal);
                // Vuelvo a cargar el estado de idle, para intentar 
                // encontrar un plan
                fsm.popState();
                fsm.pushState(IdleState);
            }
        };
    }

    private void CrearEstadoActuar()
    {
        ActState = (fsm, gameObj) =>
        {
            // Ejecutar la accion
            if (AccionesActuales.Count <= 0) // ya no hay acciones
            {
                fsm.popState(); // salgo de actuar
                fsm.pushState(IdleState); // vuelvo a planear
                datosPlaneador.ActionFinished();
                return;
            }
            // Mientras tenga acciones, obtengo la primera
            GoapAction_Merodeadores accion = AccionesActuales.Peek();
            if (accion.isDone())
            {
                // si la accion se concluyó, la saco de mi lista
                AccionesActuales.Dequeue();
            }
            // Si no ha terminado se ejecuta
            if (AccionesActuales.Count > 0)
            {
                accion = AccionesActuales.Peek();
                // Verificar si necesita estar cerca de un objetivo
                bool enRango = accion.requiresInRange() ?
                    accion.IsInRange() : true;
                // Ya está donde debe estar
                if (enRango)
                {
                    bool exito = accion.Perform(gameObj);
                    // si la acción falla
                    if (!exito)
                    {
                        // Planeamos otra vez
                        fsm.popState();
                        fsm.pushState(IdleState);
                        datosPlaneador.PlanAborted(accion);
                    }
                }
                else
                {
                    // NO esta donde debe estar
                    Debug.Log("Esta lejos del objetivo");
                    fsm.pushState(MoveState);
                }
            } // if count
            else
            {
                // No quedan acciones, podemos regresar a idle
                fsm.popState();
                fsm.pushState(IdleState);
                datosPlaneador.ActionFinished();
            }
        };
    }

    private void CrearEstadoMoverse()
    {
        MoveState = (fsm, gameObj) =>
        {
            GoapAction_Merodeadores accion = AccionesActuales.Peek();
            // Mover al agente hacia su objetivo si tiene
            if (accion.requiresInRange() && accion.Target == null)
            {
                Debug.Log("Acción requiere Target, pero no tiene.");
                fsm.popState(); // sale
                fsm.pushState(IdleState);
                return;
            }
            // que se mueva
            if (datosPlaneador.moveAgent(accion))
            {
                // sale del estado moverse o de actuar
                fsm.popState();
            }
            /*
            // Movimiento, pueden reemplazarlo por Steering despues
            gameObj.transform.position = Vector3.MoveTowards(
                gameObj.transform.position,
                accion.Target.transform.position,
                Time.deltaTime * 5f);
            // Verificar si llega al objetivo
            if (Vector3.Distance(
                gameObj.transform.position,
                accion.Target.transform.position) < 1f)
            {
                // llega al objetivo
                accion.SetInRange(true);
                fsm.popState(); // salimos del estado de moverse
            }*/
        };
    }

    private void Start()
    {
        Planeador = new GoapPlanner_Merodeadores();
        MaquinaDeEstados = new FSMGOAP();
        AccionesActuales = new Queue<GoapAction_Merodeadores>();
        AccionesDisponibles = new List<GoapAction_Merodeadores>();
        // Buscamos al proveedor de datos del mundo
        datosPlaneador = GetComponent<IGoap_Merodeadores>();
        // Creamos nuestros estados
        CrearEstadoIdle();
        CrearEstadoActuar();
        CrearEstadoMoverse();
        // Iniciamos al agente en modo de planear
        MaquinaDeEstados.pushState(IdleState);
        // Carga las acciones que puede hacer el agente
        GoapAction_Merodeadores[] acciones = GetComponents<GoapAction_Merodeadores>();
        foreach (GoapAction_Merodeadores a in acciones)
            AccionesDisponibles.Add(a);
    }

    private void Update()
    {
        MaquinaDeEstados.Update(gameObject);
    }
}
