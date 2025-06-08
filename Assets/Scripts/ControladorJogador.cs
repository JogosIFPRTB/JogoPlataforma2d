using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Script responsável por controlar o jogador:
/// movimentação, pulo, vidas, quedas e transição de fase.
/// Integrado com sistema de animação (Unity 6).
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class ControladorJogador : MonoBehaviour
{
    [Header("Movimentação do Jogador")]
    [SerializeField] private float velocidade = 5f;    // Velocidade de movimento horizontal
    [SerializeField] private float forcaPulo = 5f;     // Força aplicada ao pulo

    [Header("Checagem de Chão")]
    [SerializeField] private Transform pontoChao;      // Ponto abaixo do jogador usado para detectar o chão
    [SerializeField] private float raioChao = 0.1f;    // Raio da checagem de contato com o chão
    [SerializeField] private LayerMask camadaChao;     // Camadas consideradas como chão

    [Header("Estado do Jogo")]
    [SerializeField] private float limiteInferior = -10f; // Y mínimo antes de considerar que o jogador caiu
    [SerializeField] private int vidas = 3;                // Total de vidas do jogador

    [Header("Transição de Cena")]
    [SerializeField] private string proximaCena = "Fase02"; // Nome da próxima cena a carregar
    [SerializeField] private float tempoEsperaTransicao = 2f; // Tempo antes de mudar de cena

    // Referência ao Rigidbody2D (física)
    private Rigidbody2D rb;

    // Referência ao script que controla o Animator
    private JogadorAnimador animador;

    // Posição inicial do jogador (usada para reiniciar após queda)
    private Vector3 posicaoInicial;

    // Entrada de movimento horizontal e estado de pulo
    private float direcaoHorizontal;
    private bool puloRequisitado;

    /// <summary>
    /// Inicialização de variáveis e referências no início do jogo.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();                    // Obtém o Rigidbody2D
        animador = GetComponentInChildren<JogadorAnimador>(); // Obtém o animador no filho do jogador
        posicaoInicial = transform.position;                 // Salva a posição inicial
    }

    /// <summary>
    /// Atualizações por frame. Aqui processamos entrada e verificamos quedas.
    /// </summary>
    void Update()
    {
        ProcessarEntrada();
        VerificarQueda();
    }

    /// <summary>
    /// Atualizações de física (movimentação e pulo).
    /// </summary>
    void FixedUpdate()
    {
        MoverJogador();
    }

    /// <summary>
    /// Captura entrada horizontal e pulo.
    /// </summary>
    private void ProcessarEntrada()
    {
        // Eixo horizontal (teclado ou joystick)
        direcaoHorizontal = Input.GetAxis("Horizontal");

        // Se apertar o botão de pulo e estiver no chão, requisita o pulo
        if (Input.GetButtonDown("Jump") && EstaNoChao())
            puloRequisitado = true;
    }

    /// <summary>
    /// Aplica movimentação e executa o pulo se requisitado.
    /// </summary>
    private void MoverJogador()
    {
        // Define velocidade horizontal, mantendo a vertical
        rb.linearVelocity = new Vector2(direcaoHorizontal * velocidade, rb.linearVelocity.y);

        // Atualiza animações
        animador.AtualizarMovimento(direcaoHorizontal);
        animador.SetEstaNoChao(EstaNoChao());

        // Executa o pulo se solicitado
        if (puloRequisitado)
        {
            rb.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse); // Aplica impulso para cima
            animador.AtivarPulo(); // Aciona animação de pulo
            puloRequisitado = false;
        }
    }

    /// <summary>
    /// Verifica se o jogador caiu abaixo do limite da fase.
    /// </summary>
    private void VerificarQueda()
    {
        // Se ainda estiver acima do limite, nada a fazer
        if (transform.position.y >= limiteInferior) return;

        // Reduz uma vida
        vidas--;
        Debug.Log($"Você caiu! Vidas restantes: {vidas}");

        // Se ainda há vidas, reinicia na posição inicial
        if (vidas > 0)
        {
            ReiniciarNaPosicaoInicial();
        }
        else
        {
            // Se não há vidas, inicia sequência de Game Over
            Debug.Log("Game Over");
            StartCoroutine(ReiniciarCena());
        }
    }

    /// <summary>
    /// Reposiciona o jogador e reseta física.
    /// </summary>
    private void ReiniciarNaPosicaoInicial()
    {
        transform.position = posicaoInicial;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.SetRotation(0f);
    }

    /// <summary>
    /// Verifica se o jogador está tocando o chão.
    /// </summary>
    private bool EstaNoChao()
    {
        return Physics2D.OverlapCircle(pontoChao.position, raioChao, camadaChao);
    }

    /// <summary>
    /// Aguarda e recarrega a cena atual.
    /// </summary>
    private IEnumerator ReiniciarCena()
    {
        yield return new WaitForSecondsRealtime(tempoEsperaTransicao);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Detecta se o jogador entrou na meta da fase.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.CompareTag("Meta"))
        {
            Debug.Log("Parabéns! Você completou a fase!");
            StartCoroutine(CarregarProximaFase());
        }
    }

    /// <summary>
    /// Aguarda e carrega a próxima cena.
    /// </summary>
    private IEnumerator CarregarProximaFase()
    {
        yield return new WaitForSecondsRealtime(tempoEsperaTransicao);
        SceneManager.LoadScene(proximaCena);
    }

#if UNITY_EDITOR
    /// <summary>
    /// Desenha visualmente no editor a área de checagem do chão.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (pontoChao != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(pontoChao.position, raioChao);
        }
    }
#endif
}
