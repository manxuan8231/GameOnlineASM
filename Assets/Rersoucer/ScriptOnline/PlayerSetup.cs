using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour
{
    public void SetupCamera()
    {
        if (!Object.HasStateAuthority) return;        
        CameraFollow cameraFollow = FindAnyObjectByType<CameraFollow>();
        if (cameraFollow != null) cameraFollow.AssignCamera(transform);                                   
    }
    //setup diem, mp, hp cho nhan vat
    public void SetupPlayer()
    {
        
    }
}

