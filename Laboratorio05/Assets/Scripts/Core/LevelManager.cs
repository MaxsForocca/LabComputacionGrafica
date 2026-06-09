using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // Asegúrate de tener TextMeshPro en tu proyecto

public class LevelManager : MonoBehaviour
{
    // Singleton para acceder fácilmente: LevelManager.Instance.LoadLevel("Nombre");
    public static LevelManager Instance { get; private set; }

    [Header("UI de Carga")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI progressText;

    private void Awake()
    {
        // Configuración del Singleton Persistente
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // No se destruye al cambiar de escena
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método público para cargar por Nombre de escena
    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadSceneAsynchronously(sceneName));
    }

    // Método público para cargar por Índice (Build Index)
    public void LoadLevel(int sceneIndex)
    {
        // Convierte el índice a nombre para la corrutina
        string sceneName = NameFromIndex(sceneIndex);
        StartCoroutine(LoadSceneAsynchronously(sceneName));
    }

    // Carga la siguiente escena en la lista de Build Settings
    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            LoadLevel(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("¡Ya estás en el último nivel!");
        }
    }

    private IEnumerator LoadSceneAsynchronously(string sceneName)
    {
        // 1. Activar la pantalla de carga
        loadingScreen.SetActive(true);

        // 2. Iniciar la carga en segundo plano
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        
        // Evita que la escena se active inmediatamente al terminar de cargar (útil para transiciones lentas)
        operation.allowSceneActivation = false; 

        // 3. Actualizar la barra mientras carga
        while (!operation.isDone)
        {
            // operation.progress va de 0 a 0.9. Al llegar a 0.9 la carga terminó y espera la activación.
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            
            if (progressBar != null) progressBar.value = progress;
            if (progressText != null) progressText.text = $"Cargando... {(progress * 100):F0}%";

            // Si llegó al 90%, activamos la escena
            if (operation.progress >= 0.9f)
            {
                // Opcional: Agregar un pequeño delay para que la pantalla de carga no parpadee demasiado rápido
                yield return new WaitForSeconds(0.5f); 
                operation.allowSceneActivation = true;
            }

            yield return null;
        }

        // 4. Desactivar la pantalla de carga al finalizar
        // 4. Desactivar la pantalla de carga al finalizar de forma segura
        if (loadingScreen != null) 
        {
            loadingScreen.SetActive(false);
        }
    }

    // Función auxiliar para obtener el nombre desde un índice
    private string NameFromIndex(int BuildIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
        int slash = path.LastIndexOf('/');
        int dot = path.LastIndexOf('.');
        return path.Substring(slash + 1, dot - slash - 1);
    }
}