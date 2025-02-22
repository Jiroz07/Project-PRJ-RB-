using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public Animator animator;

    void Awake()
    {
        Instance = this;
    }

    public void SetAnimation(string name)
    {
        animator.Play(name);
    }

}