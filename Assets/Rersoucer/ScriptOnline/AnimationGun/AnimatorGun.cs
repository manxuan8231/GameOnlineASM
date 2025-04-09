using Fusion;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class AnimatorGun : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(OnFireChanged))]
   private bool fireAnimator {  get; set; }
   
    public Animator animator;
    void OnFireChanged() 
    {
      animator.SetBool("Fire",fireAnimator);       
    }

    private void Update()
    {
        if (Object.HasInputAuthority)
        {
            PlayerGun playerGun = FindAnyObjectByType<PlayerGun>();
            if (playerGun.currentAmmo > 0)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    fireAnimator = true;
                }
                else
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    fireAnimator = false;
                }
                else if (Input.GetKey(KeyCode.Q))
                {
                    fireAnimator = true;
                }
                else if (Input.GetKeyUp(KeyCode.Q))
                {
                    fireAnimator = false;
                }
            }
            else
            {
                fireAnimator = false;
            }
        }
    }
}
