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

    public GameObject effect;
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
                    effect.SetActive(true);
                }
                else
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    fireAnimator = false;
                    effect.SetActive(false);
                }
                else if (Input.GetKey(KeyCode.Q))
                {
                    fireAnimator = true;
                    effect.SetActive(true);
                }
                else if (Input.GetKeyUp(KeyCode.Q))
                {
                    fireAnimator = false;
                    effect.SetActive(false);
                }
            }
            else
            {
                fireAnimator = false;
                effect.SetActive(false);
            }
        }
    }
}
