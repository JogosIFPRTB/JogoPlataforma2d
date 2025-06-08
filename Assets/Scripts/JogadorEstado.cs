// ===============================
// JogadorEstado.cs
// ===============================
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Controla a l�gica de vida do jogador, queda da plataforma e rein�cio da fase.
/// Respons�vel por monitorar se o jogador caiu fora da fase e tomar decis�es:
/// - Reiniciar posi��o caso ainda tenha vidas.
/// - Reiniciar cena caso as vidas acabem.
/// </summary>
public class JogadorEstado : MonoBehaviour
{
    [Header("Configura��o de Vidas")]
    [Tooltip("N�mero total de vidas que o jogador come�a.")]
    public int vidas = 3;

    [Header("Detec��o de Queda")]
    [Tooltip("Altura m�nima antes de considerar que o jogador caiu da fase.")]
    public float limiteInferior = -10f;

    [Header("Tempo de Transi��o")]
    [Tooltip("Tempo (em segundos) de espera antes de reiniciar a cena.")]
    public float tempoEsperaTransicao = 2f;

    // Guarda a posi��o inicial para reiniciar o jogador ap�s cair
    private Vector3 posicaoInicial;

    // Refer�ncia ao componente Rigidbody2D usado para aplicar f�sica
    private Rigidbody2D rb;

    /// <summary>
    /// Inicializa vari�veis no in�cio do jogo.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();            // Obt�m o Rigidbody do objeto
        posicaoInicial = transform.position;         // Salva a posi��o inicial
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
                Reiniciar(); // Reinicia na posi��o inicial
            else
                StartCoroutine(ReiniciarCena()); // Reinicia a fase inteira
        }
    }

    /// <summary>
    /// Reinicia o jogador na posi��o original e zera a f�sica.
    /// </summary>
    void Reiniciar()
    {
        transform.position = posicaoInicial;              // Reposiciona no ponto inicial
        rb.linearVelocity = Vector2.zero;            // Zera a velocidade linear
        rb.angularVelocity = 0;                      // Zera a rota��o angular
        rb.SetRotation(0f);                          // Reseta a rota��o
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
