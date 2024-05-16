using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// It is an action to activate a GameObject.
    /// </summary>
    [Action("Cartas de accion/MoverseIdle")]
    [Help("Se mueve con Path Following")]
    public class Merodeadores_Mov_idle : GOAction
    {
        

        public override void OnStart()
        {
            Debug.Log("Entro A movIdle");


            Equipo_keeperMerodeadores Keppp = gameObject.GetComponent<Equipo_keeperMerodeadores>();


            Keppp.Lanzado = false;
            Keppp.Detenido = false;
            Keppp.Intercept = false;

            //Keppp.EnPosPorteria = false;

            Keppp.EnMovIdle = true;
            //acceder a script player
            //Player playerInfo = gameObject.GetComponent<Player>();

            //llamar al codigo para que inicie el pathFollow

            //usamos la funcion de daño en el rival
            //playerInfo.GetEnemyInfo().Damage();
        }

        /// <summary>Method of Update of SetActive.</summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}
