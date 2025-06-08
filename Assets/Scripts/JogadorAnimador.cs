using UnityEngine;

/// <summary>
/// Responsável por controlar o Animator e a orientação visual do personagem.
/// Define animações de movimento, pulo e rotação do sprite com base na direção.
/// </summary>
[RequireComponent(typeof(Animator))]
public class JogadorAnimador : MonoBehaviour
{
    // Referência ao Animator que controla as animações
    private Animator animator;

    // Referência ao SpriteRenderer para virar o sprite ao mudar de direção
    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// Inicializa componentes ao ativar o objeto.
    /// </summary>
    void Awake()
    {
        animator = GetComponent<Animator>();         // Obtém o componente Animator do GameObject
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtém o SpriteRenderer (responsável pela imagem do personagem)
    }

    /// <summary>
    /// Atualiza o parâmetro "Velocidade" do Animator e vira o sprite.
    /// </summary>
    /// <param name="direcao">Valor do eixo horizontal (positivo: direita, negativo: esquerda).</param>
    public void AtualizarMovimento(float direcao)
    {
        // Define a velocidade absoluta (sem sinal) para o Blend Tree
        animator.SetFloat("Velocidade", Mathf.Abs(direcao));

        // Vira o sprite horizontalmente de acordo com a direção
        if (direcao < 0)
            spriteRenderer.flipX = true;  // Vira para a esquerda
        else if (direcao > 0)
            spriteRenderer.flipX = false; // Mantém virado para a direita
    }

    /// <summary>
    /// Aciona o Trigger "Pular" no Animator para iniciar a animação de pulo.
    /// </summary>
    public void AtivarPulo()
    {
        animator.SetTrigger("Pular");
    }

    /// <summary>
    /// Define o parâmetro booleano "EstaNoChao" no Animator, 
    /// indicando se o personagem está tocando o chão ou não.
    /// </summary>
    /// <param name="noChao">True se o jogador está no chão, false se está no ar.</param>
    public void SetEstaNoChao(bool noChao)
    {
        animator.SetBool("EstaNoChao", noChao);
    }
}
