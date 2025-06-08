// ===============================
// JogadorPulo.cs
// ===============================
using UnityEngine;

/// <summary>
/// Controla a lógica de pulo e a detecção de contato com o chão.
/// Este script garante que o jogador só possa pular quando estiver no chão
/// e também atualiza o Animator com o estado atual (no chão ou no ar).
/// </summary>
public class JogadorPulo : MonoBehaviour
{
    [Header("Configuração do Pulo")]
    [Tooltip("Força aplicada verticalmente ao jogador quando ele pula.")]
    public float forcaPulo = 5f;

    [Header("Detecção de Chão")]
    [Tooltip("Transform que marca a posição do 'pé' do jogador.")]
    public Transform pontoChao;

    [Tooltip("Raio do círculo usado para detectar o chão.")]
    public float raioChao = 0.1f;

    [Tooltip("Layer que representa o chão. Deve estar configurada corretamente.")]
    public LayerMask camadaChao;

    // Referência ao Rigidbody2D para aplicar força de pulo
    private Rigidbody2D rb;

    // Referência ao componente que lê a entrada do jogador
    private JogadorInput entrada;

    // Referência ao script responsável pelas animações
    private JogadorAnimador animador;

    /// <summary>
    /// Inicializa as referências dos componentes necessários.
    /// </summary>
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();                       // Física
        entrada = GetComponent<JogadorInput>();                 // Entrada de usuário
        animador = GetComponentInChildren<JogadorAnimador>();   // Animações
    }

    /// <summary>
    /// Executa a lógica de pulo e atualização de animação a cada frame de física.
    /// </summary>
    void FixedUpdate()
    {
        // Se o jogador apertou o botão de pulo e está no chão...
        if (entrada.ConsumirPulo() && EstaNoChao())
        {
            // Aplica força para pular (impulso vertical)
            rb.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);

            // Informa ao Animator que o pulo foi iniciado
            animador?.AtivarPulo();
        }

        // Atualiza o estado do chão no Animator (true ou false)
        animador?.SetEstaNoChao(EstaNoChao());
    }

    /// <summary>
    /// Verifica se o jogador está tocando o chão.
    /// Usa um círculo invisível que detecta colisões com a camada do chão.
    /// </summary>
    /// <returns>True se estiver no chão, false se estiver no ar.</returns>
    public bool EstaNoChao()
    {
        return Physics2D.OverlapCircle(pontoChao.position, raioChao, camadaChao);
    }

#if UNITY_EDITOR
    /// <summary>
    /// Desenha visualmente no editor a área de checagem do chão.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (pontoChao != null)
        {
            Gizmos.color = Color.green;                             // Definição da cor
            Gizmos.DrawWireSphere(pontoChao.position, raioChao);    // Desenha o círculo
        }
    }
#endif

}
