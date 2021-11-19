using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DUCK.Tween
{
    public class ScaleAnimationWhile : TimedAnimation
    {
        private readonly Vector3 from;
        private readonly Vector3 to;
        private readonly Func<bool> scaleWhile;

        /// <summary>
        /// Creates a new ScaleAnimation
        /// </summary>
        /// <param name="target">The target GameObject of this Animation</param>
        /// <param name="from">The start scale of the ScaleAnimation</param>
        /// <param name="to">The end scale of the ScaleAnimation</param>
        /// <param name="duration">The duration of the animation in seconds, defaults to 1f</param>
        /// <param name="easingFunction">The easing function that will be used to interpolate with</param>
        public ScaleAnimationWhile(GameObject target, Vector3 from, Vector3 to, Func<bool> scaleWhile, float duration = 1f, Func<float, float> easingFunction = null)
                : base(target, duration, easingFunction)
        {
            this.from = from;
            this.to = to;
            this.scaleWhile = scaleWhile;
        }

        /// <summary>
        /// Creates a new ScaleAnimation
        /// </summary>
        /// <param name="target">The target GameObject of this Animation</param>
        /// <param name="from">The start scale of the ScaleAnimation</param>
        /// <param name="to">The end scale of the ScaleAnimation</param>
        /// <param name="duration">The duration of the animation in seconds, defaults to 1f</param>
        /// <param name="easingFunction">The easing function that will be used to interpolate with</param>
        public ScaleAnimationWhile(GameObject target, float from, float to, Func<bool> scaleWhile, float duration = 1f, Func<float, float> easingFunction = null)
            : base(target, duration, easingFunction)
        {
            this.from = new Vector3(from, from, from);
            this.to = new Vector3(to, to, to);
            this.scaleWhile = scaleWhile;
        }

        protected override void Refresh(float progress)
        {
            if (scaleWhile())
            {
                TargetObject.transform.localScale = Interpolate(from, to, progress);
            }
        }
    }
}
