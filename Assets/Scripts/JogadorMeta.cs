// ===============================
// JogadorMeta.cs
// ===============================
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Script respons�vel por detectar quando o jogador atinge a meta do n�vel.
/// Ao entrar em contato com um objeto com a tag "Meta", o script inicia
/// uma transi��o de cena para a pr�xima fase definida.
/// </summary>
public class JogadorMeta : MonoBehaviour
{
    [Header("Configura��o da Cena")]
    [Tooltip("Nome da pr�xima cena a ser carregada ap�s completar a fase.")]
    public string proximaCena = "Fase02";

    [Tooltip("Tempo (em segundos) de espera antes de carregar a pr�xima cena.")]
    public float tempoEsperaTransicao = 2f;

    /// <summary>
    /// Detecta colis�es com triggers (colisores marcados como IsTrigger).
    /// Espera encontrar um objeto com a tag "Meta", que simboliza o fim da fase.
    /// </summary>
    /// <param name="other">Collider do objeto com o qual houve contato.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto tocado possui a tag "Meta"
        if (other.CompareTag("Meta"))
        {
            Debug.Log("Parab�ns! Voc� completou a fase!"); // Mensagem no console
            StartCoroutine(CarregarProximaFase());         // Inicia troca de cena
        }
    }

    /// <summary>
    /// Corrotina que aguarda alguns segundos antes de carregar a pr�xima fase.
    /// Isso permite tempo para exibir efeitos de transi��o, som ou mensagens.
    /// </summary>
    private IEnumerator CarregarProximaFase()
    {
        yield return new WaitForSecondsRealtime(tempoEsperaTransicao); // Espera real, mesmo se o Time.timeScale = 0
        SceneManager.LoadScene(proximaCena); // Carrega a nova cena
    }
}
