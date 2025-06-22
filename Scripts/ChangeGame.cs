using UnityEngine;
using UnityEngine.SceneManagement; // Needed for changing scenes

public class ChangeGame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Flabby"))
        {
            Debug.Log("Entered Flappy trigger - loading Flappy scene...");
            SceneManager.LoadScene("Flabby Bird");
        }
        else if (other.CompareTag("Fishing"))
        {
            Debug.Log("Entered Fishing trigger - loading Fishing scene...");
            SceneManager.LoadScene("Fishing Game");
        }
    }
}
