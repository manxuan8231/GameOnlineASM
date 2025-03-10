using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        Firing();//
    }
    void Firing()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            _animator.SetBool("isFire", true);
        }
        else
        {
            _animator.SetBool("isFire", false);
        }
    }
}
