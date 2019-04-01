using Newtonsoft.Json;
using OpenGL_Game.Managers;
using OpenTK;
using PrototypeEngine.AI;
using PrototypeEngine.Managers;
using PrototypeEngine.Objects;
using PrototypeEngine.Utilities;
using System;
using System.Collections.Generic;
using System.IO;

using Excel = Microsoft.Office.Interop.Excel;

namespace OpenGL_Game.Utilities
{
    static class BoardReader
    {
        class BoardJson
        {
            public List<Entity> EntityInScene = new List<Entity>();

            [JsonConverter(typeof(VectorConverter))]
            public Vector3 Spawn;

            public int[] GhostSpawnExit = new int[2];
            public List<int[]> GhostSpawnPoints = new List<int[]>();

            public Node[,] AllBoardNodes;

            public int TotalItems;
        }

        public static void SetBoard(string path)
        {
            if (!File.Exists(path))
                return;

            FileStream fin = File.OpenRead(path);

            BoardJson boardJSON = null;

            using (StreamReader r = new StreamReader(fin))
            {
                JsonSerializerSettings set = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects
                };

                string json = r.ReadToEnd();
                boardJSON = JsonConvert.DeserializeObject<BoardJson>(json, set);

                BoardCalculateNeighbour(boardJSON.AllBoardNodes);

                GameManager.Instance.SetSpawnPoint(boardJSON.Spawn);
                GameManager.Instance.SetupPickupBoard(boardJSON.TotalItems);


                var exit = boardJSON.AllBoardNodes[boardJSON.GhostSpawnExit[0], boardJSON.GhostSpawnExit[1]];
                var spawns = new List<Node>();

                foreach (var a in boardJSON.GhostSpawnPoints)
                {
                    spawns.Add(boardJSON.AllBoardNodes[a[0], a[1]]);
                }

                AIManager.Instance.SetGhostSpawn(exit, spawns);

                foreach (var item in boardJSON.EntityInScene)
                {
                    item.SetPrefab();
                    EntityManager.Instance.AddEntity(item);
                }
            }
        }

        public static void ReadBoard(string path, string jsonPath)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(string.Format(@"{0}\{1}", Environment.CurrentDirectory, path));
            Excel._Worksheet xlWorksheet = xlWorkBook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            var BoardJson = new BoardJson();

            Node[,] boardNodes = new Node[50, 50];
            for (int i = 6; i < 56; i++)
            {
                for (int column = 1; column < 51; column++)
                {
                    boardNodes[i - 6, column - 1] = new Node();
                    CreateBoardNode(i - 6, column - 1, xlRange.Cells[i, column].Value2.ToString(), "Prefabs/wall.txt", "Prefabs/pickup.txt", "Prefabs/powerup.txt", boardNodes[i - 6, column - 1], BoardJson);
                }
            }

            BoardJson.AllBoardNodes = boardNodes;

            xlWorkBook.Close(0);
            xlApp.Quit();

            JsonSerializerSettings set = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            };

            string json = JsonConvert.SerializeObject(BoardJson, set);
            File.WriteAllText(jsonPath, json);
        }

        static void BoardCalculateNeighbour(Node[,] nodeBoard)
        {
            for (int row = 0; row < 50; row++)
            {
                for (int column = 0; column < 50; column++)
                {
                    SetNeighbours(nodeBoard, row, column);
                }
            }
        }

        static void SetNeighbours(Node[,] nodeBoard, int row, int column)
        {
            var currentNode = nodeBoard[row, column];
            currentNode.Neighbours = new Node[4];

            if (row - 1 >= 0)
                currentNode.Neighbours[0] = nodeBoard[row - 1, column];

            if (column - 1 >= 0)
                currentNode.Neighbours[1] = nodeBoard[row, column - 1];

            if (column + 1 < 50)
                currentNode.Neighbours[2] = nodeBoard[row, column + 1];

            if (row + 1 < 50)
                currentNode.Neighbours[3] = nodeBoard[row + 1, column];

        }

        static void CreateBoardNode(int row, int column, string nodeString, string wallName, string pickupName, string powerupName, Node currentNode, BoardJson board)
        {
            if (nodeString == "x")
            {
                Entity wall = EntityManager.Instance.SpawnPrefab(wallName);
                wall.Transform.Position = new Vector3(row, 0, column);

                board.EntityInScene.Add(wall);

                currentNode.Position = wall.Transform.Position;
                currentNode.Walkable = false;
            }

            else if (nodeString == "o")
            {
                Entity node = EntityManager.Instance.SpawnPrefab(pickupName);
                node.Transform.Position = new Vector3(row, 0.4f, column);

                board.EntityInScene.Add(node);

                currentNode.Position = new Vector3(row, 0, column);
                currentNode.Walkable = true;

                board.TotalItems++;
            }

            else if (nodeString == "g")
            {
                //Ghost Spawn
                currentNode.Position = new Vector3(row, 0, column);
                currentNode.Walkable = false;

                board.GhostSpawnExit[0] = row;
                board.GhostSpawnExit[1] = column;
            }

            else if (nodeString == "s")
            {
                //Player SPawn
                currentNode.Position = new Vector3(row, 0, column);
                currentNode.Walkable = true;

                board.Spawn = new Vector3(row, 0, column);
            }

            else if (nodeString == "p")
            {
                //Power Up Spawn
                Entity node = EntityManager.Instance.SpawnPrefab(powerupName);
                node.Transform.Position = new Vector3(row, 0.4f, column);

                board.EntityInScene.Add(node);

                currentNode.Position = new Vector3(row, 0, column);
                currentNode.Walkable = true;

                board.TotalItems++;
            }
            else if (nodeString == "k")
            {
                currentNode.Position = new Vector3(row, 0, column);
                currentNode.Walkable = false;

                var pos = new int[2];
                pos[0] = row;
                pos[1] = column;

                board.GhostSpawnPoints.Add(pos);
            }

            else
            {
                currentNode.Position = new Vector3(row, 0, column);
                currentNode.Walkable = false;
            }
        }
    }
}
