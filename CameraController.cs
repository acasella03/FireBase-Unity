using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Referencia al GameObject del jugador.
    public GameObject player;

    // La distancia entre la cámara y el jugador.
    private Vector3 offset;

    public float moveSpeed = 10f;
    public float turnSpeed = 50f;

    // Start se llama antes de la primera actualización de fotograma.
    void Start()
    {
        // Calcular el desplazamiento inicial entre la posición de la cámara y la posición del jugador.
        offset = transform.position - player.transform.position;
    }

    void Update ()
    {
        //Esto lo comentaría cuando el desplazamiento ya se cumpla con el método LateUpdate
        //if(Input.GetKey(KeyCode.W))
        //    transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        //if(Input.GetKey(KeyCode.S))
        //    transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
        
        /* Esto es para rotar en un mismo punto del jugador y no a su alrededor
        if(Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        
        if(Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        */

        //Rotar alredor del jugador
        //if (Input.GetKey(KeyCode.A))
        //    transform.RotateAround(player.transform.position, Vector3.up, -turnSpeed * Time.deltaTime);

        //if (Input.GetKey(KeyCode.D))
        //    transform.RotateAround(player.transform.position, Vector3.up, turnSpeed * Time.deltaTime);
    }


    // LateUpdate se llama una vez por frame después de que se hayan completado todas las funciones Update.
    void LateUpdate()
    {
        // Mantener el mismo desplazamiento entre la cámara y el jugador a lo largo del juego.
        transform.position = player.transform.position + offset;

    }
}
