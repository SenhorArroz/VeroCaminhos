using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para gerenciar cenas

public class SceneLoader : MonoBehaviour
{
    // Carrega uma cena pelo nome exato (string)
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Atalho para recarregar a cena atual (Reiniciar)
    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Fecha o jogo
    public void QuitGame()
    {
        Debug.Log("Saindo do Jogo..."); // Mostra no editor que funcionou
        Application.Quit();
    }
}