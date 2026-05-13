using UnityEngine;

/// <summary>
/// Attach this script to the goal plane/trigger collider.
/// Make sure the collider has "Is Trigger" checked.
/// The car's root GameObject (or any of its children) needs a tag "Player".
/// </summary>
public class GoalTrigger : MonoBehaviour
{
    [Tooltip("Tag del GameObject del vehículo")]
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;
        if (GameManager.Instance.IsWin()) return;

        GameManager.Instance.WinGame();
    }
}