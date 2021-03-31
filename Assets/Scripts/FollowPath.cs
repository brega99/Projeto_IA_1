using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    Transform goal;
     float speed = 5.0f;  //velocidade do objeto
     float accuracy = 2f; // verificção da distacia do ponto
     float rotSpeed = 3f;// velocidade da rotação

    public GameObject wpManager;//para pegar o objeto que tenha script wpmanager 
    GameObject[] wp;// lista de wp para se orientar
    GameObject currentNode;
    int currentWP = 0; // zera wp
    Graph g;

    // Start is called before the first frame update
    void Start()
    {

        wp = wpManager.GetComponent<wpmanager>().waypoints;        //peganndo os obejto focando no waypoints wpmanager
        g = wpManager.GetComponent<wpmanager>().graph;        //peganndo os obejto graph dentro do wpmanager


        currentNode = wp[0]; // ja começa a lista de wp em 0
    }

   

    public void GotoHeliport()// gatilho que execulta uma ação, que seria se mover em um ponto denominado pela varial posta dentro do metodo que é  modificada qando se  poen dentor de um evento na unity
    {
        g.AStar(currentNode, wp[1]);
        currentWP = 0;
    }
    public void GotoRuins()// gatilho que execulta uma ação, que seria se mover em um ponto denominado pela varial posta dentro do metodo que é  modificada qando se  poen dentor de um evento na unity
    {
        g.AStar(currentNode, wp[6]);
        currentWP = 0;
    }
    public void GotoTanks()// gatilho que execulta uma ação, que seria se mover em um ponto denominado pela varial posta dentro do metodo que é  modificada qando se  poen dentor de um evento na unity
    {
        g.AStar(currentNode, wp[7]);
        currentWP = 0;
    }




    private void LateUpdate()
    {

        if (g.getPathLength() == 0 || currentWP == g.getPathLength()) // se o valor for 0 ou se for o maior valor da lista ele retorna a valor inicial 
            return;

        //O nó que estará mais próximo neste momento
        currentNode = g.getPathPoint(currentWP);

        //se estivermos mais próximo bastante do nó o tanque se moverá para o próximo
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