using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    Transform goal;
     float speed = 5.0f;  //velocidade do Player, modificavel diretamente na Unity
     float accuracy = 2f; //Verificação de distãcia de cada waypoint,modificavel diretamente na Unity
     float rotSpeed = 3f;// velocidade do player durante a rota, modificavel diretamente na Unity

    public GameObject wpManager;//Aciona o Objeto que tenha o código wpmanager 
    GameObject[] wp;// lista de Waypoints 
    GameObject currentNode;
    int currentWP = 0; 
    Graph g;

    // Start is called before the first frame update
    void Start()
    {

        wp = wpManager.GetComponent<wpmanager>().waypoints;     
        g = wpManager.GetComponent<wpmanager>().graph;        


        currentNode = wp[0]; //wp inicia com o valor = 0
    }

   

    public void GotoHeliport()// gatilho que execulta uma ação, nesse caso o botão Heliporto
    {
        g.AStar(currentNode, wp[1]);
        currentWP = 0;
    }
    public void GotoRuins()// gatilho que execulta uma ação, nesse caso o botão Ruinas 
    {
        g.AStar(currentNode, wp[6]);
        currentWP = 0;
    }
    public void GotoTanks()// gatilho que execulta uma ação, nessa caso o botão Tanques 
    {
        g.AStar(currentNode, wp[7]);
        currentWP = 0;
    }




    private void LateUpdate()
    {

        if (g.getPathLength() == 0 || currentWP == g.getPathLength()) // se o valor for 0 ou se for o maior valor da lista ele retorna a valor inicial 
            return;

        //O point que está mais próximo do player 
        currentNode = g.getPathPoint(currentWP);

        //Caso o player esteja próximo o bastante do point ele irá se mover até o próximo
        if (Vector3.Distance(g.getPathPoint(currentWP).transform.position, transform.position) < accuracy)
        { currentWP++; }

        if (currentWP < g.getPathLength())
        {
            goal = g.getPathPoint(currentWP).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }
}