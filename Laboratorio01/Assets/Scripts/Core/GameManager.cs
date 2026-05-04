using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;

    public int totalEnemies = 0;
    public int defeatedEnemies = 0;
    public System.Action<int, int> OnEnemyCountChanged;
    void Awake()
    {
        Instance = this;
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
    }
    public void PlayerDied()
    {
        Debug.Log("Jugador muerto - Reiniciando nivel");

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void RegisterEnemy()
    {
        totalEnemies++;
        OnEnemyCountChanged?.Invoke(defeatedEnemies, totalEnemies);
    }
    public void EnemyDefeated()
    {
        defeatedEnemies++;

        Debug.Log("Enemigos: " + defeatedEnemies + " / " + totalEnemies);

        OnEnemyCountChanged?.Invoke(defeatedEnemies, totalEnemies);
    }
}
