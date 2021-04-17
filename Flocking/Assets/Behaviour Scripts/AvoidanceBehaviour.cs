using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Menu/Avoidance")]
public class AvoidanceBehaviour : FilteredFlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // if no neighbours, return no adjustment
        if (context.Count == 0) 
            return Vector2.zero;


        
        Vector2 AvoidanceMove = Vector2.zero;
        int nAvoid = 0; //number of agents to avoid
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            Vector3 closestPoint = item.gameObject.GetComponent<Collider2D>().ClosestPoint(agent.transform.position); //use collider for 3d
            if (Vector2.SqrMagnitude(closestPoint - agent.transform.position) < flock.squareAvoidanceRadius)
            {
                nAvoid++;
                AvoidanceMove += (Vector2)(agent.transform.position - closestPoint); //give offset and move away from flock agent
            }
        }
        if (nAvoid > 0)
            AvoidanceMove /= nAvoid;

        return AvoidanceMove;

    }
}
