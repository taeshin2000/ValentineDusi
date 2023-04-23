using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    [SerializeField] Animator myAnimator;
    [SerializeField] GameObject self;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void ToggleAnswer(){
        myAnimator.SetTrigger("Answer");
    }
    public void ToggleThink(){
        if(self.activeInHierarchy){
            StartCoroutine("enemy1think");
         }    
    }
    IEnumerator enemy1think(){
        yield return new WaitForSeconds(0.5f);
        myAnimator.SetTrigger("Think");
    }
}
