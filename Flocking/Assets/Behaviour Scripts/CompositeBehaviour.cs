using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Menu/Composite")]
public class CompositeBehaviour : FlockBehaviour
{
    public FlockBehaviour[] behaviours;
    public float[] weights;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //handle data mismatch
        if (weights.Length != behaviours.Length)
        {
            Debug.LogError("Data mismatch in " + name, this);
            return Vector2.zero;  //makes agent stay where they are
        }

        //setup move
        Vector2 Move = Vector2.zero;

        //iterate through behaviours
        for (int i = 0; i < behaviours.Length; i++)
        {
            Vector2 partialMove = behaviours[i].CalculateMove(agent, context, flock) * weights[i]; //for loop better because needed same index

            if (partialMove != Vector2.zero)
            {
                if (partialMove.sqrMagnitude > weights[i]*weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }

                Move += partialMove;
            }
        }

        return Move;
    }
}
