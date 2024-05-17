using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitarRecurso_Merodeadores : MonoBehaviour
{
    Inventario_Merodeadores inventario;
    public TipoDeRecurso recurso;

    private void RecursoTerminado()
    {
        //Charcaer si en su inventario ya no tiene el tipo de recurso que provee
        if (inventario.ObtenerCantidadRecurso(recurso) <= 0)
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Awake()
    {
        inventario = GetComponent<Inventario_Merodeadores>();
    }
    private void Start()
    {
        //darle una cantidad de recursos al inicio
        inventario.AgregarRecurso(recurso, 10);
    }

    // Update is called once per frame
    void Update()
    {
        RecursoTerminado();
    }
}
