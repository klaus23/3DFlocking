using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent AgentPrefab;
    List<FlockAgent> Agents = new List<FlockAgent>();  //store all agents here to iterate through them
    public FlockBehaviour Behaviour;

    [Range(10, 500)]   // controllable variable
    public int StartingCount = 250;
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]  // controllable variable
    public float DriveFactor = 10f;
    [Range(1f, 100f)]  // controllable variable
    public float MaxSpeed = 5f;

    [Range(1f, 10f)]  // controllable variable
    public float NeighbourRadius = 1.5f;

    [Range(0f, 1f)]  // controllable variable
    public float AvoidanceRadiusMultiplier = 0.5f;

    //utility variables
    float SquareMaxSpeed;
    float SquareNeighbourRadius;
    float SquareAvoidanceRadius;
    public float squareAvoidanceRadius { get { return SquareAvoidanceRadius;} }
    void OnCollisionEnter(Collision collision)
    {
        //add agent to the list
    }

    void OnColiisionExit (Collision collision)
    {

        //remove the agent from the list

    }


    void OnCollisionExit(Collision collision2)
    {


        print("No longer in contact with " + collision2.transform.name);
    }

    // Start is called before the first frame update
    void Start()
    {
        //utility maths
        SquareMaxSpeed = MaxSpeed * MaxSpeed;
        SquareNeighbourRadius = NeighbourRadius * NeighbourRadius;
        SquareAvoidanceRadius = SquareNeighbourRadius * AvoidanceRadiusMultiplier * AvoidanceRadiusMultiplier;

        for (int i = 0; i < StartingCount; i++) //setting up the flock agents
        {
            FlockAgent NewAgent = Instantiate(
                AgentPrefab,
                Random.insideUnitCircle * StartingCount * AgentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),   //rotation on z axis
                transform
                );


            NewAgent.name = "Agent" + i;
            NewAgent.Initialize(this);
            Agents.Add(NewAgent);
        }

    }

    // Update is called once per frame
    void Update()
    {
        foreach (FlockAgent agent in Agents) //making the flock move around and behave
        {
            List<Transform> context = GetNearbyObjects(agent);
            Vector2 move = Behaviour.CalculateMove(agent, context, this); 
            move *= DriveFactor;
            if (move.sqrMagnitude > SquareMaxSpeed)
            {
                move = move.normalized * MaxSpeed;
            }
            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects( FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, NeighbourRadius); //3d use normal without 2d

        foreach (Collider2D c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }

}
