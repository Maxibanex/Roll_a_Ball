using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Agregamos
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class JugadorController : MonoBehaviour
{
    //crear variable rigidbody para asociar a jugador
    private Rigidbody rb;

    //Inicializo el contador de coleccionables recogidos
    public int contador;

    public float tiempo = 60f;
    public int segundos;

    public string nombreEscena;


    //Inicializo variables para los textos
    public Text textoContador, textoGanar, TextoTiempo;



    //Declaro la variable pública velocidad para poder modificarla desde la Inspector window
    public float velocidad;

    private AudioSource SonidoRecoleccion;
    


    // Start is called before the first frame update
    void Start()
    {
        //capturar variable
        rb = GetComponent<Rigidbody>();

        //Inicio el contador a 0
        contador = 0;

        //Actualizo el texto del contador por pimera vez
        setTextoContador();

        velocidad = 10f;

        //Inicio el texto de ganar a vacío
        textoGanar.text = "";

        //Inicio el texto de tiempo en 60 segundos
        TextoTiempo.text = "Tiempo: " + tiempo.ToString() + " Segundos";

    }


    void Awake()
    {
        SonidoRecoleccion = gameObject.GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        //Estas variables nos capturan el movimiento en horizontal y
        //vertical de nuestro teclado
        float movimientoH = Input.GetAxis("Horizontal");
        float movimientoV = Input.GetAxis("Vertical");

        //Un vector 3 es un trío de posiciones en el espacio XYZ, en este
        //caso el que corresponde al movimiento
        Vector3 movimiento = new Vector3(movimientoH, 0.0f, movimientoV);

        //Asigno ese movimiento o desplazamiento a mi RigidBody
        rb.AddForce(movimiento * velocidad);



        if (tiempo > 0 || contador != 12)
        {
            tiempo -= Time.deltaTime;
            segundos = Mathf.CeilToInt(tiempo);
            TextoTiempo.text = "Tiempo: " + segundos.ToString() + " Segundos";

        } else
        {
            textoGanar.text = "¡Perdiste!";
            System.Threading.Thread.Sleep(5000);
            SceneManager.LoadScene("MenuInicial");

        }




    }


    //Se ejecuta al entrar a un objeto con la opción isTrigger seleccionada
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coleccionable"))
        {
            //Desactivo el objeto
            other.gameObject.SetActive(false);

            if (SonidoRecoleccion.enabled == true)
            {
                SonidoRecoleccion.enabled = false;
            }

            SonidoRecoleccion.enabled = true;

            //Incremento el contador en uno (también se peude hacercomo contador++)
            contador = contador + 1;

            //Actualizo elt exto del contador
            setTextoContador();

        }
    }


    //Actualizo el texto del contador (O muestro el de ganar si las ha cogido todas)
    void setTextoContador()
    {
        textoContador.text = "Contador: " + contador.ToString();
        if (contador >= 12)
        {
            textoGanar.text = "¡Ganaste!";

            nombreEscena = SceneManager.GetActiveScene().name;

            if (nombreEscena == "Nivel1")
            {
                System.Threading.Thread.Sleep(5000);
                SceneManager.LoadScene("Nivel2");
            } else
            {
                System.Threading.Thread.Sleep(5000);
                SceneManager.LoadScene("MenuInicial");
            }
            
            
        }
    }

}
