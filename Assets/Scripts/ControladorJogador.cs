using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Script respons�vel por controlar o jogador:
/// movimenta��o, pulo, vidas, quedas e transi��o de fase.
/// Integrado com sistema de anima��o (Unity 6).
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class ControladorJogador : MonoBehaviour
{
    [Header("Movimenta��o do Jogador")]
    [SerializeField] private float velocidade = 5f;    // Velocidade de movimento horizontal
    [SerializeField] private float forcaPulo = 5f;     // For�a aplicada ao pulo

    [Header("Checagem de Ch�o")]
    [SerializeField] private Transform pontoChao;      // Ponto abaixo do jogador usado para detectar o ch�o
    [SerializeField] private float raioChao = 0.1f;    // Raio da checagem de contato com o ch�o
    [SerializeField] private LayerMask camadaChao;     // Camadas consideradas como ch�o

    [Header("Estado do Jogo")]
    [SerializeField] private float limiteInferior = -10f; // Y m�nimo antes de considerar que o jogador caiu
    [SerializeField] private int vidas = 3;                // Total de vidas do jogador

    [Header("Transi��o de Cena")]
    [SerializeField] private string proximaCena = "Fase02"; // Nome da pr�xima cena a carregar
    [SerializeField] private float tempoEsperaTransicao = 2f; // Tempo antes de mudar de cena

    // Refer�ncia ao Rigidbody2D (f�sica)
    private Rigidbody2D rb;

    // Refer�ncia ao script que controla o Animator
    private JogadorAnimador animador;

    // Posi��o inicial do jogador (usada para reiniciar ap�s queda)
    private Vector3 posicaoInicial;

    // Entrada de movimento horizontal e estado de pulo
    private float direcaoHorizontal;
    private bool puloRequisitado;

    /// <summary>
    /// Inicializa��o de vari�veis e refer�ncias no in�cio do jogo.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();                    // Obt�m o Rigidbody2D
        animador = GetComponentInChildren<JogadorAnimador>(); // Obt�m o animador no filho do jogador
        posicaoInicial = transform.position;                 // Salva a posi��o inicial
    }

    /// <summary>
    /// Atualiza��es por frame. Aqui processamos entrada e verificamos quedas.
    /// </summary>
    void Update()
    {
        ProcessarEntrada();
        VerificarQueda();
    }

    /// <summary>
    /// Atualiza��es de f�sica (movimenta��o e pulo).
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

        // Se apertar o bot�o de pulo e estiver no ch�o, requisita o pulo
        if (Input.GetButtonDown("Jump") && EstaNoChao())
            puloRequisitado = true;
    }

    /// <summary>
    /// Aplica movimenta��o e executa o pulo se requisitado.
    /// </summary>
    private void MoverJogador()
    {
        // Define velocidade horizontal, mantendo a vertical
        rb.linearVelocity = new Vector2(direcaoHorizontal * velocidade, rb.linearVelocity.y);

        // Atualiza anima��es
        animador.AtualizarMovimento(direcaoHorizontal);
        animador.SetEstaNoChao(EstaNoChao());

        // Executa o pulo se solicitado
        if (puloRequisitado)
        {
            rb.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse); // Aplica impulso para cima
            animador.AtivarPulo(); // Aciona anima��o de pulo
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
        Debug.Log($"Voc� caiu! Vidas restantes: {vidas}");

        // Se ainda h� vidas, reinicia na posi��o inicial
        if (vidas > 0)
        {
            ReiniciarNaPosicaoInicial();
        }
        else
        {
            // Se n�o h� vidas, inicia sequ�ncia de Game Over
            Debug.Log("Game Over");
            StartCoroutine(ReiniciarCena());
        }
    }

    /// <summary>
    /// Reposiciona o jogador e reseta f�sica.
    /// </summary>
    private void ReiniciarNaPosicaoInicial()
    {
        transform.position = posicaoInicial;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.SetRotation(0f);
    }

    /// <summary>
    /// Verifica se o jogador est� tocando o ch�o.
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
            Debug.Log("Parab�ns! Voc� completou a fase!");
            StartCoroutine(CarregarProximaFase());
        }
    }

    /// <summary>
    /// Aguarda e carrega a pr�xima cena.
    /// </summary>
    private IEnumerator CarregarProximaFase()
    {
        yield return new WaitForSecondsRealtime(tempoEsperaTransicao);
        SceneManager.LoadScene(proximaCena);
    }

#if UNITY_EDITOR
    /// <summary>
    /// Desenha visualmente no editor a �rea de checagem do ch�o.
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
