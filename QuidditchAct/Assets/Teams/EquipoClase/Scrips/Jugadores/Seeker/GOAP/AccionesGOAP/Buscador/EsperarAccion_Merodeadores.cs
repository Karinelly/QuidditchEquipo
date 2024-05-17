using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsperarAccion_Merodeadores : GoapAction_Merodeadores
{

    //variables de ejecución
    private bool terminado = false;
    //ya inicio y cuando
    private float tiempoInicio = 0f;

    public float duracionAccion = 3f;
    
    public EsperarAccion_Merodeadores()
    {
        //No tiene precondiciones por lo tanto se ejecuta primero
        //AddEffect

        //defininir efectos
        AddEffect("NoTengoLaSnitch", true);
        AddEffect("ComezoJuego", true);
        //AddEffect("NoTengoLaSnitch" , false);
    }

    public override bool checkPrecondition(GameObject gameObject)
    { 
        //El agente debe estar cerca de la posicion de inicio
        GameObject target = BuscarObjetivoCercano(gameObject.transform.position);
        //GameObject[] targets = GameObject.FindGameObjectsWithTag("Seeker1StartPosition");

        if(target != null)
        {
            Target = target;
            return true;
        }

        return false;
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
    
   
    //funcion de que ya acabo
    public override bool isDone()
    {
        return terminado;
    }

    // Aqui va ir el perform
    public override bool Perform(GameObject gameObject)
    {
        if (tiempoInicio == 0f)
        {
             tiempoInicio = Time.timeSinceLevelLoad;
             Debug.Log("Voy a mi posicion de inicio");
        }
           
        //cundo haya pasado el tiempo9 para cumplir la accion, realiza sus cambios
        if(Time.timeSinceLevelLoad > tiempoInicio + duracionAccion)
        {

                //Termina la accion
                    terminado = true;

                    return true;
            
             }
            /*else
            {
                //si no hay herramienta en el almacen, no se puede completar la tarea
                return false;

            }
            */
       
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

