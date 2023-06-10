using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndingController : MonoBehaviour
{
    [SerializeField] GameObject dict;
    [SerializeField] Animator finalSceneAnimator;
    [SerializeField] Animator canvasAnimator;
    [SerializeField] AudioSource endingBGM;
    // Start is called before the first frame update
    void Start()
    {
        endingBGM.Play();
        StartCoroutine(PlayGlitterSound());
        StartCoroutine(PlayStampSound());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisableDict()
    {
        dict.SetActive(false);
    }

    public void StartFinalScene()
    {
        finalSceneAnimator.Play("Final_scene_start");
    }

    public void WhiteScreenAnimation()
    {
        canvasAnimator.Play("white_screen_animation");
    }

    public void NormalTransition()
    {
        SceneManager.LoadScene("EndingTransition");
    }
    IEnumerator PlayGlitterSound()
    {
        yield return new WaitForSeconds(42);
        AudioManager.instance.Play("Glitter");
    }
    IEnumerator PlayStampSound()
    {
        yield return new WaitForSeconds(59);
        AudioManager.instance.Play("Stamp");
    }
}
