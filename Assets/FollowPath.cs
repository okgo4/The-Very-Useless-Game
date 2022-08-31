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
    }

    public void GoToRuin() {
        agent.SetDestination(wps[0].transform.position);
    }

    // Update is called once per frame
    void LateUpdate() {

    }
}