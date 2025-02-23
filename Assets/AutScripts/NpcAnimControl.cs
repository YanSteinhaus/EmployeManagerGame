using Unity.VisualScripting;
using UnityEngine;

public class NpcAnimControl : MonoBehaviour
{
    private Animator animController;
    private Rigidbody rb;

    void Start()
    {
        animController = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rb == null || animController == null) return;

        Debug.Log("Velocidade do Rigidbody: " + rb.linearVelocity);

        float velocidade = rb.linearVelocity.magnitude;

        // S� atualiza se a velocidade for realmente percept�vel
        if (velocidade > 0.01f)
        {
            animController.speed = velocidade;
        }

        if (Input.GetMouseButtonDown(0))
        {
            rb.linearVelocity = transform.forward;
        }
    }

    


}
