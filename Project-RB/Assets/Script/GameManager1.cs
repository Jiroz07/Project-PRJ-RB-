using UnityEngine;
using UnityEngine.UI;

public enum GameState { Ready, Playing };

public class GameManager : MonoBehaviour
{
    public RawImage background, platform;
    public GameState gameState = GameState.Ready;
    public Transform player; 
    public float parallaxSpeed = 0.02f;
    public GameObject uiReady;

    private float lastPlayerPosition; 

    void Start()
    {
        lastPlayerPosition = player.position.x;
        //player.position = new Vector2(player.position.x, -3.328f);
    }

    void Update()
    {

        UpdateParallax();
        UpdateGameState();
    }

    void UpdateGameState()
    {
        if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
        {
            gameState = GameState.Playing;
            uiReady.SetActive(false);
        }
    }


    void UpdateParallax()
    {
        if (gameState == GameState.Playing)
        {
            float finalSpeed = parallaxSpeed * Time.deltaTime;
            background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);


            float playerMovement = player.position.x - lastPlayerPosition;
            // platform.uvRect = new Rect(platform.uvRect.x + playerMovement * parallaxSpeed, 0f, 1f, 1f);


            lastPlayerPosition = player.position.x;
            if (background.uvRect.x >= 1f)
            {
                background.uvRect = new Rect(0f, 0f, 1f, 1f);
            }

            if (platform.uvRect.x >= 1f)
            {
                platform.uvRect = new Rect(0f, 0f, 1f, 1f);
            }
        }
    }






}
