using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Rigidbody2D _rigibody2D;
    private Vector2 _direccionMovimiento;
    private Vector2 _input;
  

    public Animator animator;
    [SerializeField]private float velocidad;

    void Awake()
    {
        _rigibody2D = GetComponent<Rigidbody2D>();
       

    }

    private void Update()
    {
        _input = new Vector2(x:Input.GetAxisRaw("Horizontal"), y:Input.GetAxisRaw("Vertical"));
        if (_input.x > 0.1f ) {
            _direccionMovimiento.x = 1f;
        }

        else if (_input.x < -0f)
        {
            _direccionMovimiento.x = -1f;
        }

        else if (_input.x < -0f)
        {
            _direccionMovimiento.x = 0;
        }

    }

    private void FixedUpdate()
    {
        _rigibody2D.MovePosition(_rigibody2D.position + _direccionMovimiento * velocidad * Time.fixedDeltaTime);
        
    }




 


}