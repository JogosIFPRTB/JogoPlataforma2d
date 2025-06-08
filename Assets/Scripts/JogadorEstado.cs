// ===============================
// JogadorEstado.cs
// ===============================
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Controla a lógica de vida do jogador, queda da plataforma e reinício da fase.
/// Responsável por monitorar se o jogador caiu fora da fase e tomar decisões:
/// - Reiniciar posição caso ainda tenha vidas.
/// - Reiniciar cena caso as vidas acabem.
/// </summary>
public class JogadorEstado : MonoBehaviour
{
    [Header("Configuração de Vidas")]
    [Tooltip("Número total de vidas que o jogador começa.")]
    public int vidas = 3;

    [Header("Detecção de Queda")]
    [Tooltip("Altura mínima antes de considerar que o jogador caiu da fase.")]
    public float limiteInferior = -10f;

    [Header("Tempo de Transição")]
    [Tooltip("Tempo (em segundos) de espera antes de reiniciar a cena.")]
    public float tempoEsperaTransicao = 2f;

    // Guarda a posição inicial para reiniciar o jogador após cair
    private Vector3 posicaoInicial;

    // Referência ao componente Rigidbody2D usado para aplicar física
    private Rigidbody2D rb;

    /// <summary>
    /// Inicializa variáveis no início do jogo.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();            // Obtém o Rigidbody do objeto
        posicaoInicial = transform.position;         // Salva a posição inicial
    }

    /// <summary>
    /// Verifica a cada frame se o jogador caiu abaixo do limite permitido.
    /// </summary>
    void Update()
    {
        if (transform.position.y < limiteInferior)
        {
            vidas--; // Perde uma vida

            if (vidas > 0)
                Reiniciar(); // Reinicia na posição inicial
            else
                StartCoroutine(ReiniciarCena()); // Reinicia a fase inteira
        }
    }

    /// <summary>
    /// Reinicia o jogador na posição original e zera a física.
    /// </summary>
    void Reiniciar()
    {
        transform.position = posicaoInicial;              // Reposiciona no ponto inicial
        rb.linearVelocity = Vector2.zero;            // Zera a velocidade linear
        rb.angularVelocity = 0;                      // Zera a rotação angular
        rb.SetRotation(0f);                          // Reseta a rotação
    }

    /// <summary>
    /// Espera o tempo definido e recarrega a cena atual.
    /// </summary>
    IEnumerator ReiniciarCena()
    {
        yield return new WaitForSecondsRealtime(tempoEsperaTransicao);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarrega a cena ativa
    }
}
