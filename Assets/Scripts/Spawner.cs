using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Camera cam;
    public GameObject mazeHandler;

    public Cell cellPrefub;
    public Vector2 cellSize = new(1, 1);

    public int width = 10;
    public int height = 10;


    public void GenerateMaze()
    {
        foreach (Transform child in mazeHandler.transform)
            Destroy(child.gameObject);

        var generator = new Generator();
        var maze = generator.GenerateMaze(width, height);

        GenRealMaze(maze);
    }

    public void GenerateMaze_AldousBroder()
    {
        foreach (Transform child in mazeHandler.transform)
            Destroy(child.gameObject);

        var generator = new Generator();
        var maze = generator.GenerateMaze_AldousBroder(width, height);

        GenRealMaze(maze);
    }

    private void GenRealMaze(Maze maze)
    {
        for (var x = 0; x < maze.Cells.GetLength(0); x++)
        {
            for (var z = 0; z < maze.Cells.GetLength(1); z++)
            {
                var c = Instantiate(cellPrefub, new Vector3(x * cellSize.x, 0, z * cellSize.y), Quaternion.identity);

                if (maze.Cells[x, z].Left == false)
                    Destroy(c.Left);
                if (maze.Cells[x, z].Right == false)
                    Destroy(c.Right);
                if (maze.Cells[x, z].Up == false)
                    Destroy(c.Up);
                if (maze.Cells[x, z].Bottom == false)
                    Destroy(c.Bottom);

                c.transform.parent = mazeHandler.transform;
                c.distance.text = maze.Cells[x, z].Distance.ToString();
            }
        }

        cam.transform.position = new Vector3((width * cellSize.x) / 3, Mathf.Max(width, height) * 2.5f, (height * cellSize.y) / 2.3f);
    }
}