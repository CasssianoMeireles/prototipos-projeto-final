using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoPatrulha : IEstado
{
    private NPC npc;
    private Transform[] pontosDePatrulha;
    private int proximoPonto = 0;

    public EstadoPatrulha(NPC npc, Transform[] pontosDePatrulha)
    {
        this.npc = npc;
        this.pontosDePatrulha = pontosDePatrulha;
    }

    public void Entrar()
    {
        // NPC começa a patrulhar
        Debug.Log("Entrando no estado Patrulha");
    }

    public void Atualizar()
    {
        npc.Patrulhar(pontosDePatrulha[proximoPonto]);

        if (Vector3.Distance(npc.transform.position, pontosDePatrulha[proximoPonto].position) < 1f)
        {
            proximoPonto = (proximoPonto + 1) % pontosDePatrulha.Length;
        }

        if (npc.DetectarPlayer()) // Se o player estiver próximo, mudar para perseguição
        {
            npc.AlterarEstado(new EstadoPerseguicao(npc));
        }
    }

    public void Sair()
    {
        // Lógica ao sair do estado de patrulha
        Debug.Log("Saindo do estado Patrulha");
    }
}