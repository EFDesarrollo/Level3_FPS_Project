using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5;
    public float fuerzaSalto = 5;
    [Header("Camara")]
    public float sensibilidadRaton = 3;
    public float maxvistaX = 80, maxvistaY;
    public float minvistaX = -80, minvistaY;
    private float rotacionX;

    public GameObject camara;
    private Rigidbody fisica;

    private void Awake()
    {
        fisica = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        VistaCamara();
        if (Input.GetButtonDown("Jump"))
        {
            Salto();
        }
    }

    public void Movimiento()
    {
        float x = Input.GetAxis("Horizontal") * velocidad;
        float z = Input.GetAxis("Vertical") * velocidad;

        Vector3 direccion = transform.right * x +transform.forward * z;

        fisica.velocity = direccion;
    }
    void VistaCamara()
    {
        float y = Input.GetAxis("Mouse X") * sensibilidadRaton;
        rotacionX += Input.GetAxis("Mouse Y") * sensibilidadRaton;

        rotacionX = Mathf.Clamp(rotacionX, minvistaX, maxvistaX);

        camara.transform.localRotation = Quaternion.Euler(-rotacionX, 0, 0);
        transform.eulerAngles += Vector3.up * y;
    }
    void Salto()
    {
        Ray rayo = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(rayo, 1.1f))
        {
            fisica.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
    }
}
