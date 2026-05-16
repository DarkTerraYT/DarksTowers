using System;
using System.Collections;
using System.Collections.Generic;
using BTD_Mod_Helper;
using MelonLoader;
using MelonLoader.Utils;
using UnityEngine;
using Random = System.Random;

namespace DarksTowers.Displays;

[RegisterTypeInIl2Cpp(false)]
public class IdleAnimationCycler : MonoBehaviour
{
    private static readonly int IdleHash = Animator.StringToHash("Idle");

    public readonly struct IdleVariantData(int index, string name, float weight)
    {
        public readonly int Index = index;
        public readonly string Name = name;
        public readonly float Weight = weight;
    }

    public List<IdleVariantData> variants = [];
    
    public Animator animator;
    bool isAltPlaying = false;
    bool check = false;

    private string nameCache = "";
    
    private Random rand;

    float timeBetweenChecks = 3;

    public float TimeBetweenChecks
    {
        get => timeBetweenChecks;
        set
        {
            nextCheckTime -= timeBetweenChecks;
            timeBetweenChecks = value;
            nextCheckTime += timeBetweenChecks;
        }
    }
    float nextCheckTime = 0;
    
    void Start()
    {
        rand = new Random();
        if (!animator)
        {
            animator = GetComponent<Animator>();
        }
    }
    
    void Update()
    {
        if (Time.time >= nextCheckTime && !isAltPlaying)
        {
            float prng = (float)rand.NextDouble();

            float currentWeight = 0;
            foreach (var data in variants)
            {
                currentWeight += data.Weight;
                if (prng <= currentWeight)
                {
                    animator.SetInteger(IdleHash, data.Index);
                    nameCache = data.Name;
                    isAltPlaying = true;
                    check = true;
                    
                    break;
                }
                
            }
        }
        else if(check)
        {
            var info = animator.GetCurrentAnimatorStateInfo(0);
            if (info.IsName(nameCache))
            {
                check = false;
                nameCache = "";
                MelonCoroutines.Start(WaitForAnimEnd(info));
            }
        }
    }

    IEnumerator WaitForAnimEnd(AnimatorStateInfo stateInfo)
    {
        yield return new WaitForSeconds(stateInfo.length);
        nextCheckTime = Time.time + timeBetweenChecks;
        isAltPlaying = false;
            animator.SetInteger(IdleHash, 0);
    }
}