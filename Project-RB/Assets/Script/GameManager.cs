using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public RawImage background, platform;
    public float parallaxSpeed = 0.02f;
    
    void Start()
    {
        
    }

    void Update()
    {
        float finalSpeed = parallaxSpeed * Time.deltaTime;
        background.uvRect = new Rect(background.uvRect.x+finalSpeed, 0f, 1f, 1f);
        platform.uvRect = new Rect(platform.uvRect.x+finalSpeed*4, 0f, 1f, 1f);
    }
}
