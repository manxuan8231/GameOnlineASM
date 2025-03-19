using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour
{
    public void SetupCamera()
    {
        if (!Object.HasStateAuthority) return;
        //CameraFollowOn cameraFollow = FindAnyObjectByType<CameraFollowOn>();
        // if (cameraFollow != null) cameraFollow.AssignCamera(transform);
        CameraController cameraController = FindAnyObjectByType<CameraController>();
        if (cameraController != null) cameraController.FollowPlayer(transform);
       
    }
    //setup diem, mp, hp cho nhan vat
    public void SetupPlayer()
    {
        
    }
}

