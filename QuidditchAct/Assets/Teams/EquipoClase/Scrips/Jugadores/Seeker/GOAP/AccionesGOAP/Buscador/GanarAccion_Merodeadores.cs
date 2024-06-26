using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GanarAccion_Merodeadores : GoapAction_Merodeadores
{
    //acceder al game manager

    //variables de ejecución
    private bool terminado = false;
    //ya inicio y cuando
    private float tiempoInicio = 0f;

    public float duracionAccion = 0f;
    
    public GanarAccion_Merodeadores()
    {
        //precondiciones para que se ejecute
        AddPrecondition("AtrapeLaSnitch", true);

        //defininir efectos
        //AddEffect("ComezoJuego", false);
        //AddEffect("SeAcaboElJuego", true);
        //AddEffect("NoTengoLaSnitch" , false);
        //AddEffect("AtrapelaSnitch", true);
    }

    public override bool checkPrecondition(GameObject gameObject)
    { 
        //GameObject target = BuscarObjetivoCercano(gameObject.transform.position);
        //El agente debe estar cerca de la snitch
        GameObject target = BuscarObjetivoCercano(gameObject.transform.position);
        if(target != null)
        {
            target = target;
            return true;
        }
        
        Debug.Log("Porque entra aqui?");
        return false; //sino no regresa nada y busca otro plan
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
        GameManager.instancia.GrabSnitch(this.gameObject);
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