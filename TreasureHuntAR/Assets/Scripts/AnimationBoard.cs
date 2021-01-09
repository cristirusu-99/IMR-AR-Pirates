using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBoard : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        anim.Play("Floating");
    }
}