using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatTrigger : MonoBehaviour
{
    // Esse método é chamado quando outro objeto entra no Trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que colidiu tem a tag "Player"
        if (other.gameObject.tag == "player")
        {
            // Carrega a cena da vitória
            SceneManager.LoadScene("DefeatScene"); // Certifique-se de que o nome da cena está correto
        }
    }
}