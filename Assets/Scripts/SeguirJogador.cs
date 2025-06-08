using UnityEngine;

/// <summary>
/// Faz a c�mera seguir suavemente o jogador com possibilidade de olhar para cima ou para baixo.
/// </summary>
public class SeguirJogador : MonoBehaviour
{
    [Header("Refer�ncia do Jogador")]
    [Tooltip("Transform do jogador que ser� seguido.")]
    public Transform jogador;

    [Header("Configura��o de Deslocamento")]
    [Tooltip("Deslocamento relativo entre a c�mera e o jogador.")]
    public Vector3 deslocamento = new Vector3(0f, 2f, -10f);

    [Header("Olhar Para Cima/Baixo")]
    [Tooltip("Quanto a c�mera pode subir/descer ao pressionar para cima/baixo")]
    public float deslocamentoVerticalMaximo = 2f;
    public float velocidadeOlhar = 5f;

    [Header("Suaviza��o do Movimento")]
    [Tooltip("Quanto menor o valor, mais r�pido a c�mera acompanha o jogador.")]
    [Range(0.01f, 1f)]
    public float suavizacao = 0.125f;

    // Velocidade atual usada pelo SmoothDamp (necess�ria para o c�lculo interno)
    private Vector3 velocidade = Vector3.zero;

    /// <summary>
    /// LateUpdate � chamado ap�s todos os Updates normais.
    /// Ideal para seguir objetos que se movem com f�sica (como o jogador).
    /// </summary>
    void LateUpdate()
    {
        // Entrada vertical (W/S, ?/?, ou joystick)
        float inputVertical = Input.GetAxis("Vertical");

        // Calcula deslocamento adicional com base na entrada do jogador
        float deslocamentoYExtra = inputVertical * deslocamentoVerticalMaximo;

        // Define posi��o desejada da c�mera com o deslocamento modificado
        Vector3 destinoDesejado = jogador.position + deslocamento + new Vector3(0f, deslocamentoYExtra, 0f);

        // Move suavemente at� a nova posi��o
        transform.position = Vector3.SmoothDamp(transform.position, destinoDesejado, ref velocidade, suavizacao);
    }
}
