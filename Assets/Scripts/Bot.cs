using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] float minTime;
    [SerializeField] float maxTime;
    [SerializeField] int percentToPickMaster;
    [SerializeField] int percentToPickAdvanced;
    [SerializeField] int percentToPickBasic;
    [SerializeField] int percentToPickWrong;
    int wrong;
    int basic;
    int advanced;
    int master;

    void Start()
    {
        wrong = percentToPickWrong;
        basic = wrong + percentToPickBasic;
        advanced = basic + percentToPickAdvanced;
        master = advanced + percentToPickMaster;
    }

    void Update()
    {

    }

    public float CalculateTime()
    {
        return Random.Range(minTime, maxTime);
    }

    public string CalculateAnswer()
    {
        int num = Random.Range(0, 100);
        if (num <= wrong)
        {
            return "wrong";
        }
        else if (num <= basic)
        {
            return "basic";
        }
        else if (num <= advanced)
        {
            return "advance";
        }
        else
        {
            return "master";
        }
    }
}
