using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaJugador : MonoBehaviour
{
    public int cantidadDeVida;

    public void TomarDaño(int daño)
    {
        cantidadDeVida -= daño;

        if (cantidadDeVida <= 0)
        {

            cantidadDeVida = 0;
        }
    }
}

