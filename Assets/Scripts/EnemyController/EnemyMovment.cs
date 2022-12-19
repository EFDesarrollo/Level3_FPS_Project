using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovment : MonoBehaviour
{
    public float speed, searchRange, attackRange, yPathOffset;
    public GameObject head;

    private List<Vector3> wayPathList;
    private FireArm fireArmController;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        fireArmController = GetComponent<FireArm>();
        playerController = FindObjectOfType<PlayerController>();

        InvokeRepeating("UpdateWayPath", 0.0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistance = Vector3.Distance(transform.position, playerController.transform.position);
        if (playerDistance > searchRange) return;
        if (playerDistance > attackRange)
        {
            GoToObjetive();
        }
        else
        {
            fireArmController.Shoot();
        }
    }

    void UpdateWayPath()
    {
        //if (wayPathList.Count == 0) return;
        NavMeshPath navPath = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, playerController.transform.position, NavMesh.AllAreas, navPath);

        wayPathList = navPath.corners.ToList();
    }
    void GoToObjetive()
    {
        transform.position = Vector3.MoveTowards(transform.position, wayPathList[0] + new Vector3(0, yPathOffset, 0), speed * Time.deltaTime);
        if (transform.position == wayPathList[0] + new Vector3(0, yPathOffset, 0))
        {
            wayPathList.RemoveAt(0);
        }
        if (head!=null)
            head.transform.LookAt(playerController.transform);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRange);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
