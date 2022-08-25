using UnityEngine;

public class FollowPath : MonoBehaviour {

    public GameObject wpManager;
    GameObject[] wps;
    UnityEngine.AI.NavMeshAgent agent;

    // Use this for initialization
    void Start() {
        wps = wpManager.GetComponent<WPManager>().waypoints;
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void GoToHeli() {
        agent.SetDestination(wps[4].transform.position);
        // g.AStar(currentNode, wps[4]);
        // currentWP = 0;
    }

    public void GoToRuin() {
        agent.SetDestination(wps[0].transform.position);
        // g.AStar(currentNode, wps[0]);
        // currentWP = 0;
    }

    // Update is called once per frame
    void LateUpdate() {

    }
}