using UnityEngine;

public class player2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float velocidad = 5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float velocidadX = Input.GetAxis("Horizontal");
        Vector3 posicion = transform.position;
        transform.position = new Vector3(velocidadX + posicion.x, posicion.y, posicion.z);

    }
}
