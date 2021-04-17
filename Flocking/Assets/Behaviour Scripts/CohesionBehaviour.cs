using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Flock/Menu/Cohesion")]
public class CohesionBehaviour : FilteredFlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
       // if no neighbours, return no adjustment
       if (context.Count == 0) //finds middle point between neighbours and tries to move there
            return Vector2.zero;


            //add all points together and average them
            Vector2 CohesionMove = Vector2.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
           CohesionMove += (Vector2)item.position;
        }
            CohesionMove /= context.Count;

            //create offset from agent position
            CohesionMove -= (Vector2)agent.transform.position;
            return CohesionMove;
        
    }


}
