using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/StayWithinRadius")]
public class StayWithinRadiusBehaviour : FlockBehaviour
{
    public Vector3 center;
    public float radius = 15f;
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //calculating how far the agent is from the centre and try to keep it close to it
        Vector3 centerOffset = center - agent.transform.position;
        float t = centerOffset.magnitude / radius; //if t = 0 centre, t=1 we are at the radius, >1 beyond radius.
        if (t < 0.9f)
        {
            return Vector3.zero;
        }

        return centerOffset * t * t;
    }

  
}
