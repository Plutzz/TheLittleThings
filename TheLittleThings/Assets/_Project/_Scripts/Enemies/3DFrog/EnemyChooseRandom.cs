using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This state randomly chooses from a list of child states
// Ex: Choose randomly from a list of attacks
public class EnemyChooseRandom : State
{
    [SerializeField] private List<State> states;
    private State state;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        state = states[Random.Range(0, states.Count)];
        stateMachine.SetState(state, true);
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();
        if (state?.isComplete ?? false)
        {
            isComplete = true;
        }
    }
}
