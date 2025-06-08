// ===============================
// JogadorMeta.cs
// ===============================
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Script responsável por detectar quando o jogador atinge a meta do nível.
/// Ao entrar em contato com um objeto com a tag "Meta", o script inicia
/// uma transição de cena para a próxima fase definida.
/// </summary>
public class JogadorMeta : MonoBehaviour
{
    [Header("Configuração da Cena")]
    [Tooltip("Nome da próxima cena a ser carregada após completar a fase.")]
    public string proximaCena = "Fase02";

    [Tooltip("Tempo (em segundos) de espera antes de carregar a próxima cena.")]
    public float tempoEsperaTransicao = 2f;

    /// <summary>
    /// Detecta colisões com triggers (colisores marcados como IsTrigger).
    /// Espera encontrar um objeto com a tag "Meta", que simboliza o fim da fase.
    /// </summary>
    /// <param name="other">Collider do objeto com o qual houve contato.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto tocado possui a tag "Meta"
        if (other.CompareTag("Meta"))
        {
            Debug.Log("Parabéns! Você completou a fase!"); // Mensagem no console
            StartCoroutine(CarregarProximaFase());         // Inicia troca de cena
        }
    }

    /// <summary>
    /// Corrotina que aguarda alguns segundos antes de carregar a próxima fase.
    /// Isso permite tempo para exibir efeitos de transição, som ou mensagens.
    /// </summary>
    private IEnumerator CarregarProximaFase()
    {
        yield return new WaitForSecondsRealtime(tempoEsperaTransicao); // Espera real, mesmo se o Time.timeScale = 0
        SceneManager.LoadScene(proximaCena); // Carrega a nova cena
    }
}
