// ===============================
// JogadorMovimento.cs
// ===============================
using UnityEngine;

/// <summary>
/// Script responsável por aplicar movimento horizontal ao jogador
/// com base na entrada capturada pelo teclado ou controle.
/// </summary>
/// <remarks>
/// Este script depende dos componentes Rigidbody2D e JogadorInput.
/// A velocidade de movimento pode ser ajustada no Inspector.
/// </remarks>
[RequireComponent(typeof(Rigidbody2D))]
public class JogadorMovimento : MonoBehaviour
{
    [Header("Configuração de Movimento")]
    [Tooltip("Velocidade de deslocamento horizontal do jogador.")]
    public float velocidade = 5f;

    // Referência ao Rigidbody2D para aplicar a física de movimento
    private Rigidbody2D rb;

    // Referência ao script JogadorInput que fornece a direção do movimento
    private JogadorInput entrada;

    // Referência ao script responsável pelas animações
    private JogadorAnimador animador;

    /// <summary>
    /// Inicializa as referências internas aos componentes necessários.
    /// </summary>
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();                       // Obtém o Rigidbody do objeto
        entrada = GetComponent<JogadorInput>();                 // Obtém as entradas do jogador
        animador = GetComponentInChildren<JogadorAnimador>();   // Script de Animações
    }

    /// <summary>
    /// Aplica o movimento horizontal com base na direção de entrada do jogador.
    /// Executado em intervalos fixos para manter consistência com a física.
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
