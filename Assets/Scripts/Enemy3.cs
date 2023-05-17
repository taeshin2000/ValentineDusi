using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    [SerializeField] Animator myAnimator;
    [SerializeField] Animator effectAnimator;
    [SerializeField] GameObject self;

    public bool wakeUp = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleAnswer()
    {
        if (wakeUp)
        {
            myAnimator.SetTrigger("Answer");
            effectAnimator.Play("wakeup_correct");
            wakeUp = false;
        }
        else
        {
            effectAnimator.Play("normal_correct");
        }
    }

    public void ToggleThink()
    {
        if (self.activeInHierarchy)
        {
            StartCoroutine("enemy3think");
        }
    }

    IEnumerator enemy3think()
    {
        yield return new WaitForSeconds(0.5f);
        myAnimator.SetTrigger("Think");
        effectAnimator.Play("think_effect");
    }
}
