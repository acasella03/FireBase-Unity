using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //Fuerza del salto
    public float jumpForce = 8.0f;

    // Rigidbody del jugador.
    private Rigidbody rb;

    // Contador de puntos
    private int count;

    // Movimiento a lo largo de los ejes X e Y.
    private float movementX;
    private float movementY;

    // Velocidad a la que se mueve el jugador.
    public float speed = 5.0f;

    // UI componente de texto para contar los "PickUp" recopilados
    public TextMeshProUGUI countText;

    // UI para mostrar texto de victoria
    public GameObject winTextObject;

    // Añadir una referencia al Animator
    private Animator animator;

    // Start se llama antes de la primera actualización de fotograma.
    void Start()
    {
        // Inicialización del contador de puntos
        count = 0;

        // Obtener y almacenar el componente Rigidbody adjunto al jugador.
        rb = GetComponent<Rigidbody>();

        // Obtener el componente Animator
        animator = GetComponent<Animator>();

        Debug.Log("Hola, soy un mensaje de debug en el Start");

        // Actualizar la pantalla de conteo
        SetCountText();

        // Desactivar el mensaje de victoria al inicializar el juego
        winTextObject.SetActive(false);

    }

    // Dar un salto con la tecla Fire(boton izquierdo del mouse)
    void OnFire()
    {
        Debug.Log("Hola, soy un mensaje de debug en OnFire");

        //aplico fuerza al rigidbody para saltar
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // Activar el parámetro "saltar" en el Animator
        if (animator != null)
        {
            animator.SetBool("saltar", true);
        }

    }

    // Esta función se llama cuando se detecta una entrada de movimiento.
    void OnMove(InputValue movementValue)
    {
        // Convertir el valor de entrada en un Vector2 para el movimiento.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Almacenar los componentes X e Y del movimiento.
        Debug.Log("X");
        movementX = movementVector.x;
        Debug.Log("Y");
        movementY = movementVector.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Dar un salto con la tecla espacio
        if (Input.GetButtonDown("Jump"))
        {
            OnFire();
        }
    }

    // Función para actualizar el recuento mostrado de los "PickUp" colisionados
    void SetCountText(){
        // Actualizar el texto del recuento con el recuento actual
        countText.text = "Count: " + count.ToString();

        // Comprobar si el recuento ha alcanzado la cantidad para la victoria
        if (count >= 4)
       {   // Mostrar el texto de victoria
           winTextObject.SetActive(true);
           GameIsOver();
       }
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate()
    {
        // Dar un salto con la tecla spacio
        if(Input.GetKeyDown(KeyCode.Space)){
            OnFire();
        }
        // Crear un vector de movimiento 3D utilizando las entradas X e Y.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        //Debug.Log("Estoy en el FixedUpdate");

        // Aplicar fuerza al Rigidbody para mover al jugador.
        rb.AddForce(movement * speed);

        //Limitar la velocidad del jugador para que no sea demasiado rápido.
        rb.velocity = Vector3.ClampMagnitude(rb.velocity,speed);
    }

    void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto con el que chocó el jugador tiene la etiqueta "PickUp"
        if (other.gameObject.CompareTag("PickUp"))
        {
            // Desactivar el objeto colisionado (haciéndolo desaparecer)
            other.gameObject.SetActive(false);
            // Incrementar el recuento de objetos "PickUp" recopilados
            count = count + 1;
            // Actualizar la pantalla de conteo
            SetCountText();
        }

    }

    public bool GameIsOver()
    {
        return count >= 4; // O cualquier otra condición que indique que el juego ha terminado
    }
}