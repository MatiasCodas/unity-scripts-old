using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class ShipAnimation : MonoBehaviour
{
    public string idleAnimation = "Inercia";
    public string moveAnimation = "Propulsao";
    public string spinAnimation = "Rodando";
    public string hitAnimation = "Colisao";
    public string dieAnimation = "Morte";

    public enum State { Idle, Move, Spin, Hit, Die}
    public State state = State.Idle;

    private UnityArmatureComponent armature;

    public bool isMoving;
    public bool hit;
    public bool spin;

    private float timeAnim;


    private void Start()
    {
        armature = GetComponentInChildren<UnityArmatureComponent>();
    }

    private void Update()
    {
        if (hit)
        {
            StartCoroutine("PlayAnim", "Hit");
        }

        else if (spin)
        {
            StartCoroutine("PlayAnim", "Spin");
        }

         else if (isMoving)
        {
            Move();
        }

        else
        {
            Idle();
        }
    }

    public void Idle()
    {
        if(state != State.Die && state != State.Idle)
        {
            armature.animation.FadeIn(idleAnimation, 0.5f, -1);
            armature.animation.timeScale = 1f;
            state = State.Idle;
        }
    }

    public void Move()
    {
        if (state != State.Die && state != State.Move)
        {
            armature.animation.FadeIn(moveAnimation, 0.5f, -1);
            armature.animation.timeScale = 1f;
            state = State.Move;
        }
    }

    public void Spin()
    {
        if (state != State.Die && state != State.Spin)
        {
            armature.animation.FadeIn(spinAnimation, 0.5f, -1);
            armature.animation.timeScale = 1f;
            state = State.Spin;
        }
    }

    public void Hit()
    {
        if (state != State.Die && state != State.Hit)
        {
            armature.animation.FadeIn(hitAnimation, 0.5f, 1);
            armature.animation.timeScale = 1f;
            state = State.Hit;
        }
    }

    public void Die()
    {
        if (state != State.Die)
        {
            armature.animation.FadeIn(dieAnimation, 0.5f, -1);
            armature.animation.timeScale = 1f;
            state = State.Die;
        }
    }

    IEnumerator PlayAnim(string anim)
    {
        TimingAnimation(anim);
        yield return new WaitForSeconds(timeAnim);
        switch (anim)
        {
            case "Hit":
                spin = true;
                hit = false;
                break;

            case "Spin":
                spin = false;
                hit = false;
                break;

        }
    }

    void TimingAnimation(string anim)
    {
        switch (anim)
        {
            case "Hit":
                timeAnim = 0.5f;
                Hit();
                break;

            case "Spin":
                timeAnim = 1.5f;
                Spin();
                break;

        }
    }
}
