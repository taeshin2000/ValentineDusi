using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
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

    public void ToggleCorrect()
    {
        myAnimator.SetTrigger("Correct");
    }

    public void ToggleWrong()
    {
        myAnimator.SetTrigger("Wrong");
    }
}
