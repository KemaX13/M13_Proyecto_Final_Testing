using System;

namespace FSM {
    public abstract class BaseState<EState> where EState : Enum {
        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
    }
}
