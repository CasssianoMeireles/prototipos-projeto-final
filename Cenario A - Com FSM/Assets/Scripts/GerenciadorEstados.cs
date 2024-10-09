using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorEstados : MonoBehaviour
{
    private IEstado estadoAtual;

    public void DefinirEstado(IEstado novoEstado)
    {
        if (estadoAtual != null)
        {
            estadoAtual.Sair();
        }

        estadoAtual = novoEstado;
        estadoAtual.Entrar();
    }

    void Update()
    {
        if (estadoAtual != null)
        {
            estadoAtual.Atualizar();
        }
    }
}
