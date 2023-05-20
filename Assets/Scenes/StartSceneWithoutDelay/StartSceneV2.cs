using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneV2 : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Animator mcAnimator;
    [SerializeField] Animator twins1Animator;
    [SerializeField] Animator twins2Animator;
    [SerializeField] Animator mascotAnimator;
    [SerializeField] Animator senpaiAnimator;
    [SerializeField] Animator triangleEffectAnimator;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Animator logoAnimator;

    [SerializeField] Animator startButtonAnimator;
    [SerializeField] Animator exitButtonAnimator;
    void Start()
    {
        StartCoroutine("ExitButtonStart");
        StartCoroutine("StartButtonStart");
        Time.timeScale = 1f;
        audioSource.Play();
        logoAnimator.Play("Logo_start_without_delay");
        mcAnimator.Play("mc_start");
        twins1Animator.Play("twins1_start");
        twins2Animator.Play("twins2_start");
        mascotAnimator.Play("mascot_start");
        senpaiAnimator.Play("senpai_start");
        triangleEffectAnimator.Play("effect_triangles");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ExitButtonStart()
    {
        yield return new WaitForSeconds(2);
        exitButtonAnimator.Play("quit_start");
    }

    IEnumerator StartButtonStart()
    {
        yield return new WaitForSeconds(0.5f);
        startButtonAnimator.Play("startButton");
    }
}
