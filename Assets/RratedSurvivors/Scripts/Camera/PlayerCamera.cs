using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float maxPosX;
    [SerializeField] private float maxPosY;

    private Vector3 cameraPos = Vector3.zero;
    

    private void LateUpdate()
    {
        GameObject player = Managers.GameManager.player;
        if (player == null)
            return;

        Transform playerTransform2 = player.transform;
        cameraPos.Set(playerTransform2.localPosition.x, playerTransform2.localPosition.y, transform.position.z);

        float height = Camera.main.orthographicSize;
        float width = height * Screen.width / Screen.height;

        float lx = maxPosX - width;
        float clampX = Mathf.Clamp(cameraPos.x, -lx, lx);

        float ly = maxPosY - height;
        float clampY = Mathf.Clamp(cameraPos.y, -ly, ly);

        cameraPos.Set(clampX, clampY, transform.position.z);
        transform.position = cameraPos;
    }
}
