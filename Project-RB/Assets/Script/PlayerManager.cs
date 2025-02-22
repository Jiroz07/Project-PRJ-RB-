using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public Animator animator;
    public float velocidad = 5f;

    void Awake()
    {
        Instance = this;
    }


    public void SetAnimation(string name)
    {
        animator.Play(name);
    }


}