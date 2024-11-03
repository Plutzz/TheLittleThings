using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This state plays all states in the list in order one after another
public class StateSequence : State
{
    [SerializeField] private List<State> states;
    private int currentStateIndex;

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        currentStateIndex = 0;
        stateMachine.SetState(states[currentStateIndex]);
    }
    public override void CheckTransitions()
    {
        base.CheckTransitions();
        if (!currentState.isComplete) return;

        currentStateIndex++;

        // If we are not on the last state go to the next state
        if (currentStateIndex != states.Count - 1)
        {
            stateMachine.SetState(states[currentStateIndex]);
        }
        // If we are on the last state, mark this state as true
        else
        {
            isComplete = true;
        }
    }
}
