using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PointerController : MonoBehaviour
{
    public Transform Background;
    public Transform pointA; // Reference to the starting point
    public Transform pointB; // Reference to the ending point
    public RectTransform safeZone; // Reference to the safe zone RectTransform
    public float moveSpeed = 100f; // Speed of the pointer movement
    public Vector2 shrinkAmount = new Vector2(10f, 10f); // How much to shrink safeZone on success

    public Text scoreText;

    private int Score;

    private float direction = 1f; // 1 for moving towards B, -1 for moving towards A
    private RectTransform pointerTransform;
    private Vector3 targetPosition;

    private bool isGamePaused = false;  // Start unpaused now

    private void Awake()
    {
        Application.targetFrameRate = 60;
        pointerTransform = GetComponent<RectTransform>();

       
    }

    private void Start()
    {
        Score = 0;
        scoreText.text = Score.ToString();
        targetPosition = pointB.position;
        isGamePaused = false;  // Just in case
    }

    private void Update()
    {
        if (isGamePaused)
            return; // Don't update pointer movement if paused

        // Move the pointer towards the target position
        pointerTransform.position = Vector3.MoveTowards(pointerTransform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Change direction if the pointer reaches one of the points
        if (Vector3.Distance(pointerTransform.position, pointA.position) < 0.1f)
        {
            targetPosition = pointB.position;
            direction = 1f;
        }
        else if (Vector3.Distance(pointerTransform.position, pointB.position) < 0.1f)
        {
            targetPosition = pointA.position;
            direction = -1f;
        }

        // Check for input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckSuccess();
        }
    }

    private void CheckSuccess()
    {
        // Check if the pointer is within the safe zone
        if (RectTransformUtility.RectangleContainsScreenPoint(safeZone, pointerTransform.position, null))
        {
            Debug.Log("Success!");

            // Shrink the safeZone width, keep height same
            float newWidth = safeZone.sizeDelta.x - shrinkAmount.x;
            newWidth = Mathf.Max(newWidth, 20f); // Minimum width limit
            safeZone.sizeDelta = new Vector2(newWidth, safeZone.sizeDelta.y);

            // Speed up pointer
            moveSpeed += 100f;

            Score++;
            scoreText.text = Score.ToString();

            // Check for win condition
            if (Score == 10)
            {
                SceneManager.LoadScene("SampleScene");

                // Pause the game
                isGamePaused = true;
            }
        }
    }
}
