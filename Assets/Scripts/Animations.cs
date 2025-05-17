using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WalkAnimation(bool value)
    {
        animator.SetBool("Walk", value);
    }

    public void SprintAnimation(bool value)
    {
        animator.SetBool("Sprint", value);
    }

    public void JumpAnimation()
    {
        animator.Play("Rig|Jump_Start");
    }

    public void CrouchAnimation()
    {
        animator.Play("Rig|Crouch_Idle_Loop");
    }

    public void SittingAnimation()
    {
        animator.Play("Rig|Sitting_Enter");
    }

    public void ReloadAnimation()
    {
        animator.Play("Rig|Pistol_Reload");
    }

    public void InteractAnimation()
    {
        animator.Play("Rig|Interact");
    }

    public void GrabbingAnimation()
    {
        animator.Play("Rig|Fixing_Kneeling");
    }

    public void AimAnimation(bool value)
    {
        animator.SetBool("Aim", value);
    }

    public void ShootAnimation()
    {
        animator.Play("Rig|Pistol_Shoot");
    }
}