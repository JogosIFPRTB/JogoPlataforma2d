// ===============================
// JogadorMovimento.cs
// ===============================
using UnityEngine;

/// <summary>
/// Script respons�vel por aplicar movimento horizontal ao jogador
/// com base na entrada capturada pelo teclado ou controle.
/// </summary>
/// <remarks>
/// Este script depende dos componentes Rigidbody2D e JogadorInput.
/// A velocidade de movimento pode ser ajustada no Inspector.
/// </remarks>
[RequireComponent(typeof(Rigidbody2D))]
public class JogadorMovimento : MonoBehaviour
{
    [Header("Configura��o de Movimento")]
    [Tooltip("Velocidade de deslocamento horizontal do jogador.")]
    public float velocidade = 5f;

    // Refer�ncia ao Rigidbody2D para aplicar a f�sica de movimento
    private Rigidbody2D rb;

    // Refer�ncia ao script JogadorInput que fornece a dire��o do movimento
    private JogadorInput entrada;

    // Refer�ncia ao script respons�vel pelas anima��es
    private JogadorAnimador animador;

    /// <summary>
    /// Inicializa as refer�ncias internas aos componentes necess�rios.
    /// </summary>
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();                       // Obt�m o Rigidbody do objeto
        entrada = GetComponent<JogadorInput>();                 // Obt�m as entradas do jogador
        animador = GetComponentInChildren<JogadorAnimador>();   // Script de Anima��es
    }

    /// <summary>
    /// Aplica o movimento horizontal com base na dire��o de entrada do jogador.
    /// Executado em intervalos fixos para manter consist�ncia com a f�sica.
    /// </summary>
    void FixedUpdate()
    {
        float direcaoHorizontal = entrada.DirecaoHorizontal;

        // Define a velocidade horizontal mantendo a vertical inalterada
        rb.linearVelocity = new Vector2(
            direcaoHorizontal * velocidade,
            rb.linearVelocity.y
        );

        // Atualiza o movimento no Animator (float)
        animador?.AtualizarMovimento(direcaoHorizontal);
    }
}
