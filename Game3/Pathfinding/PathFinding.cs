﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class PathFinding
    {



        public static Point ConvertToTile(Point input)
        {
            Point output = new Point((int)Math.Floor((float)(input.X - RoomShower.roomOffset)/ Walls.wallSize), (int)Math.Floor((float)(input.Y - RoomShower.roomOffset)/ Walls.wallSize));
            return output;
        }

        public static List<Point> FindPath(Point start, Point end)
        {
            if (start != end)
            {
                int[,] Weights = (int[,])RoomShower.wall2DArray.Clone();

                List<Enemy> Enemys = Game1.objectHandler.SearchArray<Enemy>();
                foreach(Enemy i in Enemys)
                {
                    Point TilePos = ConvertToTile(i.bounds.Center);
                    try //because of extremely random -33m, -33m
                    {
                        Weights[TilePos.Y, TilePos.X] = 1;
                    }
                    catch (Exception)
                    {
                        break;
                        throw;
                    }
                    
                }


                Node current = null;
                List<Node> openList = new List<Node>();
                openList.Add(new Node(start, 0, null));
                List<Node> closedList = new List<Node>();
                Node FindLowest()
                {
                    float lowest = float.MaxValue;
                    Node lowestNode = null;
                    for (int i = 0; i < openList.Count; i++)
                    {
                        if (openList[i].heuristic < lowest)
                        {
                            lowestNode = openList[i];
                            lowest = openList[i].heuristic;
                        }
                    }
                    return lowestNode;
                }

                bool CheckNodeLists(Node node)
                {
                    bool inList = false;
                    for (int i = 0; i < openList.Count; i++)
                    {
                        if (openList[i].location == node.location)
                        {
                            inList = true;
                            if (node.heuristic < openList[i].heuristic && node.location != start)
                            {
                                openList[i].heuristic = node.heuristic;
                                openList[i].parent = node.parent;
                            }
                            break;
                        }
                    }

                    for (int i = 0; i < closedList.Count; i++)
                    {
                        if (closedList[i].location == node.location)
                        {
                            inList = true;
                            if (node.heuristic < closedList[i].heuristic && node.location != start)
                            {
                                closedList[i].heuristic = node.heuristic;
                                closedList[i].parent = node.parent;
                            }
                            break;
                        }
                    }
                    return inList;
                }


                float EuclideanDist(Point p1, Point p2)
                {
                    return (float)Math.Sqrt(Math.Pow(p1.X - p2.X,2) + Math.Pow(p1.Y - p2.Y, 2));
                }

                void CreateNode(Point nodeLocation, float nodeHeuristic, Node nodeParent)
                {
                    if ((nodeLocation.X >= 0) && (nodeLocation.X < 15) && (nodeLocation.Y >= 0) && (nodeLocation.Y < 9))
                    {
                        if (RoomShower.wall2DArray[nodeLocation.Y, nodeLocation.X] == 0)
                        {
                            Node newNode = new Node(nodeLocation, nodeHeuristic, nodeParent);
                            if (!CheckNodeLists(newNode))
                            {
                                openList.Add(newNode);
                            }
                        }
                    }
                }

                do
                {
                    current = FindLowest();
                    if (current == null)
                    {
                        break;
                    }
                    closedList.Add(current);
                    openList.Remove(current);
                    CreateNode(current.location + new Point(0, -1), current.heuristic + EuclideanDist(current.location,end), current);
                    CreateNode(current.location + new Point(1, 0), current.heuristic + EuclideanDist(current.location, end), current);
                    CreateNode(current.location + new Point(0, 1), current.heuristic + EuclideanDist(current.location, end), current);
                    CreateNode(current.location + new Point(-1, 0), current.heuristic + EuclideanDist(current.location, end), current);
                    //CreateNode(current.location + new Point(-1, -1), current.heuristic + EuclideanDist(current.location, end), current);
                    //CreateNode(current.location + new Point(1, -1), current.heuristic + EuclideanDist(current.location, end), current);
                    //CreateNode(current.location + new Point(1, 1), current.heuristic + EuclideanDist(current.location, end), current);
                    //CreateNode(current.location + new Point(-1, 1), current.heuristic + EuclideanDist(current.location, end), current);

                }
                while (openList.Count > 0 && current.location != end);

                if (current != null)
                {
                    if (current.location == end)
                    {
                        List<Point> pathList = new List<Point>();
                        do
                        {
                            pathList.Add(current.location);
                            current = current.parent;
                        }
                        while (current.parent != null);

                        return pathList;
                    }
                }
            }
            return null;
        }
    }
}
