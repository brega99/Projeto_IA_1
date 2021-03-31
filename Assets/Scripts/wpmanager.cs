using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [System.Serializable]
    public struct Link
    {
        public enum direction { UNI, BI }
        public GameObject node1;
        public GameObject node2;
        public direction dir;

    }
    public class wpmanager : MonoBehaviour
    {
        public GameObject[] waypoints;//array de pontos para que siga os objetos 
        public Link[] links;//para poder direcionar o objeto que vaiu patrular 
        public Graph graph = new Graph();//para  formar  grafico 

        void Start()
        {
            // se tamanho waypoints for maior que 0 entra em um verificação de repetição que passa pala os link que vai ver a direção deste objeto vai ir  
            if (waypoints.Length > 0)
            {
                foreach (GameObject wp in waypoints)
                {
                    graph.AddNode(wp);
                }
                foreach (Link i in links)
                {
                    graph.AddEdge(i.node1, i.node2);
                    if (i.dir == Link.direction.BI)
                    {
                        graph.AddEdge(i.node1, i.node2);
                    }
                }

            }

        }

        // Update is called once per frame
        void Update()
        {
            graph.debugDraw();// para aparecer a linha grafica na scena
        }
    }

