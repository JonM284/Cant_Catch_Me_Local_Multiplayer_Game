using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Manager : MonoBehaviour
{

    public NPC_Behaviour[] npcs;
    public Transform[] Map_Bounds_Manager;
    public Transform[] Points_Of_Interest_Manager;

    private void Awake()
    {
        for (int i = 0; i < npcs.Length; i++)
        {
            npcs[i].bounds = new Transform[Map_Bounds_Manager.Length];
            npcs[i].bounds_Positions = new Vector3[Map_Bounds_Manager.Length];
            npcs[i].points_Of_Interest = new Transform[Points_Of_Interest_Manager.Length];

            for (int x = 0; x < Map_Bounds_Manager.Length; x++)
            {
                npcs[i].bounds[x] = Map_Bounds_Manager[x];
            }
            
            for (int y = 0; y < Points_Of_Interest_Manager.Length; y++)
            {
                npcs[i].points_Of_Interest[y] = Points_Of_Interest_Manager[y];
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < npcs.Length; i++)
        {
            npcs[i].Custom_Update();
        }
    }
}
