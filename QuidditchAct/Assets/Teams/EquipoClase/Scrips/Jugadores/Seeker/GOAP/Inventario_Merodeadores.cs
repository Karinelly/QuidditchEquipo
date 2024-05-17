using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tipos de recursos existentes
public enum TipoDeRecurso
{
    Snitch
}


public class Inventario_Merodeadores : MonoBehaviour
{
    public List<int> Recursos;

    public DiccionarioRecursos_Merodeadores Diccionario;

    //funcion para agregar recursos al inventario cuando el agente quiera agregar algo
    public void AgregarRecurso(TipoDeRecurso pRecurso, int cantidad)
    {
        Recursos[(int)pRecurso] += cantidad;
        
    }

    //metodo para saber la cantidad de recursos de un tipo en el inventario
    public int ObtenerCantidadRecurso(TipoDeRecurso recurso)
    {
        return Recursos[(int)recurso];
    }

    //metodo para quitar recursos
    public void QuitarRecursos(TipoDeRecurso recurso, int cantidad)
    {
        Recursos[(int)recurso] -= cantidad;
        if (Recursos[(int)recurso] <= 0)
            Recursos[(int)recurso] = 0;
    }
    // Start is called before the first frame update
    void Awake()
    {
        //inicializar todo con 0
        Recursos = new List<int>();
        Recursos.Add(0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
