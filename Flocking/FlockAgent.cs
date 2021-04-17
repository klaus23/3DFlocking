using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class FlockAgent : MonoBehaviour
{
    Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock; } }

    Collider agentCollider;
    public Collider AgentCollider { get { return agentCollider; } } //access without instanciation

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<SphereCollider>();
    }

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
    }
    public void Move(Vector3 velocity)
    {
        transform.forward = velocity; //3d transform.forward
        transform.position += Time.deltaTime * (Vector3)velocity ;

    }
}
