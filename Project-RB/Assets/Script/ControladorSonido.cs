using UnityEngine;

public class ControladorSonido : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public static ControladorSonido Instance;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)

        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();

    }

    public void EjecutarSonido(AudioClip sonido)
    {
        audioSource.PlayOneShot(sonido);
    }

}
