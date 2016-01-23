using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    
    //Pathfinding variables
    private Pathfinding pathfinding = new Pathfinding();
    private Grid grid;
    Node characterNode;
    Node remCharacterNode; //Remember the character node to detect changes in editor
    Node targetNode;
    Node remTargetNode;
    Node[] path;
    private int pathIndex = 0;

    //Enemy control variables
    private GameObject player;

    void Awake()
    {
        grid = GameObject.FindGameObjectWithTag("Pathfinder").GetComponent<Grid>();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pathfinding.heuristic = (Heuristics)Random.Range(0, 3);
    }
	void Update () {
        FindPath();
	}
    void FixedUpdate()
    {
        this.FollowPath(path);
    }

    ///<summary>
    /// Method FindPath returns a Node[] path for the enemy to follow when appropriate
    ///</summary>
    void FindPath()
    {
        if (player)
            targetNode = grid.FindNodeFromPosition(player.transform.position);
        if (characterNode != null && targetNode != null && (remCharacterNode != characterNode || remTargetNode != targetNode))
        {
            path = pathfinding.ReturnPath(characterNode, targetNode).ToArray();
            remCharacterNode = characterNode;
            remTargetNode = targetNode;
            if (pathIndex > path.Length / 2)
                pathIndex = 0;
        }
    }
    ///<summary>
    /// Method FollowPath causes the object to follow Node[] path
    ///</summary>
    void FollowPath(Node[] path)
    {
        characterNode = grid.FindNodeFromPosition(transform.position);
        if (path == null) return;
        if (pathIndex >= path.Length)
        {
            FindPath();
            return;
        }
        if (pathIndex < path.Length)
        {
            transform.position = Vector2.MoveTowards(transform.position, path[pathIndex].position, Time.deltaTime * Random.Range(1, 6));
        }
        if (characterNode.position == path[pathIndex].position)
        {
            pathIndex++;
        }
    }
}
