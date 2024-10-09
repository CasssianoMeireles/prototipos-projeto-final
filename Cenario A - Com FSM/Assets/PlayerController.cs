using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // Captura o input horizontal e vertical (setas ou WASD)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calcula o movimento
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Move o personagem
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
}
