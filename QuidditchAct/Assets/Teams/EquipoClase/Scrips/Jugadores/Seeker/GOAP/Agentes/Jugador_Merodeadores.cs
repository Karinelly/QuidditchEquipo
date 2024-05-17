using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Jugador_Merodeadores : MonoBehaviour, IGoap_Merodeadores
{
    //VARIABLES DEL TRABAJADOR PARA  MOVERSE
    public float movementSpeed = 2f;

    public void ActionFinished() { }

    //Herencia
    public abstract Dictionary<string, object> CreateGoalState();

    public Inventario_Merodeadores invent;

    //para devolver la info que usan las acciones y precondiciones 
    public Dictionary<string, object> GetWorldState()
    {
        Dictionary<string, object> datos = new Dictionary<string, object>();

        //Agregar la snitch al inventario para ver que la atrapo y se caba el juego
        datos.Add("AtrapeLaSnitch", invent.ObtenerCantidadRecurso(TipoDeRecurso.Snitch) > 0);

        return datos;
    }

    //ver si fallo el plan a cumplir
    public void PlanFailed(Dictionary<string, object> FailedGoal)
    {

    }
    public void PlanFound(Dictionary<string, object> Goal, Queue<GoapAction_Merodeadores > actions)
    {

    }
    public void PlanAborted(GoapAction_Merodeadores  abortedAction)
    {

    }
    public bool moveAgent(GoapAction_Merodeadores  nextAction)
    {
        //se mueve hacia su destino (target arbol)
        float velocidadMov = movementSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(
            transform.position, nextAction.Target.transform.position, 
            velocidadMov);

        //verificar si ya llego
        if (Vector3.Distance(transform.position, nextAction.Target.transform.position) < 0.5f)
        {
            nextAction.SetInRange(true);

            //ya llego
            return true;
        }
        else
            //sino no ha llegado
            return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
