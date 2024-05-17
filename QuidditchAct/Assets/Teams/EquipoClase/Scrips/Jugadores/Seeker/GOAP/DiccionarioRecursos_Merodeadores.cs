using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DiccionarioRecursos_Merodeadores
{
    //diccionario para almacenar los diferentes recursos y sus cantidades
    public Dictionary<TipoDeRecurso, int> Recursos = new Dictionary<TipoDeRecurso, int>();
    
}
