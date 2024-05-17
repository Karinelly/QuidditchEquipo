using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// 
    /// </summary>
    [Action("Quidditch/Steering/AcompañarJugador")]
    [Help("Seek")]
    public class AcompañarAliado : GOAction
    {

        private Cohesion_Merodeadores cohesion;
        private Alinegment_Merodeadores aligment;
        private Separation_Merodeadores separation;
        ///<value>Input Force to be applied Parameter.</value>
        
        [InParam("Active")]
        [Help("Está activo o no")]
        public bool activo;

        ///<value>Input Force to be applied Parameter.</value>
        [InParam("Weight")]
        [Help("Ponderación")]
        public float weight;




        /// <summary>Initialization Method of ApplyForce.</summary>
        /// <remarks>heckea the GameObject which we apply the force, look for the rigitbody component to add strength
        /// and if it does not exist, it adds rigitbody by default.</remarks>
        public override void OnStart()
        {

            if (cohesion == null)
                cohesion = gameObject.GetComponent<Cohesion_Merodeadores>();

            if (aligment == null)
                aligment = gameObject.GetComponent<Alinegment_Merodeadores>();

            if (separation == null)
                separation = gameObject.GetComponent<Separation_Merodeadores>();

            cohesion.active = activo;
            cohesion.weigth = 1f;

            aligment.active = activo;
            aligment.weigth = 0.5f;

            aligment.active = activo;
            aligment.weigth = 0.5f;


        }

        /// <summary>Abort method of ApplyForce.</summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}
