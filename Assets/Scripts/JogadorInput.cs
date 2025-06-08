// ===============================
// JogadorInput.cs
// ===============================
using UnityEngine;

/// <summary>
/// Responsável por capturar e armazenar as entradas brutas do jogador.
/// Este script lê os comandos de movimentação horizontal e pulo,
/// e os disponibiliza para outros scripts (como JogadorMovimento e JogadorPulo).
/// </summary>
public class JogadorInput : MonoBehaviour
{
    [Header("Estado de Entrada")]
    [Tooltip("Valor entre -1 e 1 que representa a direção do movimento horizontal.")]
    public float DirecaoHorizontal { get; private set; }

    [Tooltip("Verdadeiro se o botão de pulo foi pressionado neste frame.")]
    public bool PuloRequisitado { get; private set; }

    /// <summary>
    /// Atualiza continuamente os valores de entrada do jogador.
    /// Executado a cada frame.
    /// </summary>
    void Update()
    {
        // Captura o valor contínuo do eixo horizontal (teclas A/D ou setas)
        DirecaoHorizontal = Input.GetAxis("Horizontal");

        // Detecta se o botão de pulo foi pressionado neste frame
        if (Input.GetButtonDown("Jump"))
            PuloRequisitado = true;
    }

    /// <summary>
    /// Método utilizado por outros scripts para consumir o pulo.
    /// Garante que o comando de pulo só será processado uma vez por execução.
    /// </summary>
    /// <returns>Verdadeiro se o pulo foi requisitado; falso caso contrário.</returns>
    public bool ConsumirPulo()
    {
        if (PuloRequisitado)
        {
            PuloRequisitado = false; // Reseta o estado após ser consumido
            return true;
        }
        return false;
    }
}
