using UnityEngine;

public class Death : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        var obj = animator.gameObject;
        Destroy(obj);
    }
}
