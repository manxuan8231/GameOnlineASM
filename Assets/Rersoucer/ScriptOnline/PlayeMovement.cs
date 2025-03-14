using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeMovement : NetworkBehaviour
{
    public CharacterController controller;
    public float speed = 12f;

    public override void FixedUpdateNetwork()
    {
        //kiểm tra xem có phải người chơi điều khiển không 
        if (!Object.HasStateAuthority) return;

        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        var move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.fixedDeltaTime);
    }

}