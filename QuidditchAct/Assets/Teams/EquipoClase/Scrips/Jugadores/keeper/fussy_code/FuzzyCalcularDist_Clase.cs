using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyCalcularDist_Clase : MonoBehaviour
{
    [Header("Funcion de pertenencia de distancia")]
    [SerializeField]
    private AnimationCurve Lejos;
    [SerializeField]
    private AnimationCurve Cerca;
    

    private float valorCerca;
    private float valorLejos;

    private float distanciaAQuaffle;

   
    private void EvaluarDistancia()
    {
        valorLejos = Lejos.Evaluate(distanciaAQuaffle);
        valorCerca = Cerca.Evaluate(distanciaAQuaffle);
    }



    public void SetDistancia(float dist)
    {
        distanciaAQuaffle = dist;
    }

    public bool EstaCerca()
    {
        EvaluarDistancia();

        if (valorLejos < valorCerca)
        {
            Debug.Log("esta cerca");
            return true;
        }
        else
        {
            Debug.Log("esta lejos");
            return false;
        }


    }

    /*
    public bool TieneSuficienteVida()
    {
        //pasamos los puntos de vida por las funciones de pertenencia 
        EvaluarSalud();

        //decidimos si tiene suficiente vida


        //para el ejemplo, con que tenga mayor valor de herido que de critico es suficiemte
        if (valorSaludable > valorHerido)
        {
            Debug.Log("saludable");
            return true;
        }
        else
        {
            Debug.Log("NO saludable");
        }

        if(valorHerido > valorCritico)
        {
            return false;
        }
        else
        {
            return false;
        }
        
        
    }

    public bool TieneMasOMenosVida()
    {
        //pasamos los puntos de vida por las funciones de pertenencia 
        EvaluarSalud();

        //decidimos si tiene suficiente vida

        Debug.Log("en herido");
        //para el ejemplo, con que tenga mayor valor de herido que de critico es suficiemte
        if (valorSaludable > valorHerido)
        {
            Debug.Log("en herido");
            return false;



        }
        else if (valorHerido > valorCritico)
        {
            print("VH: " + valorHerido + " vC: " + valorCritico);
            return true;
        }
        else
        {
            return false;
        }


    }
    public bool SinVida()
    {
        //pasamos los puntos de vida por las funciones de pertenencia 
        EvaluarSalud();

        //decidimos si tiene suficiente vida


        //para el ejemplo, con que tenga mayor valor de herido que de critico es suficiemte
        if (valorSaludable > valorHerido)
        {
            Debug.Log("en critico");
            return false;
        }


        if (valorHerido > valorCritico)
        {
            return false;
        }
        else
        {
            return true;
        }


    }


    
    public bool IdentificarVida(int cualCond)
    {
        //pasamos los puntos de vida por las funciones de pertenencia 
        EvaluarSalud();
        EvaluarConteo();

        //decidimos si tiene suficiente vida

        Debug.Log("entra identfc");
        //para el ejemplo, con que tenga mayor valor de herido que de critico es suficiemte
        if(cualCond == 1)
        {
            if (valorSaludable > valorHerido)
            {
                Debug.Log("en herido");
                return true;

            }
            else
            {
                return false;
            }

        }
        else if (cualCond == 2)
        {
            if (valorHerido > valorCritico && valorSaludable < valorHerido)
            {
                print("VH: " + valorHerido + " vC: " + valorCritico);
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (cualCond == 3)
        {
            if (valorHerido < valorCritico)
            {
                print("VH: " + valorHerido + " vC: " + valorCritico);
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (cualCond == 4)
        {
            if (valorMuchas > valorPocasCartas)
            {
                Debug.Log("aun hay");
                return true;

            }
            else
            {
                return false;
            }

        }
        else if (cualCond == 5)
        {
            if (valorPocasCartas > valorMuchas)
            {
                Debug.Log("no hay");
                return true;
            }
            else
            {
                Debug.Log("sale");
                return false;
            }
        }
        else
        {
            return false;
        }

    }*/
}
