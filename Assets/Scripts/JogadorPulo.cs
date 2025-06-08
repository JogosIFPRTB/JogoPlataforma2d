// ===============================
// JogadorPulo.cs
// ===============================
using UnityEngine;

/// <summary>
/// Controla a l�gica de pulo e a detec��o de contato com o ch�o.
/// Este script garante que o jogador s� possa pular quando estiver no ch�o
/// e tamb�m atualiza o Animator com o estado atual (no ch�o ou no ar).
/// </summary>
public class JogadorPulo : MonoBehaviour
{
    [Header("Configura��o do Pulo")]
    [Tooltip("For�a aplicada verticalmente ao jogador quando ele pula.")]
    public float forcaPulo = 5f;

    [Header("Detec��o de Ch�o")]
    [Tooltip("Transform que marca a posi��o do 'p�' do jogador.")]
    public Transform pontoChao;

    [Tooltip("Raio do c�rculo usado para detectar o ch�o.")]
    public float raioChao = 0.1f;

    [Tooltip("Layer que representa o ch�o. Deve estar configurada corretamente.")]
    public LayerMask camadaChao;

    // Refer�ncia ao Rigidbody2D para aplicar for�a de pulo
    private Rigidbody2D rb;

    // Refer�ncia ao componente que l� a entrada do jogador
    private JogadorInput entrada;

    // Refer�ncia ao script respons�vel pelas anima��es
    private JogadorAnimador animador;

    /// <summary>
    /// Inicializa as refer�ncias dos componentes necess�rios.
    /// </summary>
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();                       // F�sica
        entrada = GetComponent<JogadorInput>();                 // Entrada de usu�rio
        animador = GetComponentInChildren<JogadorAnimador>();   // Anima��es
    }

    /// <summary>
    /// Executa a l�gica de pulo e atualiza��o de anima��o a cada frame de f�sica.
    /// </summary>
    void FixedUpdate()
    {
        // Se o jogador apertou o bot�o de pulo e est� no ch�o...
        if (entrada.ConsumirPulo() && EstaNoChao())
        {
            // Aplica for�a para pular (impulso vertical)
            rb.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);

            // Informa ao Animator que o pulo foi iniciado
            animador?.AtivarPulo();
        }

        // Atualiza o estado do ch�o no Animator (true ou false)
        animador?.SetEstaNoChao(EstaNoChao());
    }

    /// <summary>
    /// Verifica se o jogador est� tocando o ch�o.
    /// Usa um c�rculo invis�vel que detecta colis�es com a camada do ch�o.
    /// </summary>
    /// <returns>True se estiver no ch�o, false se estiver no ar.</returns>
    public bool EstaNoChao()
    {
        return Physics2D.OverlapCircle(pontoChao.position, raioChao, camadaChao);
    }

#if UNITY_EDITOR
    /// <summary>
    /// Desenha visualmente no editor a �rea de checagem do ch�o.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (pontoChao != null)
        {
            Gizmos.color = Color.green;                             // Defini��o da cor
            Gizmos.DrawWireSphere(pontoChao.position, raioChao);    // Desenha o c�rculo
        }
    }
#endif

}
