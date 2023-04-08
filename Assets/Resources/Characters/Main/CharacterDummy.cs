using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDummy : MonoBehaviour
{
    [SerializeField] Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TriggerCorrect()
    {
        myAnimator.SetTrigger("Correct");
    }

    public void TriggerWrong()
    {
        myAnimator.SetTrigger("Wrong");
    }
}
