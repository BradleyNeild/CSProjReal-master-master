using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    class Node
    {
        public Point location;
        public float heuristic;
        public Node parent;

        public Node(Point nodeLocation, float nodeHeuristic, Node nodeParent)
        {
            location = nodeLocation;
            heuristic = nodeHeuristic;
            parent = nodeParent;
        }

    }
}
