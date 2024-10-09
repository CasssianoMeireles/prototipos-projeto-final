using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Transform[] pontosDePatrulha; // Array de pontos de patrulha
    public Transform player; 
    public float raioDeDeteccao = 5f;
    private GerenciadorEstados gerenciadorEstados;

    void Start()
    {
        gerenciadorEstados = GetComponent<GerenciadorEstados>();
        gerenciadorEstados.DefinirEstado(new EstadoPatrulha(this, pontosDePatrulha));
    }

    public void Patrulhar(Transform pontoDestino)
{
    Vector3 novaPosicao = new Vector3(pontoDestino.position.x, transform.position.y, pontoDestino.position.z);
    transform.position = Vector3.MoveTowards(transform.position, novaPosicao, Time.deltaTime * 2);
}

public void PerseguirPlayer()
{
    Vector3 novaPosicao = new Vector3(player.position.x, transform.position.y, player.position.z);
    transform.position = Vector3.MoveTowards(transform.position, novaPosicao, Time.deltaTime * 3);
}

    public bool DetectarPlayer()
    {
        return Vector3.Distance(transform.position, player.position) < raioDeDeteccao;
    }

    public void AlterarEstado(IEstado novoEstado)
    {
        gerenciadorEstados.DefinirEstado(novoEstado);
    }
}