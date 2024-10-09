using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Arraste o jogador para este campo no Inspector
    public Vector3 offset;    // Defina o deslocamento da câmera em relação ao jogador

    void Update()
    {
        // Atualiza a posição da câmera baseada na posição do jogador e o offset
        transform.position = player.position + offset;
    }
}
