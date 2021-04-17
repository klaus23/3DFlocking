using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Flock : MonoBehaviour
{
    public FlockAgent AgentPrefab;
    List<FlockAgent> Agents = new List<FlockAgent>();  //store all agents here to iterate through them
    public FlockBehaviour Behaviour;

    [Range(0, 500)]   // controllable variable
    public int StartingCount = 10;
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
            Vector3 move = Behaviour.CalculateMove(agent, context, this); 
            move *= DriveFactor;
            if (move.sqrMagnitude > SquareMaxSpeed)
            {
                move = move.normalized * MaxSpeed;
            }
            agent.Move(move);

        }

    }

    List<Transform> GetNearbyObjects( FlockAgent agent) //list of each agent on collisionenter- add coresponding agent to agent list, on collision exit-remove
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, NeighbourRadius);//OverlapCircleAll(agent.transform.position, NeighbourRadius); //3d use normal without 2d
        
        foreach (Collider c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
                
            }
        }
        return context;
    }

}
