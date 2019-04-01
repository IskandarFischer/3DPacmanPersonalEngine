using Newtonsoft.Json;
using OpenTK;
using PrototypeEngine.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrototypeEngine.AI
{
    public class Node
    {
        [JsonConverter(typeof(VectorConverter))]
        public Vector3 Position;
        public bool Walkable;

        [JsonIgnore]
        public Node[] Neighbours;
    }
}
