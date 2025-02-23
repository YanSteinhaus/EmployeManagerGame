using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Importante para usar NavMeshAgent

public class AnimationStateController : MonoBehaviour
{
    public Animator animator;
    private NavMeshAgent agent;
    private int speedHash;
    private float speed;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Obtém o NavMeshAgent
        speedHash = Animator.StringToHash("speed");
    }

    void Update()
    {
        // Obtém a velocidade atual do NavMeshAgent no plano XZ
        speed = new Vector3(agent.velocity.x, 0, agent.velocity.z).magnitude;

        // Se a velocidade for muito baixa, força para zero
        if (speed < 0.01f)
        {
            speed = 0f;
        }

        // Debug para verificar os valores da velocidade no Console
        Debug.Log("Speed: " + speed);

        animator.SetFloat(speedHash, speed);
    }
}
