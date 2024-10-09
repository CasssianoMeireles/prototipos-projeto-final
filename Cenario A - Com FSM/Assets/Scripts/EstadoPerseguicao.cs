using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoPerseguicao : IEstado
{
    private NPC npc;

    public EstadoPerseguicao(NPC npc)
    {
        this.npc = npc;
    }

    public void Entrar()
    {
        Debug.Log("Entrando no estado Perseguição");
    }

    public void Atualizar()
    {
        npc.PerseguirPlayer();

        if (!npc.DetectarPlayer())
        {
            npc.AlterarEstado(new EstadoPatrulha(npc, npc.pontosDePatrulha)); // Corrigido para "pontosDePatrulha"
        }
    }

    public void Sair()
    {
        Debug.Log("Saindo do estado Perseguição");
    }
}