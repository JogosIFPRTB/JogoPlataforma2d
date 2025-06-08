using UnityEngine;

/// <summary>
/// Respons�vel por controlar o Animator e a orienta��o visual do personagem.
/// Define anima��es de movimento, pulo e rota��o do sprite com base na dire��o.
/// </summary>
[RequireComponent(typeof(Animator))]
public class JogadorAnimador : MonoBehaviour
{
    // Refer�ncia ao Animator que controla as anima��es
    private Animator animator;

    // Refer�ncia ao SpriteRenderer para virar o sprite ao mudar de dire��o
    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// Inicializa componentes ao ativar o objeto.
    /// </summary>
    void Awake()
    {
        animator = GetComponent<Animator>();         // Obt�m o componente Animator do GameObject
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obt�m o SpriteRenderer (respons�vel pela imagem do personagem)
    }

    /// <summary>
    /// Atualiza o par�metro "Velocidade" do Animator e vira o sprite.
    /// </summary>
    /// <param name="direcao">Valor do eixo horizontal (positivo: direita, negativo: esquerda).</param>
    public void AtualizarMovimento(float direcao)
    {
        // Define a velocidade absoluta (sem sinal) para o Blend Tree
        animator.SetFloat("Velocidade", Mathf.Abs(direcao));

        // Vira o sprite horizontalmente de acordo com a dire��o
        if (direcao < 0)
            spriteRenderer.flipX = true;  // Vira para a esquerda
        else if (direcao > 0)
            spriteRenderer.flipX = false; // Mant�m virado para a direita
    }

    /// <summary>
    /// Aciona o Trigger "Pular" no Animator para iniciar a anima��o de pulo.
    /// </summary>
    public void AtivarPulo()
    {
        animator.SetTrigger("Pular");
    }

    /// <summary>
    /// Define o par�metro booleano "EstaNoChao" no Animator, 
    /// indicando se o personagem est� tocando o ch�o ou n�o.
    /// </summary>
    /// <param name="noChao">True se o jogador est� no ch�o, false se est� no ar.</param>
    public void SetEstaNoChao(bool noChao)
    {
        animator.SetBool("EstaNoChao", noChao);
    }
}
