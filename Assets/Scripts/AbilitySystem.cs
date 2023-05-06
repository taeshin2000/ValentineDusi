using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class AbilitySystem : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler
{

    private static LTDescr delay;

    public static AbilitySystem abilitySystem;

    public bool isExpand = false;
    public Animator animator;
    public void Awake() {
        abilitySystem = this ;
    }
    public void OnPointerEnter(PointerEventData eventData){
        Debug.Log("First");
        Expand();
    }
    public void OnPointerExit(PointerEventData eventData){
        delay = LeanTween.delayedCall(0.01f,()=>{
            Condense();
        });
    }
    public void Expand(){
        if (delay != null){
            LeanTween.cancel(delay.uniqueId);
        }
        abilitySystem.animator.SetTrigger("Expand");
        abilitySystem.animator.ResetTrigger("Condense");
    }
    public void Condense(){
        abilitySystem.animator.SetTrigger("Condense");
        abilitySystem.animator.ResetTrigger("Expand");
    }
    public void setIsExpandTrue(){
        isExpand = true;
    }
    public void setIsExpandFalse(){
        isExpand = false;
    }
}
