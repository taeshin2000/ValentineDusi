using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillGaugeBar : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] Image mask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float fillAmount = (float)gameController.playerSkillGuage / (float) gameController.maxPlayerSkillGauge;
        mask.fillAmount = fillAmount;
    }
}
