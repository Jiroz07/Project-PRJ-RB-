using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaJugador : MonoBehaviour
{
    public int cantidadDeVida;

    public void TomarDa�o(int da�o)
    {
        cantidadDeVida -= da�o;

        if (cantidadDeVida <= 0)
        {

            cantidadDeVida = 0;
        }
    }
}

