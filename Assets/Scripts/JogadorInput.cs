// ===============================
// JogadorInput.cs
// ===============================
using UnityEngine;

/// <summary>
/// Respons�vel por capturar e armazenar as entradas brutas do jogador.
/// Este script l� os comandos de movimenta��o horizontal e pulo,
/// e os disponibiliza para outros scripts (como JogadorMovimento e JogadorPulo).
/// </summary>
public class JogadorInput : MonoBehaviour
{
    [Header("Estado de Entrada")]
    [Tooltip("Valor entre -1 e 1 que representa a dire��o do movimento horizontal.")]
    public float DirecaoHorizontal { get; private set; }

    [Tooltip("Verdadeiro se o bot�o de pulo foi pressionado neste frame.")]
    public bool PuloRequisitado { get; private set; }

    /// <summary>
    /// Atualiza continuamente os valores de entrada do jogador.
    /// Executado a cada frame.
    /// </summary>
    void Update()
    {
        // Captura o valor cont�nuo do eixo horizontal (teclas A/D ou setas)
        DirecaoHorizontal = Input.GetAxis("Horizontal");

        // Detecta se o bot�o de pulo foi pressionado neste frame
        if (Input.GetButtonDown("Jump"))
            PuloRequisitado = true;
    }

    /// <summary>
    /// M�todo utilizado por outros scripts para consumir o pulo.
    /// Garante que o comando de pulo s� ser� processado uma vez por execu��o.
    /// </summary>
    /// <returns>Verdadeiro se o pulo foi requisitado; falso caso contr�rio.</returns>
    public bool ConsumirPulo()
    {
        if (PuloRequisitado)
        {
            PuloRequisitado = false; // Reseta o estado ap�s ser consumido
            return true;
        }
        return false;
    }
}
