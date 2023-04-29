using System;
using System.Collections;
using UnityEngine;

 
namespace MalbersAnimations.Reactions
{
    [Serializable]
    /// <summary>Abstract Class to anything react </summary>
    public abstract class Reaction 
    {
        /// <summary>Instant Reaction ... without considering Active or Delay parameters</summary>
        protected abstract bool _TryReact(Component reactor);

        /// <summary>Get the Type of the reaction</summary>
        public abstract Type ReactionType { get; }

        public void React(Component component) => TryReact(component);

        public void React(GameObject go) => React(go.GetComponentInParent(ReactionType));

        public bool TryReact(Component component)
        {
            if (ReactionType.IsAssignableFrom(component.GetType()))
            {
                return  _TryReact(component);
            }
            else
            {
                return _TryReact(component.GetComponentInParent(ReactionType));
            }
        }
    }
}