using UnityEngine;
using UnityEngine.AI;

public class NpcMovement : MonoBehaviour
{
    [SerializeField]
    private Transform HRtable;
    [SerializeField]
    private Transform StandByPosition;

    Vector3 tablePos ;
    Vector3 standByPos ;

    public NavMeshAgent agent;

    void Start()
    {
         tablePos = HRtable.transform.position;
         standByPos = StandByPosition.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ComeToRoom();
        }

        if (Input.GetMouseButtonDown(1))
        {
            LeaveRoom();
        }
    }

    public void ComeToRoom()
    {
        agent.SetDestination(tablePos);
    }

    public void LeaveRoom()
    {
        agent.SetDestination(standByPos);
    }
}
