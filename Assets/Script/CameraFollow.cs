using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Vector3 cameraDistance = new Vector3(0, 0, 0);
    [SerializeField] float followSpeed;
    private Transform playerTransform;
    
    void LateUpdate()
    {
        if (playerTransform == null)
        {
            var localPlayer = NetworkClient.localPlayer;
            if (localPlayer != null)
            {
                playerTransform = localPlayer.transform;
            }
        }

        if (playerTransform != null)
        {
            Vector3 targetPosition = playerTransform.position + cameraDistance;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
