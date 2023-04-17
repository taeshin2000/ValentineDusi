using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] Animator mcAnimator;
    [SerializeField] Animator triangleEffectAnimator;
    [SerializeField] Animator fall1Animator;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("McStart");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loadingFinished()
    {
        Debug.Log("loading finished !!!");
    }

    IEnumerator McStart()
    {
        yield return new WaitForSeconds(5);
        triangleEffectAnimator.Play("effect_triangles");
    }

    IEnumerator Fall1()
    {
        yield return new WaitForSeconds(7);
        fall1Animator.Play("fall");
    }
}
