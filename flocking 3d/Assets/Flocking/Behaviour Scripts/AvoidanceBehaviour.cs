using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Menu/Avoidance")]
public class AvoidanceBehaviour : FilteredFlockBehaviour
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // if no neighbours, return no adjustment
        if (context.Count == 0) 
            return Vector3.zero;


        
        Vector3 AvoidanceMove = Vector3.zero;
        int nAvoid = 0; //number of agents to avoid
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            Vector3 closestPoint = item.gameObject.GetComponent<Collider>().ClosestPoint(agent.transform.position); //use collider for 3d
            if (Vector3.SqrMagnitude(closestPoint - agent.transform.position) < flock.squareAvoidanceRadius)
            {
                nAvoid++;
                AvoidanceMove += (agent.transform.position - closestPoint); //give offset and move away from flock agent
            }
        }
        if (nAvoid > 0)
            AvoidanceMove /= nAvoid;

        return AvoidanceMove;

    }
}
