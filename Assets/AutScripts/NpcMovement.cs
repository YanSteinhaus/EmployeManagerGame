using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NpcMovement : MonoBehaviour
{
    [SerializeField] private Transform HRtable;
    [SerializeField] private Transform StandByPosition;

    private NavMeshAgent agent;
    private float arrivalThreshold = 0.2f;
    private bool isLeaving = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Detecta chegada no standby e dispara próximo NPC após delay
        if (isLeaving && Vector3.Distance(transform.position, StandByPosition.position) < arrivalThreshold)
        {
            isLeaving = false;
            StartCoroutine(CallNextNpcWithDelay());
        }
    }

    public void ComeToRoom()
    {
        if (agent != null && HRtable != null)
        {
            agent.SetDestination(HRtable.position);
            isLeaving = false;
        }
    }

    public void LeaveRoom()
    {
        if (agent != null && StandByPosition != null)
        {
            agent.SetDestination(StandByPosition.position);
            isLeaving = true;
        }
    }

    private IEnumerator CallNextNpcWithDelay()
    {
        float delay = Random.Range(2f, 5f);
        yield return new WaitForSeconds(delay);

        NpcManager npcManager = FindObjectOfType<NpcManager>();
        if (npcManager != null)
        {
            npcManager.SelectRamdomEmployee();
        }
    }
}
