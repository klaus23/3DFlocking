using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Menu/SteeredCohesion")]
public class SteeredCohesionBehaviour : FilteredFlockBehaviour
{
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;



    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // if no neighbours, return no adjustment
        if (context.Count == 0 || filter.Filter(agent,context).Count == 0) //finds middle point between neighbours and tries to move there
            return Vector2.up;


        //add all points together and average them
        Vector2 CohesionMove = Vector2.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            CohesionMove += (Vector2)item.position;
        }
        CohesionMove /= filteredContext.Count;

        //create offset from agent position
        CohesionMove -= (Vector2)agent.transform.position;
        if (float.IsNaN(currentVelocity.x) || float.IsNaN(currentVelocity.y))
        {
            currentVelocity = Vector2.zero;
        }
        CohesionMove = Vector2.SmoothDamp(agent.transform.up, CohesionMove, ref currentVelocity, agentSmoothTime);
        return CohesionMove;
    }

    
}
