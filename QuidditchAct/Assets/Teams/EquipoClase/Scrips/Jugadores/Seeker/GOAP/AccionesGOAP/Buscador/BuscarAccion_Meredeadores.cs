using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuscarAccion_Meredeadores : GoapAction_Merodeadores
{
    //variables de ejecución
    private bool terminado = false;
    //ya inicio y cuando
    private float tiempoInicio = 0f;

    //public float duracionAccion = 0f;
    
    public BuscarAccion_Meredeadores()
    {
        //precondiciones para que se ejecute
        AddPrecondition("NoTengoLaSnitch", true);

        //defninir efectos
        AddEffect("AtrapeLaSnitch", true);
    }

    public override bool checkPrecondition(GameObject gameObject)
    {
        //El agente debe estar cerca de la snitch
        GameObject target = BuscarObjetivoCercano(gameObject.transform.position);

        //si ecuentra un objetivo
        if(target != null)
        {
            Target = target;
            return true;
        }
        return false; //sino no regresa nada y busca otro plan
    }
    private GameObject BuscarObjetivoCercano(Vector3 posicion)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Ball Snitch");
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
             Debug.Log("Buscando la Snitch");
        }
           
        //cundo haya pasado el tiempo9 para cumplir la accion, realiza sus cambios
        //if(Time.timeSinceLevelLoad > tiempoInicio + duracionAccion)
        //{
            //Para tomara Herramienta hay que ver si hay disponibles
            Inventario_Merodeadores inventarioAlmacen = Target.GetComponent<Inventario_Merodeadores>();
            if(inventarioAlmacen.ObtenerCantidadRecurso(TipoDeRecurso.Snitch) > 0)
            { 

                //Termina la accion
                //Deberia haber depositado la madera en el almacen y se queda sin ese recurso
                Inventario_Merodeadores invent = gameObject.GetComponent<Inventario_Merodeadores>();
                if(invent != null)
                {
                    //quitar unidades de madera
                    invent.QuitarRecursos(TipoDeRecurso.Snitch, 1);
                    //Debug.Log(invent.ObtenerCantidadRecurso(TipoDeRecurso.Madera));

                }
                    //quitar la snitch de la snitch
                    inventarioAlmacen.QuitarRecursos(TipoDeRecurso.Snitch, 1);

                    terminado = true;

                    return true;
            
            }
            /*else
            {
                //si no hay herramienta en el almacen, no se puede completar la tarea
                return false;

            }*/
        //}
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
