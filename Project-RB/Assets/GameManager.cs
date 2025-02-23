using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public RawImage background, platform;
    public Transform player; 
    public float parallaxSpeed = 0.02f;

    private float lastPlayerPosition; 

    void Start()
    {
        lastPlayerPosition = player.position.x; 
    }

    void Update()
    {
        
        float finalSpeed = parallaxSpeed * Time.deltaTime;
        background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);

        
        float playerMovement = player.position.x - lastPlayerPosition; 
       // platform.uvRect = new Rect(platform.uvRect.x + playerMovement * parallaxSpeed, 0f, 1f, 1f);


        lastPlayerPosition = player.position.x;
    }
}
