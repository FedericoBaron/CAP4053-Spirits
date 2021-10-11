using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float speed = 10;
    public float distance = 1;
    public string find = "Player";
    private Transform target;
    public Tilemap ground;
    public Tilemap collision;
    public bool chase = true;
    public int locInPath = -1;
    public float timer = 0f;
    
    void Start()
    {
        target = GameObject.FindWithTag(find).transform;
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    public void UpdatePath(){
        A_Star(Vector2Int.FloorToInt(transform.position), Vector2Int.FloorToInt(target.position));
        // Debug.Log("Val = " +string.Join("",
        //      new List<Vector2Int>(path)
        //      .ConvertAll(i => i.ToString())
        //      .ToArray()));
    }
    
    void Update()
    {   
        if (!chase){
            if (Vector2.Distance(transform.position, target.position) < distance)
                transform.position = Vector2.MoveTowards(transform.position, target.position, -1 * speed * Time.deltaTime);
        }
        else  {
            timer += 1 * Time.deltaTime;
            if (locInPath >= 0){
                // Debug.Log(transform.position);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(path[locInPath].x, path[locInPath].y), speed * Time.deltaTime);
                Vector2Int temp = Vector2Int.FloorToInt(transform.position);
                Debug.Log(canMove(temp) + " " + temp);
                if (temp == path[locInPath])
                    locInPath--;
            }
        }
    }

    Dictionary<Vector2Int, double> distanceChart = new Dictionary<Vector2Int, double>();                        
    Dictionary<Vector2Int, Vector2Int> pathChart = new Dictionary<Vector2Int, Vector2Int>();         
    List<Vector2Int> path = new List<Vector2Int>();

    public void getPath(Vector2Int end){
        int cnt = 1000;
        path = new List<Vector2Int>();
        Vector2Int temp = end;
        if (!pathChart.ContainsKey(temp))
            return;
        while (temp != invalid && cnt > 0){
            Vector2Int next = pathChart[temp];
            path.Add(temp);
            temp = next;
            cnt--;
        }
    }  

    Vector2Int invalid = new Vector2Int(100000000, 100000000);

    public void BFS(Vector2Int startPos, Vector2Int endPos){   
        int cnt = 1000;
        Vector2Int currentPos = startPos;
        Queue<Vector2Int> frontier = new Queue<Vector2Int>();
       
        distanceChart.Clear();
        pathChart.Clear();
       
        frontier.Enqueue(currentPos);
        distanceChart.Add(currentPos, 0);
        pathChart.Add(currentPos, invalid);                                            
       
        while (cnt > 0 && frontier.Count > 0)                                                                
        {
            currentPos = frontier.Dequeue();    
            if (currentPos == endPos)
                break;
            foreach (Vector2Int nextPos in GetNeighbors(currentPos))                            
            {
                if (canMove(nextPos) && distanceChart.ContainsKey(nextPos) == false)                                
                {
                    frontier.Enqueue(nextPos);                                              
                    distanceChart.Add(nextPos, 1 + distanceChart[currentPos]);
                    pathChart.Add(nextPos, currentPos);
                }
            }
            cnt--;
        }

         getPath(endPos);
         locInPath = path.Count - 1;
    }

    public void Dijkstra(Vector2Int startPos, Vector2Int endPos){   
        int cnt = 1000;
        int oo = 1000000000;
        Vector2Int currentPos = startPos;
        PriorityQueue<Vector2Int> frontier = new PriorityQueue<Vector2Int>();
       
        distanceChart.Clear();
        pathChart.Clear();
       
        frontier.Enqueue(0, currentPos);
        pathChart.Add(currentPos, invalid); 
        distanceChart.Add(currentPos, 0);                                           
       
        while (cnt > 0 && frontier.Count > 0)                                                                
        {
            (Vector2Int, double) tuple = frontier.Dequeue();
            currentPos = tuple.Item1;
            double value = tuple.Item2;

            if (distanceChart[currentPos] < value) continue;
            
            if (currentPos == endPos) break;
            
            foreach (Vector2Int nextPos in GetNeighbors(currentPos))                            
            {
                if (canMove(nextPos))                                
                {
                    if (!distanceChart.ContainsKey(nextPos)){
                        distanceChart.Add(nextPos, oo);
                        pathChart.Add(nextPos, invalid);
                    } 
                    if (value + 1 < distanceChart[nextPos]){
                        frontier.Enqueue(value + 1, nextPos);   
                        distanceChart[nextPos] = value + 1;
                        pathChart[nextPos] = currentPos;
                    }                                           
                }
            }

             foreach (Vector2Int nextPos in GetNeighborsDia(currentPos))                            
            {
                if (canMove(nextPos))                                
                {
                     if (!distanceChart.ContainsKey(nextPos)){
                        distanceChart.Add(nextPos, oo);
                        pathChart.Add(nextPos, invalid);
                    } 
                    if (value + 1.41 < distanceChart[nextPos]){
                        frontier.Enqueue(value + 1.41, nextPos);   
                        distanceChart[nextPos] = value + 1.41;
                        pathChart[nextPos] = currentPos;
                    }                                                
                }
            }
            cnt--;
        }

         getPath(endPos);
         locInPath = path.Count - 1;
    }

    public void A_Star(Vector2Int startPos, Vector2Int endPos){   
        int cnt = 1000;
        int oo = 1000000000;
        Vector2Int currentPos = startPos;
        PriorityQueue<Vector2Int> frontier = new PriorityQueue<Vector2Int>();
       
        distanceChart.Clear();
        pathChart.Clear();
       
        frontier.Enqueue(0, currentPos);
        pathChart.Add(currentPos, invalid); 
        distanceChart.Add(currentPos, 0);                                           
       
        while (cnt > 0 && frontier.Count > 0)                                                                
        {
            (Vector2Int, double) tuple = frontier.Dequeue();
            currentPos = tuple.Item1;
            double value = tuple.Item2;

            // if (distanceChart[currentPos] < value - heuristic(currentPos, endPos)) continue;
            
            if (currentPos == endPos) break;
            
            foreach (Vector2Int nextPos in GetNeighbors(currentPos))                            
            {
                if (canMove(nextPos))                                
                {
                    if (!distanceChart.ContainsKey(nextPos)){
                        distanceChart.Add(nextPos, oo);
                        pathChart.Add(nextPos, invalid);
                    } 
                     double nextCost = distanceChart[currentPos] + 1;
                    if (nextCost < distanceChart[nextPos]){
                        frontier.Enqueue(nextCost + heuristic(nextPos, endPos), nextPos);   
                        distanceChart[nextPos] = nextCost;
                        pathChart[nextPos] = currentPos;
                    }                                            
                }
            }

             foreach (Vector2Int nextPos in GetNeighborsDia(currentPos))                            
            {
                if (canMove(nextPos))                                
                {
                     if (!distanceChart.ContainsKey(nextPos)){
                        distanceChart.Add(nextPos, oo);
                        pathChart.Add(nextPos, invalid);
                    } 
                    double nextCost = distanceChart[currentPos] + 1.41;
                    if (nextCost < distanceChart[nextPos]){
                        frontier.Enqueue(nextCost + heuristic(nextPos, endPos), nextPos);   
                        distanceChart[nextPos] = nextCost;
                        pathChart[nextPos] = currentPos;
                    }                                                
                }
            }
            cnt--;
        }

         getPath(endPos);
         locInPath = path.Count - 1;
    }

    public int heuristic(Vector2Int node, Vector2Int goal){
        int dx = abs(node.x - goal.x);
        int dy = abs(node.y - goal.y);
        return dx + dy;
    }

    public int abs(int val){
        if (val < 0) return -val;
        return val;
    }

    public Vector2Int[] GetNeighbors(Vector2Int start){
        Vector2Int[] vals = {start + Vector2Int.up, start + Vector2Int.down, start + Vector2Int.left, start + Vector2Int.right};
        return vals;
    }

    public Vector2Int[] GetNeighborsDia(Vector2Int start){
        Vector2Int[] vals = {start + Vector2Int.up + Vector2Int.right,
             start + Vector2Int.down + Vector2Int.right, start + Vector2Int.up + Vector2Int.left, 
             start + Vector2Int.right + Vector2Int.down};
        return vals;
    }

    private bool canMove(Vector2Int direction){
        Vector3 temp = new Vector3(direction.x, direction.y, 0);
        Vector3Int gridPosition = ground.WorldToCell(temp);
        Vector3Int colPosition = collision.WorldToCell(temp);

        if (!ground.HasTile(gridPosition) || collision.HasTile(colPosition))
            return false;
        return true;
    }
}
