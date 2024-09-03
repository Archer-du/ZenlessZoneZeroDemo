using UnityEngine;
using View;

namespace ZZZDemo.Runtime.Behavior.View
{
    public class ViewProxy : IViewHandler
    {
        private Animator animator;
        
        public ViewProxy(Animator animator)
        {
            this.animator = animator;
        }

        public void Update(float deltaTime)
        {
            
        }

        public IMovementComponent Movement { get; }
        public IAnimationComponent animation { get; }
    }
}