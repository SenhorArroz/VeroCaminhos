using UnityEngine;

public class FallingItem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o Singleton existe antes de tentar usar (evita erros ao fechar o jogo)
        if (GameManager.instance == null) return;

        // Se o jogo já acabou, ignora colisões
        if (GameManager.instance.isGameOver) return;

        if (other.CompareTag("Player"))
        {
            // CHAMADA DO SINGLETON
            GameManager.instance.AddScore();
            Destroy(gameObject);
        }
        else if (other.CompareTag("Finish"))
        {
            // CHAMADA DO SINGLETON
            GameManager.instance.GameOver();
            Destroy(gameObject);
        }
    }
}