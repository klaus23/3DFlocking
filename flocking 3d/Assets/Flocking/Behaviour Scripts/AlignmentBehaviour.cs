using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Menu/Alignment")]
public class AlignmentBehaviour : FilteredFlockBehaviour
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // if no neighbours, maintain current alingment
        if (context.Count == 0) //finds middle point between neighbours and tries to move there
            return agent.transform.forward; //use .forward for 3d


        //add all points together and average them
        Vector3 AlignmentMove = Vector3.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            AlignmentMove += (Vector3)item.transform.forward;
        }
        AlignmentMove /= context.Count; //maybe change back to context
        
        return AlignmentMove;

    }
}
