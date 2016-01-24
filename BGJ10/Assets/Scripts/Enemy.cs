using UnityEngine;
using System.Collections;
using UnityEditor.Animations;

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
    private float attackTimer = 0;
    public Colortype type;
    public float hp = 10;

    //Colors
    public AnimatorController blue;
    public AnimatorController green;
    public AnimatorController pink;
    public AnimatorController yellow;

    void Awake()
    {
        grid = GameObject.FindGameObjectWithTag("Pathfinder").GetComponent<Grid>();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pathfinding.heuristic = (Heuristics)Random.Range(0, 3);
        SetEnemyColor(type);
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
	void Update () {
        FindPath();
        attackTimer -= Time.deltaTime;

        if (Vector2.Distance(this.transform.position, player.transform.position) <= 1.5f)
            Attack(player.GetComponent<Player>());

        if (hp <= 0)
            this.Die();
	}
    void FixedUpdate()
    {
        this.FollowPath(path);
    }

    ///<summary>
    /// Method Die causes the enemy object to remvoe itself and apply its death changes
    ///</summary>
    public void Die()
    {
        Destroy(this.gameObject);
    }

    ///<summary>
    /// Method Attack causes object to run an attack which can damage Player player
    ///</summary>
    void SetEnemyColor(Colortype type)
    {
        
        switch (type)
        {
            case Colortype.Blue: gameObject.GetComponent<Animator>().runtimeAnimatorController = blue; break;
            case Colortype.Green: gameObject.GetComponent<Animator>().runtimeAnimatorController = green; break;
            case Colortype.Pink: gameObject.GetComponent<Animator>().runtimeAnimatorController = pink; break;
            case Colortype.Yellow: gameObject.GetComponent<Animator>().runtimeAnimatorController = yellow; break;
            default: Debug.Log("Enemy is white?"); break;
        }
    }
    ///<summary>
    /// Method Attack causes object to run an attack which can damage Player player
    ///</summary>
    void Attack(Player player)
    {
        if (Vector2.Distance(this.transform.position, player.transform.position) <= 0.7f && attackTimer <= 0)
        {
            Debug.Log("zap hit");
            player.hp -= 5;
            attackTimer = 1.2f;
        }
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
