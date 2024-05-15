﻿using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// 
    /// </summary>
    [Action("Quidditch/Steering/SeekBT")]
    [Help("Seek")]
    public class SeekBT : GOAction
    {
        ///<value>Input Game object where the force is applied Parameter.</value>
        [InParam("SteeringNewbies")]
        [Help("Steering Component")]
        public SteeringCombined steering;

        ///<value>Input Force to be applied Parameter.</value>
        [InParam("Active")]
        [Help("Está activo o no")]
        public bool activo;

        ///<value>Input Force to be applied Parameter.</value>
        [InParam("Weight")]
        [Help("Ponderación")]
        public float weight;

        ///<value>Input Force to be applied Parameter.</value>
        [InParam("Target")]
        [Help("Objetivo")]
        public GameObject Target;



        /// <summary>Initialization Method of ApplyForce.</summary>
        /// <remarks>heckea the GameObject which we apply the force, look for the rigitbody component to add strength
        /// and if it does not exist, it adds rigitbody by default.</remarks>
        public override void OnStart()
        {
            if(steering == null)
                steering = gameObject.GetComponent<SteeringCombined>();

            steering.seek = activo;
            steering.seekWeight = weight;
            steering.Target = Target.transform;
        }

        /// <summary>Abort method of ApplyForce.</summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}
