using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class CameraFollowOn : MonoBehaviour
{
    public CinemachineCamera FollowCamera;

    public void AssignCamera(Transform target)
    {
       FollowCamera.Follow = target;
       FollowCamera.LookAt = target;
    }
   
}
