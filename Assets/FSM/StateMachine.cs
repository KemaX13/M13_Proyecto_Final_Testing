using System;
using UnityEngine;

namespace FSM {
    public abstract class StateMachine<EState> : MonoBehaviour where EState : Enum {
        [SerializeField] BaseState<EState> currentState;

        protected BaseState<EState> queuedState;
        protected bool IsTransitioningState = false;

        void Start() {
            currentState.EnterState();
        }

        void Update() {
            if(IsTransitioningState)
                return;

            currentState.UpdateState();
        }

        public void TransitionToState() {
            IsTransitioningState = true;
            currentState.ExitState();
            currentState = queuedState;
            currentState.EnterState();
            IsTransitioningState = false;
        }
    }
}
