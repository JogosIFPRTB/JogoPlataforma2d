using UnityEngine;

/// <summary>
/// Faz a câmera seguir suavemente o jogador com possibilidade de olhar para cima ou para baixo.
/// </summary>
public class SeguirJogador : MonoBehaviour
{
    [Header("Referência do Jogador")]
    [Tooltip("Transform do jogador que será seguido.")]
    public Transform jogador;

    [Header("Configuração de Deslocamento")]
    [Tooltip("Deslocamento relativo entre a câmera e o jogador.")]
    public Vector3 deslocamento = new Vector3(0f, 2f, -10f);

    [Header("Olhar Para Cima/Baixo")]
    [Tooltip("Quanto a câmera pode subir/descer ao pressionar para cima/baixo")]
    public float deslocamentoVerticalMaximo = 2f;
    public float velocidadeOlhar = 5f;

    [Header("Suavização do Movimento")]
    [Tooltip("Quanto menor o valor, mais rápido a câmera acompanha o jogador.")]
    [Range(0.01f, 1f)]
    public float suavizacao = 0.125f;

    // Velocidade atual usada pelo SmoothDamp (necessária para o cálculo interno)
    private Vector3 velocidade = Vector3.zero;

    /// <summary>
    /// LateUpdate é chamado após todos os Updates normais.
    /// Ideal para seguir objetos que se movem com física (como o jogador).
    /// </summary>
    void LateUpdate()
    {
        // Entrada vertical (W/S, ?/?, ou joystick)
        float inputVertical = Input.GetAxis("Vertical");

        // Calcula deslocamento adicional com base na entrada do jogador
        float deslocamentoYExtra = inputVertical * deslocamentoVerticalMaximo;

        // Define posição desejada da câmera com o deslocamento modificado
        Vector3 destinoDesejado = jogador.position + deslocamento + new Vector3(0f, deslocamentoYExtra, 0f);

        // Move suavemente até a nova posição
        transform.position = Vector3.SmoothDamp(transform.position, destinoDesejado, ref velocidade, suavizacao);
    }
}
