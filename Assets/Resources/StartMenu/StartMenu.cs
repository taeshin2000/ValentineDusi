using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class StartMenu : MonoBehaviour
{
    [SerializeField] Animator mcAnimator;
    [SerializeField] Animator twins1Animator;
    [SerializeField] Animator twins2Animator;
    [SerializeField] Animator mascotAnimator;
    [SerializeField] Animator senpaiAnimator;
    [SerializeField] Animator triangleEffectAnimator;
    [SerializeField] Animator fall1Animator;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Animator logoAnimator;



    // Start is called before the first frame update
    void Start()
    {
        startMusic();
        StartCoroutine("SceneStart");
        StartCoroutine("LogoStart");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loadingFinished()
    {
        Debug.Log("loading finished !!!");
    }

    IEnumerator SceneStart()
    {
        yield return new WaitForSeconds(5);
        mcAnimator.Play("mc_start");
        twins1Animator.Play("twins1_start");
        twins2Animator.Play("twins2_start");
        mascotAnimator.Play("mascot_start");
        senpaiAnimator.Play("senpai_start");
        triangleEffectAnimator.Play("effect_triangles");
    }

    IEnumerator Fall1()
    {
        yield return new WaitForSeconds(7);
        fall1Animator.Play("fall");
    }

    IEnumerator LogoStart()
    {
        yield return new WaitForSeconds(5);
        logoAnimator.Play("Logo_start_without_delay");
    }

    public void startMusic()
    {
        audioSource.PlayDelayed(5f);
    }
}
