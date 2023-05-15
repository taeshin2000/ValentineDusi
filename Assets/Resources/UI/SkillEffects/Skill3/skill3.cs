using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill3 : MonoBehaviour
{
    [SerializeField] Animator skill3Animator;

    // Start is called before the first frame update
    void Start(){
        skill3Animator.Play("skill3idle");
    }

    public void idleAbility3(){
        skill3Animator.SetTrigger("idle");
    }
}
