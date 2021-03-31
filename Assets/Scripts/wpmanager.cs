using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [System.Serializable]
    public struct Link
    {
        public enum direction { UNI, BI }//Deixar publica na Unity essa informaçao, ou seja se torna modificavél
        public GameObject node1;//Deixar publica na Unity essa informaçao, ou seja se torna modificavél
        public GameObject node2;//Deixar publica na Unity essa informaçao, ou seja se torna modificavél
        public direction dir;//Deixar publica na Unity essa informaçao, ou seja se torna modificavél

}
    public class wpmanager : MonoBehaviour
    {
        public GameObject[] waypoints;//Array que permite o player seguir os pontos
        public Link[] links;//Links que determinam os pontos que o player irá seguir
        public Graph graph = new Graph();

        void Start()
        {
            // Caso o tamanho dos waypoints for > que 0 Começa uma verificação onde se ouver repetição os link irão direcionar o player até determinado ponto  
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
            graph.debugDraw();
        }
    }

