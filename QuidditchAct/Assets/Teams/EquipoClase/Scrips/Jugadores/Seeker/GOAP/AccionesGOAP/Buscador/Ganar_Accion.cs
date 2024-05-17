using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ganar_Accion : GoapAction_Merodeadores
{
    //acceder al game manager
    public GameObject Manager;

    //variables de ejecución
    private bool terminado = false;
    //ya inicio y cuando
    private float tiempoInicio = 0f;

    public float duracionAccion = 1f;
    
    void Awake()
    {
        Manager = GameObject.FindGameObjectWithTag("Manager");
    }
    public Ganar_Accion()
    {
        //precondiciones para que se ejecute
        AddPrecondition("AtrapeLaSnitch", true);

        //defininir efectos
        AddEffect("SeAcaboElJuego", true);
        //AddEffect("NoTengoLaSnitch" , false);
    }

    public override bool checkPrecondition(GameObject gameObject)
    { 
        GameObject target = BuscarObjetivoCercano(gameObject.transform.position);

        if(target != null)
        {
            target = target;
            return true;
        }

        return false;
        //El agente debe detenerse
        //si ya cuenta con la snitch en su inventario
        //if(target != null)
        //ponerle el if
        //float dist = Vector3
        /*{
            Manager.GetComponent<GameManager>().GrabSnitch(gameObject);
            return true;
        }*/
        //return false; //sino no regresa nada y busca otro plan
    }
    
   
    //funcion de que ya acabo
    public override bool isDone()
    {
        return terminado;
    }

    private GameObject BuscarObjetivoCercano(Vector3 posicion)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Seeker1StartPosition");
        GameObject targetCercano = null;
        float distanciaMenor = Mathf.Infinity;

        foreach(GameObject objetivo in targets)
        {
            float dist = Vector3.Distance(objetivo.transform.position, posicion);
            if(dist < distanciaMenor)
            {
                //Encontramos un objetivo mas cerca
                targetCercano = objetivo;
                distanciaMenor = dist;
            }
        }
        return targetCercano;
    }

    // Aqui va ir el perform
    public override bool Perform(GameObject gameObject)
    { 
        if (tiempoInicio == 0f)
        {
             tiempoInicio = Time.timeSinceLevelLoad;
             Debug.Log("Regresando al punto de inicio, devoré");
        }
        //para ganar se debe atrapar la snitch(tenerla en el inventario)
        Inventario_Merodeadores invent = gameObject.GetComponent<Inventario_Merodeadores>();
        //si en el inventario es >=1
        if (invent.ObtenerCantidadRecurso(TipoDeRecurso.Snitch) >=1)
        {
            //Acabar el juego
            Manager.GetComponent<GameManager>().GrabSnitch(gameObject);

            terminado = true;
            return true;
        }
        terminado = true;
        return true;
    }


    /// lo que sigue
     public override bool requiresInRange()
    {
        //si necesita estar cerca del target para cumplir su accion
        return true;
    }
    public override void mReset()
    {
        terminado = false;
        Target = null;
        tiempoInicio = 0f;
    }
}
