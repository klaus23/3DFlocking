using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Menu/SteeredCohesion")]
public class SteeredCohesionBehaviour : FilteredFlockBehaviour
{
    Vector3 currentVelocity;
    public float agentSmoothTime = 0.5f;



    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // if no neighbours, return no adjustment
        if (context.Count == 0 || filter.Filter(agent,context).Count == 0) //finds middle point between neighbours and tries to move there
            return Vector3.forward;


        //add all points together and average them
        Vector3 CohesionMove = Vector3.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            CohesionMove += (Vector3)item.position;
        }
        CohesionMove /= filteredContext.Count;

        //create offset from agent position
        CohesionMove -= agent.transform.position;
        if (float.IsNaN(currentVelocity.x) || float.IsNaN(currentVelocity.y) || float.IsNaN(currentVelocity.z))
        {
            currentVelocity = Vector3.zero;
        }
        CohesionMove = Vector3.SmoothDamp(agent.transform.forward, CohesionMove, ref currentVelocity, agentSmoothTime);
        return CohesionMove;
    }

    
}
