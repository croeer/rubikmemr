
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Diagnostics;

namespace rubikmemr
{
    [DebuggerDisplay("{Name} {color1} {color2}")]
    public class Edge
    {
        public Color color1;
        public Color color2;

        public string Name = String.Empty;

        public (Face face, int index) position1;
        public (Face face, int index) position2;
    }

    [DebuggerDisplay("{Name} {color1} {color2} {color3}")]
    public class Corner
    {
        public Color color1;
        public Color color2;
        public Color color3;

        public string Name = String.Empty;

        public (Face face, int index) position1;
        public (Face face, int index) position2;
        public (Face face, int index) position3;
    }

    public enum Color
    {
        yellow,
        blue,
        red,
        green,
        orange,
        white
    }

    public enum Face
    {
        Up,
        Left,
        Front,
        Right,
        Back,
        Down
    }

    public class Cube
    {
        public Color[,] State;
        HashSet<Edge> edges = new HashSet<Edge>();
        ILookup<Tuple<Color, Color>, Edge> edgeToLetter;
        ILookup<string, Edge> letterToEdge;

        HashSet<Corner> corners = new HashSet<Corner>();

        public void TurnByString(string s)
        {
            s = s.Replace("'", "p");
            var turns = s.Split(" ");
            foreach (var turn in turns)
            {
                if (turn.Length == 2 && turn[1].Equals('2'))
                {
                    var theTurn = turn[0].ToString();
                    typeof(Cube).GetMethod(theTurn)?.Invoke(this, null);
                    typeof(Cube).GetMethod(theTurn)?.Invoke(this, null);
                }
                else
                {
                    typeof(Cube).GetMethod(turn)?.Invoke(this, null);
                }
            }

        }

        #region "edges"

        public IEnumerable<Edge> Edges => edges.ToArray();

        public Cube(Color[,] state)
        {
            this.State = state;
            Setup();
        }

        public string EdgeToLetter(Color c1, Color c2)
        {
            return edgeToLetter[new Tuple<Color, Color>(c1, c2)].Single().Name;
        }

        public string EdgeToLetter(Edge edge)
        {
            return edgeToLetter[new Tuple<Color, Color>(edge.color1, edge.color2)].Single().Name;
        }

        public Edge InverseEdge(Edge edge)
        {
            return edgeToLetter[new Tuple<Color, Color>(edge.color2, edge.color1)].Single();
        }

        public Edge LetterToEdge(string letter)
        {
            var tempSide = letterToEdge[letter].Single();
            var color1 = State[(int)tempSide.position1.face, tempSide.position1.index];
            var color2 = State[(int)tempSide.position2.face, tempSide.position2.index];

            return edgeToLetter[new Tuple<Color, Color>(color1, color2)].Single();
        }

        #endregion

        /// <summary>
        /// Default cube in solved state
        /// </summary>
        public Cube()
        {
            this.State = new Color[,] {
                { Color.yellow, Color.yellow, Color.yellow,
                  Color.yellow, Color.yellow, Color.yellow,
                  Color.yellow, Color.yellow, Color.yellow},
                { Color.blue, Color.blue, Color.blue,
                  Color.blue, Color.blue, Color.blue,
                  Color.blue, Color.blue, Color.blue},
                { Color.red, Color.red, Color.red,
                  Color.red, Color.red, Color.red,
                  Color.red, Color.red, Color.red},
                { Color.green, Color.green, Color.green,
                  Color.green, Color.green, Color.green,
                  Color.green, Color.green, Color.green},
                { Color.orange, Color.orange, Color.orange,
                  Color.orange, Color.orange, Color.orange,
                  Color.orange, Color.orange, Color.orange},
                { Color.white, Color.white, Color.white,
                  Color.white, Color.white, Color.white,
                  Color.white, Color.white, Color.white}
            };
            Setup();
        }

        private void Setup()
        {
            SetupEdges();
            SetupCorners();

            edgeToLetter = edges.ToLookup(x => new Tuple<Color, Color>(x.color1, x.color2));
            letterToEdge = edges.ToLookup(x => x.Name);
        }

        private void SetupEdges()
        {
            edges.Add(new Edge { color1 = Color.yellow, color2 = Color.orange, Name = "A", position1 = (Face.Up, 1), position2 = (Face.Back, 1) });
            edges.Add(new Edge { color1 = Color.yellow, color2 = Color.green, Name = "B", position1 = (Face.Up, 5), position2 = (Face.Right, 1) });
            edges.Add(new Edge { color1 = Color.yellow, color2 = Color.red, Name = "C", position1 = (Face.Up, 7), position2 = (Face.Front, 1) });
            edges.Add(new Edge { color1 = Color.yellow, color2 = Color.blue, Name = "D", position1 = (Face.Up, 3), position2 = (Face.Left, 1) });

            edges.Add(new Edge { color1 = Color.blue, color2 = Color.yellow, Name = "E", position1 = (Face.Left, 1), position2 = (Face.Up, 3) });
            edges.Add(new Edge { color1 = Color.blue, color2 = Color.red, Name = "F", position1 = (Face.Left, 5), position2 = (Face.Front, 3) });
            edges.Add(new Edge { color1 = Color.blue, color2 = Color.white, Name = "G", position1 = (Face.Left, 7), position2 = (Face.Down, 3) });
            edges.Add(new Edge { color1 = Color.blue, color2 = Color.orange, Name = "H", position1 = (Face.Left, 3), position2 = (Face.Back, 5) });

            edges.Add(new Edge { color1 = Color.red, color2 = Color.yellow, Name = "I", position1 = (Face.Front, 1), position2 = (Face.Up, 7) });
            edges.Add(new Edge { color1 = Color.red, color2 = Color.green, Name = "J", position1 = (Face.Front, 5), position2 = (Face.Right, 3) });
            edges.Add(new Edge { color1 = Color.red, color2 = Color.white, Name = "K", position1 = (Face.Front, 7), position2 = (Face.Down, 1) });
            edges.Add(new Edge { color1 = Color.red, color2 = Color.blue, Name = "L", position1 = (Face.Front, 3), position2 = (Face.Left, 5) });

            edges.Add(new Edge { color1 = Color.green, color2 = Color.yellow, Name = "M", position1 = (Face.Right, 1), position2 = (Face.Up, 5) });
            edges.Add(new Edge { color1 = Color.green, color2 = Color.orange, Name = "N", position1 = (Face.Right, 5), position2 = (Face.Back, 3) });
            edges.Add(new Edge { color1 = Color.green, color2 = Color.white, Name = "O", position1 = (Face.Right, 7), position2 = (Face.Down, 5) });
            edges.Add(new Edge { color1 = Color.green, color2 = Color.red, Name = "P", position1 = (Face.Right, 3), position2 = (Face.Front, 5) });

            edges.Add(new Edge { color1 = Color.orange, color2 = Color.yellow, Name = "Q", position1 = (Face.Back, 1), position2 = (Face.Up, 1) });
            edges.Add(new Edge { color1 = Color.orange, color2 = Color.blue, Name = "R", position1 = (Face.Back, 5), position2 = (Face.Left, 3) });
            edges.Add(new Edge { color1 = Color.orange, color2 = Color.white, Name = "S", position1 = (Face.Back, 7), position2 = (Face.Down, 7) });
            edges.Add(new Edge { color1 = Color.orange, color2 = Color.green, Name = "T", position1 = (Face.Back, 3), position2 = (Face.Right, 5) });

            edges.Add(new Edge { color1 = Color.white, color2 = Color.red, Name = "U", position1 = (Face.Down, 1), position2 = (Face.Front, 7) });
            edges.Add(new Edge { color1 = Color.white, color2 = Color.green, Name = "V", position1 = (Face.Down, 5), position2 = (Face.Right, 7) });
            edges.Add(new Edge { color1 = Color.white, color2 = Color.orange, Name = "W", position1 = (Face.Down, 7), position2 = (Face.Back, 7) });
            edges.Add(new Edge { color1 = Color.white, color2 = Color.blue, Name = "X", position1 = (Face.Down, 3), position2 = (Face.Left, 7) });
        }

        private void SetupCorners()
        {
            corners.Add(new Corner { color1 = Color.yellow, color2 = Color.orange, color3 = Color.blue, Name = "A", position1 = (Face.Up, 0), position2 = (Face.Back, 2), position3 = (Face.Left, 0) });
            corners.Add(new Corner { color1 = Color.yellow, color2 = Color.green, color3 = Color.orange, Name = "B", position1 = (Face.Up, 1), position2 = (Face.Back, 0), position3 = (Face.Right, 2) });
            corners.Add(new Corner { color1 = Color.yellow, color2 = Color.green, color3 = Color.red, Name = "C", position1 = (Face.Up, 8), position2 = (Face.Right, 0), position3 = (Face.Front, 2) });
            corners.Add(new Corner { color1 = Color.yellow, color2 = Color.red, color3 = Color.blue, Name = "D", position1 = (Face.Up, 6), position2 = (Face.Front, 0), position3 = (Face.Left, 2) });
          
            corners.Add(new Corner { color1 = Color.blue, color2 = Color.orange, color3 = Color.yellow, Name = "E", position1 = (Face.Left, 0), position2 = (Face.Back, 2), position3 = (Face.Up, 0) });
            corners.Add(new Corner { color1 = Color.blue, color2 = Color.yellow, color3 = Color.red, Name = "F", position1 = (Face.Left, 2), position2 = (Face.Up, 6), position3 = (Face.Front, 0) });
            corners.Add(new Corner { color1 = Color.blue, color2 = Color.red, color3 = Color.white, Name = "G", position1 = (Face.Left, 8), position2 = (Face.Front, 6), position3 = (Face.Down, 0) });
            corners.Add(new Corner { color1 = Color.blue, color2 = Color.white, color3 = Color.orange, Name = "H", position1 = (Face.Left, 6), position2 = (Face.Down, 6), position3 = (Face.Back, 6) });
   
            corners.Add(new Corner { color1 = Color.red, color2 = Color.blue, color3 = Color.yellow, Name = "I", position1 = (Face.Front, 0), position2 = (Face.Left, 2), position3 = (Face.Up, 6) });
            corners.Add(new Corner { color1 = Color.red, color2 = Color.yellow, color3 = Color.green, Name = "J", position1 = (Face.Front, 2), position2 = (Face.Up, 8), position3 = (Face.Right, 0) });
            corners.Add(new Corner { color1 = Color.red, color2 = Color.green, color3 = Color.white, Name = "K", position1 = (Face.Front, 8), position2 = (Face.Right, 6), position3 = (Face.Down, 2) });
            corners.Add(new Corner { color1 = Color.red, color2 = Color.white, color3 = Color.blue, Name = "L", position1 = (Face.Front, 6), position2 = (Face.Down, 0), position3 = (Face.Left, 8) });
   
            corners.Add(new Corner { color1 = Color.green, color2 = Color.red, color3 = Color.yellow, Name = "M", position1 = (Face.Right, 0), position2 = (Face.Front, 2), position3 = (Face.Up, 8) });
            corners.Add(new Corner { color1 = Color.green, color2 = Color.yellow, color3 = Color.orange, Name = "N", position1 = (Face.Right, 2), position2 = (Face.Up, 2), position3 = (Face.Back, 0) });
            corners.Add(new Corner { color1 = Color.green, color2 = Color.orange, color3 = Color.white, Name = "O", position1 = (Face.Right, 8), position2 = (Face.Back, 6), position3 = (Face.Down, 8) });
            corners.Add(new Corner { color1 = Color.green, color2 = Color.white, color3 = Color.red, Name = "P", position1 = (Face.Right, 6), position2 = (Face.Down, 2), position3 = (Face.Front, 8) });
  
            corners.Add(new Corner { color1 = Color.orange, color2 = Color.green, color3 = Color.yellow, Name = "Q", position1 = (Face.Back, 0), position2 = (Face.Right, 2), position3 = (Face.Up, 2) });
            corners.Add(new Corner { color1 = Color.orange, color2 = Color.yellow, color3 = Color.blue, Name = "R", position1 = (Face.Back, 2), position2 = (Face.Up, 0), position3 = (Face.Left, 0) });
            corners.Add(new Corner { color1 = Color.orange, color2 = Color.blue, color3 = Color.white, Name = "S", position1 = (Face.Back, 8), position2 = (Face.Left, 6), position3 = (Face.Down, 6) });
            corners.Add(new Corner { color1 = Color.orange, color2 = Color.white, color3 = Color.green, Name = "T", position1 = (Face.Back, 6), position2 = (Face.Down, 8), position3 = (Face.Right, 8) });
  
            corners.Add(new Corner { color1 = Color.white, color2 = Color.orange, color3 = Color.blue, Name = "U", position1 = (Face.Down, 0), position2 = (Face.Left, 8), position3 = (Face.Front, 6) });
            corners.Add(new Corner { color1 = Color.white, color2 = Color.blue, color3 = Color.red, Name = "V", position1 = (Face.Down, 2), position2 = (Face.Front, 8), position3 = (Face.Right, 6) });
            corners.Add(new Corner { color1 = Color.white, color2 = Color.red, color3 = Color.green, Name = "W", position1 = (Face.Down, 8), position2 = (Face.Right, 8), position3 = (Face.Back, 6) });
            corners.Add(new Corner { color1 = Color.white, color2 = Color.green, color3 = Color.orange, Name = "X", position1 = (Face.Down, 6), position2 = (Face.Back, 8), position3 = (Face.Left, 6) });

        }

        #region "F moves"

        public Cube F()
        {
            var t1 = State[(int)Face.Up, 6];
            var t2 = State[(int)Face.Up, 7];
            var t3 = State[(int)Face.Up, 8];

            var rot0 = State[(int)Face.Front, 0];
            var rot1 = State[(int)Face.Front, 1];
            var rot2 = State[(int)Face.Front, 2];
            var rot3 = State[(int)Face.Front, 3];
            var rot4 = State[(int)Face.Front, 4];
            var rot5 = State[(int)Face.Front, 5];
            var rot6 = State[(int)Face.Front, 6];
            var rot7 = State[(int)Face.Front, 7];
            var rot8 = State[(int)Face.Front, 8];

            // up
            State[(int)Face.Up, 6] = State[(int)Face.Left, 8];
            State[(int)Face.Up, 7] = State[(int)Face.Left, 5];
            State[(int)Face.Up, 8] = State[(int)Face.Left, 2];

            // left
            State[(int)Face.Left, 2] = State[(int)Face.Down, 0];
            State[(int)Face.Left, 5] = State[(int)Face.Down, 1];
            State[(int)Face.Left, 8] = State[(int)Face.Down, 2];

            // down
            State[(int)Face.Down, 0] = State[(int)Face.Right, 6];
            State[(int)Face.Down, 1] = State[(int)Face.Right, 3];
            State[(int)Face.Down, 2] = State[(int)Face.Right, 0];

            // right
            State[(int)Face.Right, 0] = t1;
            State[(int)Face.Right, 3] = t2;
            State[(int)Face.Right, 6] = t3;

            // rotate front ccw
            State[(int)Face.Front, 0] = rot6;
            State[(int)Face.Front, 1] = rot3;
            State[(int)Face.Front, 2] = rot0;
            State[(int)Face.Front, 3] = rot7;
            State[(int)Face.Front, 4] = rot4;
            State[(int)Face.Front, 5] = rot1;
            State[(int)Face.Front, 6] = rot8;
            State[(int)Face.Front, 7] = rot5;
            State[(int)Face.Front, 8] = rot2;

            return this;
        }
        public Cube Fp()
        {
            var r = new Cube(this.State);

            var t1 = State[(int)Face.Up, 6];
            var t2 = State[(int)Face.Up, 7];
            var t3 = State[(int)Face.Up, 8];

            var rot0 = State[(int)Face.Front, 0];
            var rot1 = State[(int)Face.Front, 1];
            var rot2 = State[(int)Face.Front, 2];
            var rot3 = State[(int)Face.Front, 3];
            var rot4 = State[(int)Face.Front, 4];
            var rot5 = State[(int)Face.Front, 5];
            var rot6 = State[(int)Face.Front, 6];
            var rot7 = State[(int)Face.Front, 7];
            var rot8 = State[(int)Face.Front, 8];

            // up
            State[(int)Face.Up, 6] = State[(int)Face.Right, 0];
            State[(int)Face.Up, 7] = State[(int)Face.Right, 3];
            State[(int)Face.Up, 8] = State[(int)Face.Right, 6];

            // right
            State[(int)Face.Right, 0] = State[(int)Face.Down, 2];
            State[(int)Face.Right, 3] = State[(int)Face.Down, 1];
            State[(int)Face.Right, 6] = State[(int)Face.Down, 0];

            // down
            State[(int)Face.Down, 2] = State[(int)Face.Left, 8];
            State[(int)Face.Down, 1] = State[(int)Face.Left, 5];
            State[(int)Face.Down, 0] = State[(int)Face.Left, 2];

            // right
            State[(int)Face.Left, 2] = t1;
            State[(int)Face.Left, 5] = t2;
            State[(int)Face.Left, 8] = t3;

            // rotate front cw
            State[(int)Face.Front, 0] = rot2;
            State[(int)Face.Front, 1] = rot5;
            State[(int)Face.Front, 2] = rot8;
            State[(int)Face.Front, 3] = rot1;
            State[(int)Face.Front, 4] = rot4;
            State[(int)Face.Front, 5] = rot7;
            State[(int)Face.Front, 6] = rot0;
            State[(int)Face.Front, 7] = rot3;
            State[(int)Face.Front, 8] = rot6;

            return r;
        }

        #endregion

        #region "U moves"

        public Cube U()
        {
            var t1 = State[(int)Face.Front, 0];
            var t2 = State[(int)Face.Front, 1];
            var t3 = State[(int)Face.Front, 2];

            var rot0 = State[(int)Face.Up, 0];
            var rot1 = State[(int)Face.Up, 1];
            var rot2 = State[(int)Face.Up, 2];
            var rot3 = State[(int)Face.Up, 3];
            var rot4 = State[(int)Face.Up, 4];
            var rot5 = State[(int)Face.Up, 5];
            var rot6 = State[(int)Face.Up, 6];
            var rot7 = State[(int)Face.Up, 7];
            var rot8 = State[(int)Face.Up, 8];

            // front
            State[(int)Face.Front, 0] = State[(int)Face.Right, 0];
            State[(int)Face.Front, 1] = State[(int)Face.Right, 1];
            State[(int)Face.Front, 2] = State[(int)Face.Right, 2];

            // right
            State[(int)Face.Right, 0] = State[(int)Face.Back, 0];
            State[(int)Face.Right, 1] = State[(int)Face.Back, 1];
            State[(int)Face.Right, 2] = State[(int)Face.Back, 2];

            // Back
            State[(int)Face.Back, 0] = State[(int)Face.Left, 0];
            State[(int)Face.Back, 1] = State[(int)Face.Left, 1];
            State[(int)Face.Back, 2] = State[(int)Face.Left, 2];

            // Left
            State[(int)Face.Left, 0] = t1;
            State[(int)Face.Left, 1] = t2;
            State[(int)Face.Left, 2] = t3;

            // rotate up ccw
            State[(int)Face.Up, 0] = rot6;
            State[(int)Face.Up, 1] = rot3;
            State[(int)Face.Up, 2] = rot0;
            State[(int)Face.Up, 3] = rot7;
            State[(int)Face.Up, 4] = rot4;
            State[(int)Face.Up, 5] = rot1;
            State[(int)Face.Up, 6] = rot8;
            State[(int)Face.Up, 7] = rot5;
            State[(int)Face.Up, 8] = rot2;

            return this;
        }
        public Cube Up()
        {
            var t1 = State[(int)Face.Front, 0];
            var t2 = State[(int)Face.Front, 1];
            var t3 = State[(int)Face.Front, 2];

            var rot0 = State[(int)Face.Up, 0];
            var rot1 = State[(int)Face.Up, 1];
            var rot2 = State[(int)Face.Up, 2];
            var rot3 = State[(int)Face.Up, 3];
            var rot4 = State[(int)Face.Up, 4];
            var rot5 = State[(int)Face.Up, 5];
            var rot6 = State[(int)Face.Up, 6];
            var rot7 = State[(int)Face.Up, 7];
            var rot8 = State[(int)Face.Up, 8];

            // front
            State[(int)Face.Front, 0] = State[(int)Face.Left, 0];
            State[(int)Face.Front, 1] = State[(int)Face.Left, 1];
            State[(int)Face.Front, 2] = State[(int)Face.Left, 2];

            // left
            State[(int)Face.Left, 0] = State[(int)Face.Back, 0];
            State[(int)Face.Left, 1] = State[(int)Face.Back, 1];
            State[(int)Face.Left, 2] = State[(int)Face.Back, 2];

            // Back
            State[(int)Face.Back, 0] = State[(int)Face.Right, 0];
            State[(int)Face.Back, 1] = State[(int)Face.Right, 1];
            State[(int)Face.Back, 2] = State[(int)Face.Right, 2];

            // Right
            State[(int)Face.Right, 0] = t1;
            State[(int)Face.Right, 1] = t2;
            State[(int)Face.Right, 2] = t3;

            // rotate up cw
            State[(int)Face.Up, 0] = rot2;
            State[(int)Face.Up, 1] = rot5;
            State[(int)Face.Up, 2] = rot8;
            State[(int)Face.Up, 3] = rot1;
            State[(int)Face.Up, 4] = rot4;
            State[(int)Face.Up, 5] = rot7;
            State[(int)Face.Up, 6] = rot0;
            State[(int)Face.Up, 7] = rot3;
            State[(int)Face.Up, 8] = rot6;

            return this;
        }

        #endregion

        #region "R moves"

        public Cube R()
        {
            var t1 = State[(int)Face.Up, 2];
            var t2 = State[(int)Face.Up, 5];
            var t3 = State[(int)Face.Up, 8];

            var rot0 = State[(int)Face.Right, 0];
            var rot1 = State[(int)Face.Right, 1];
            var rot2 = State[(int)Face.Right, 2];
            var rot3 = State[(int)Face.Right, 3];
            var rot4 = State[(int)Face.Right, 4];
            var rot5 = State[(int)Face.Right, 5];
            var rot6 = State[(int)Face.Right, 6];
            var rot7 = State[(int)Face.Right, 7];
            var rot8 = State[(int)Face.Right, 8];

            // up
            State[(int)Face.Up, 2] = State[(int)Face.Front, 2];
            State[(int)Face.Up, 5] = State[(int)Face.Front, 5];
            State[(int)Face.Up, 8] = State[(int)Face.Front, 8];

            // front
            State[(int)Face.Front, 2] = State[(int)Face.Down, 2];
            State[(int)Face.Front, 5] = State[(int)Face.Down, 5];
            State[(int)Face.Front, 8] = State[(int)Face.Down, 8];

            // down
            State[(int)Face.Down, 2] = State[(int)Face.Back, 6];
            State[(int)Face.Down, 5] = State[(int)Face.Back, 3];
            State[(int)Face.Down, 8] = State[(int)Face.Back, 0];

            // back
            State[(int)Face.Back, 6] = t1;
            State[(int)Face.Back, 3] = t2;
            State[(int)Face.Back, 0] = t3;

            // rotate right ccw
            State[(int)Face.Right, 0] = rot6;
            State[(int)Face.Right, 1] = rot3;
            State[(int)Face.Right, 2] = rot0;
            State[(int)Face.Right, 3] = rot7;
            State[(int)Face.Right, 4] = rot4;
            State[(int)Face.Right, 5] = rot1;
            State[(int)Face.Right, 6] = rot8;
            State[(int)Face.Right, 7] = rot5;
            State[(int)Face.Right, 8] = rot2;

            return this;
        }
        public Cube Rp()
        {
            var t1 = State[(int)Face.Up, 2];
            var t2 = State[(int)Face.Up, 5];
            var t3 = State[(int)Face.Up, 8];

            var rot0 = State[(int)Face.Right, 0];
            var rot1 = State[(int)Face.Right, 1];
            var rot2 = State[(int)Face.Right, 2];
            var rot3 = State[(int)Face.Right, 3];
            var rot4 = State[(int)Face.Right, 4];
            var rot5 = State[(int)Face.Right, 5];
            var rot6 = State[(int)Face.Right, 6];
            var rot7 = State[(int)Face.Right, 7];
            var rot8 = State[(int)Face.Right, 8];

            // up
            State[(int)Face.Up, 2] = State[(int)Face.Back, 6];
            State[(int)Face.Up, 5] = State[(int)Face.Back, 3];
            State[(int)Face.Up, 8] = State[(int)Face.Back, 0];

            // back
            State[(int)Face.Back, 0] = State[(int)Face.Down, 8];
            State[(int)Face.Back, 3] = State[(int)Face.Down, 5];
            State[(int)Face.Back, 6] = State[(int)Face.Down, 2];

            // down
            State[(int)Face.Down, 2] = State[(int)Face.Front, 2];
            State[(int)Face.Down, 5] = State[(int)Face.Front, 5];
            State[(int)Face.Down, 8] = State[(int)Face.Front, 8];

            // front
            State[(int)Face.Front, 2] = t1;
            State[(int)Face.Front, 5] = t2;
            State[(int)Face.Front, 8] = t3;

            // rotate right cw
            State[(int)Face.Right, 0] = rot2;
            State[(int)Face.Right, 1] = rot5;
            State[(int)Face.Right, 2] = rot8;
            State[(int)Face.Right, 3] = rot1;
            State[(int)Face.Right, 4] = rot4;
            State[(int)Face.Right, 5] = rot7;
            State[(int)Face.Right, 6] = rot0;
            State[(int)Face.Right, 7] = rot3;
            State[(int)Face.Right, 8] = rot6;

            return this;
        }

        #endregion

        #region "B moves"

        public Cube B()
        {
            var t1 = State[(int)Face.Right, 2];
            var t2 = State[(int)Face.Right, 5];
            var t3 = State[(int)Face.Right, 8];

            var rot0 = State[(int)Face.Back, 0];
            var rot1 = State[(int)Face.Back, 1];
            var rot2 = State[(int)Face.Back, 2];
            var rot3 = State[(int)Face.Back, 3];
            var rot4 = State[(int)Face.Back, 4];
            var rot5 = State[(int)Face.Back, 5];
            var rot6 = State[(int)Face.Back, 6];
            var rot7 = State[(int)Face.Back, 7];
            var rot8 = State[(int)Face.Back, 8];

            // right
            State[(int)Face.Right, 2] = State[(int)Face.Down, 8];
            State[(int)Face.Right, 5] = State[(int)Face.Down, 7];
            State[(int)Face.Right, 8] = State[(int)Face.Down, 6];

            // down
            State[(int)Face.Down, 6] = State[(int)Face.Left, 0];
            State[(int)Face.Down, 7] = State[(int)Face.Left, 3];
            State[(int)Face.Down, 8] = State[(int)Face.Left, 6];

            // left
            State[(int)Face.Left, 0] = State[(int)Face.Up, 2];
            State[(int)Face.Left, 3] = State[(int)Face.Up, 1];
            State[(int)Face.Left, 6] = State[(int)Face.Up, 0];

            // up
            State[(int)Face.Up, 0] = t1;
            State[(int)Face.Up, 1] = t2;
            State[(int)Face.Up, 2] = t3;

            // rotate back ccw
            State[(int)Face.Back, 0] = rot6;
            State[(int)Face.Back, 1] = rot3;
            State[(int)Face.Back, 2] = rot0;
            State[(int)Face.Back, 3] = rot7;
            State[(int)Face.Back, 4] = rot4;
            State[(int)Face.Back, 5] = rot1;
            State[(int)Face.Back, 6] = rot8;
            State[(int)Face.Back, 7] = rot5;
            State[(int)Face.Back, 8] = rot2;

            return this;
        }
        public Cube Bp()
        {
            var t1 = State[(int)Face.Left, 0];
            var t2 = State[(int)Face.Left, 3];
            var t3 = State[(int)Face.Left, 6];

            var rot0 = State[(int)Face.Back, 0];
            var rot1 = State[(int)Face.Back, 1];
            var rot2 = State[(int)Face.Back, 2];
            var rot3 = State[(int)Face.Back, 3];
            var rot4 = State[(int)Face.Back, 4];
            var rot5 = State[(int)Face.Back, 5];
            var rot6 = State[(int)Face.Back, 6];
            var rot7 = State[(int)Face.Back, 7];
            var rot8 = State[(int)Face.Back, 8];

            // left
            State[(int)Face.Left, 0] = State[(int)Face.Down, 6];
            State[(int)Face.Left, 3] = State[(int)Face.Down, 7];
            State[(int)Face.Left, 6] = State[(int)Face.Down, 8];

            // down
            State[(int)Face.Down, 6] = State[(int)Face.Right, 8];
            State[(int)Face.Down, 7] = State[(int)Face.Right, 5];
            State[(int)Face.Down, 8] = State[(int)Face.Right, 2];

            // right
            State[(int)Face.Right, 2] = State[(int)Face.Up, 0];
            State[(int)Face.Right, 5] = State[(int)Face.Up, 1];
            State[(int)Face.Right, 8] = State[(int)Face.Up, 2];

            // up
            State[(int)Face.Up, 2] = t1;
            State[(int)Face.Up, 1] = t2;
            State[(int)Face.Up, 0] = t3;

            // rotate back cw
            State[(int)Face.Back, 0] = rot2;
            State[(int)Face.Back, 1] = rot5;
            State[(int)Face.Back, 2] = rot8;
            State[(int)Face.Back, 3] = rot1;
            State[(int)Face.Back, 4] = rot4;
            State[(int)Face.Back, 5] = rot7;
            State[(int)Face.Back, 6] = rot0;
            State[(int)Face.Back, 7] = rot3;
            State[(int)Face.Back, 8] = rot6;

            return this;
        }

        #endregion

        #region "D moves"

        public Cube D()
        {
            var t1 = State[(int)Face.Front, 6];
            var t2 = State[(int)Face.Front, 7];
            var t3 = State[(int)Face.Front, 8];

            var rot0 = State[(int)Face.Down, 0];
            var rot1 = State[(int)Face.Down, 1];
            var rot2 = State[(int)Face.Down, 2];
            var rot3 = State[(int)Face.Down, 3];
            var rot4 = State[(int)Face.Down, 4];
            var rot5 = State[(int)Face.Down, 5];
            var rot6 = State[(int)Face.Down, 6];
            var rot7 = State[(int)Face.Down, 7];
            var rot8 = State[(int)Face.Down, 8];

            // front
            State[(int)Face.Front, 6] = State[(int)Face.Left, 6];
            State[(int)Face.Front, 7] = State[(int)Face.Left, 7];
            State[(int)Face.Front, 8] = State[(int)Face.Left, 8];

            // left
            State[(int)Face.Left, 6] = State[(int)Face.Back, 6];
            State[(int)Face.Left, 7] = State[(int)Face.Back, 7];
            State[(int)Face.Left, 8] = State[(int)Face.Back, 8];

            // back
            State[(int)Face.Back, 6] = State[(int)Face.Right, 6];
            State[(int)Face.Back, 7] = State[(int)Face.Right, 7];
            State[(int)Face.Back, 8] = State[(int)Face.Right, 8];

            // right
            State[(int)Face.Right, 6] = t1;
            State[(int)Face.Right, 7] = t2;
            State[(int)Face.Right, 8] = t3;

            // rotate down ccw
            State[(int)Face.Down, 0] = rot6;
            State[(int)Face.Down, 1] = rot3;
            State[(int)Face.Down, 2] = rot0;
            State[(int)Face.Down, 3] = rot7;
            State[(int)Face.Down, 4] = rot4;
            State[(int)Face.Down, 5] = rot1;
            State[(int)Face.Down, 6] = rot8;
            State[(int)Face.Down, 7] = rot5;
            State[(int)Face.Down, 8] = rot2;

            return this;
        }
        public Cube Dp()
        {
            var t1 = State[(int)Face.Front, 6];
            var t2 = State[(int)Face.Front, 7];
            var t3 = State[(int)Face.Front, 8];

            var rot0 = State[(int)Face.Down, 0];
            var rot1 = State[(int)Face.Down, 1];
            var rot2 = State[(int)Face.Down, 2];
            var rot3 = State[(int)Face.Down, 3];
            var rot4 = State[(int)Face.Down, 4];
            var rot5 = State[(int)Face.Down, 5];
            var rot6 = State[(int)Face.Down, 6];
            var rot7 = State[(int)Face.Down, 7];
            var rot8 = State[(int)Face.Down, 8];

            // front
            State[(int)Face.Front, 6] = State[(int)Face.Right, 6];
            State[(int)Face.Front, 7] = State[(int)Face.Right, 7];
            State[(int)Face.Front, 8] = State[(int)Face.Right, 8];

            // right
            State[(int)Face.Right, 6] = State[(int)Face.Back, 6];
            State[(int)Face.Right, 7] = State[(int)Face.Back, 7];
            State[(int)Face.Right, 8] = State[(int)Face.Back, 8];

            // back
            State[(int)Face.Back, 6] = State[(int)Face.Left, 6];
            State[(int)Face.Back, 7] = State[(int)Face.Left, 7];
            State[(int)Face.Back, 8] = State[(int)Face.Left, 8];

            // left
            State[(int)Face.Left, 6] = t1;
            State[(int)Face.Left, 7] = t2;
            State[(int)Face.Left, 8] = t3;

            // rotate down cw
            State[(int)Face.Down, 0] = rot2;
            State[(int)Face.Down, 1] = rot5;
            State[(int)Face.Down, 2] = rot8;
            State[(int)Face.Down, 3] = rot1;
            State[(int)Face.Down, 4] = rot4;
            State[(int)Face.Down, 5] = rot7;
            State[(int)Face.Down, 6] = rot0;
            State[(int)Face.Down, 7] = rot3;
            State[(int)Face.Down, 8] = rot6;

            return this;
        }

        #endregion

        #region "L moves"

        public Cube L()
        {
            var t1 = State[(int)Face.Up, 0];
            var t2 = State[(int)Face.Up, 3];
            var t3 = State[(int)Face.Up, 6];

            var rot0 = State[(int)Face.Left, 0];
            var rot1 = State[(int)Face.Left, 1];
            var rot2 = State[(int)Face.Left, 2];
            var rot3 = State[(int)Face.Left, 3];
            var rot4 = State[(int)Face.Left, 4];
            var rot5 = State[(int)Face.Left, 5];
            var rot6 = State[(int)Face.Left, 6];
            var rot7 = State[(int)Face.Left, 7];
            var rot8 = State[(int)Face.Left, 8];

            // up
            State[(int)Face.Up, 0] = State[(int)Face.Back, 8];
            State[(int)Face.Up, 3] = State[(int)Face.Back, 5];
            State[(int)Face.Up, 6] = State[(int)Face.Back, 2];

            // back
            State[(int)Face.Back, 2] = State[(int)Face.Down, 6];
            State[(int)Face.Back, 5] = State[(int)Face.Down, 3];
            State[(int)Face.Back, 8] = State[(int)Face.Down, 0];

            // down
            State[(int)Face.Down, 6] = State[(int)Face.Front, 6];
            State[(int)Face.Down, 3] = State[(int)Face.Front, 3];
            State[(int)Face.Down, 0] = State[(int)Face.Front, 0];

            // front
            State[(int)Face.Front, 0] = t1;
            State[(int)Face.Front, 3] = t2;
            State[(int)Face.Front, 6] = t3;

            // rotate down ccw
            State[(int)Face.Left, 0] = rot6;
            State[(int)Face.Left, 1] = rot3;
            State[(int)Face.Left, 2] = rot0;
            State[(int)Face.Left, 3] = rot7;
            State[(int)Face.Left, 4] = rot4;
            State[(int)Face.Left, 5] = rot1;
            State[(int)Face.Left, 6] = rot8;
            State[(int)Face.Left, 7] = rot5;
            State[(int)Face.Left, 8] = rot2;

            return this;
        }
        public Cube Lp()
        {
            var t1 = State[(int)Face.Up, 0];
            var t2 = State[(int)Face.Up, 3];
            var t3 = State[(int)Face.Up, 6];

            var rot0 = State[(int)Face.Left, 0];
            var rot1 = State[(int)Face.Left, 1];
            var rot2 = State[(int)Face.Left, 2];
            var rot3 = State[(int)Face.Left, 3];
            var rot4 = State[(int)Face.Left, 4];
            var rot5 = State[(int)Face.Left, 5];
            var rot6 = State[(int)Face.Left, 6];
            var rot7 = State[(int)Face.Left, 7];
            var rot8 = State[(int)Face.Left, 8];

            // up
            State[(int)Face.Up, 0] = State[(int)Face.Front, 0];
            State[(int)Face.Up, 3] = State[(int)Face.Front, 3];
            State[(int)Face.Up, 6] = State[(int)Face.Front, 6];

            // front
            State[(int)Face.Front, 0] = State[(int)Face.Down, 0];
            State[(int)Face.Front, 3] = State[(int)Face.Down, 3];
            State[(int)Face.Front, 6] = State[(int)Face.Down, 6];

            // down
            State[(int)Face.Down, 0] = State[(int)Face.Back, 8];
            State[(int)Face.Down, 3] = State[(int)Face.Back, 5];
            State[(int)Face.Down, 6] = State[(int)Face.Back, 2];

            // back
            State[(int)Face.Back, 8] = t1;
            State[(int)Face.Back, 5] = t2;
            State[(int)Face.Back, 2] = t3;

            // rotate down cw
            State[(int)Face.Left, 0] = rot2;
            State[(int)Face.Left, 1] = rot5;
            State[(int)Face.Left, 2] = rot8;
            State[(int)Face.Left, 3] = rot1;
            State[(int)Face.Left, 4] = rot4;
            State[(int)Face.Left, 5] = rot7;
            State[(int)Face.Left, 6] = rot0;
            State[(int)Face.Left, 7] = rot3;
            State[(int)Face.Left, 8] = rot6;

            return this;
        }

        #endregion

        #region "drawing'

        public void OutputBitmap()
        {
            OutputBitmap(0);
        }

        public void OutputBitmap(int index)
        {
            using (Image image = new Image<Rgba32>(17 * SQ, 13 * SQ))
            {
                DrawSide(image, 1, 5, Face.Left);
                DrawSide(image, 5, 1, Face.Up);
                DrawSide(image, 5, 5, Face.Front);
                DrawSide(image, 5, 9, Face.Down);
                DrawSide(image, 9, 5, Face.Right);
                DrawSide(image, 13, 5, Face.Back);

                image.SaveAsPng($"cube{index}.png");
            }

        }

        public byte[] ToByteArray()
        {
            using (Image image = new Image<Rgba32>(17 * SQ, 13 * SQ))
            {
                DrawSide(image, 1, 5, Face.Left);
                DrawSide(image, 5, 1, Face.Up);
                DrawSide(image, 5, 5, Face.Front);
                DrawSide(image, 5, 9, Face.Down);
                DrawSide(image, 9, 5, Face.Right);
                DrawSide(image, 13, 5, Face.Back);

                using (var ms = new MemoryStream())
                {
                    image.Save(ms, new SixLabors.ImageSharp.Formats.Png.PngEncoder());
                    return ms.ToArray();
                }
            }
        }

        const int SQ = 50;
        private readonly IEnumerable<Edge> sides1;

        private SixLabors.ImageSharp.Color toIColor(Color c)
        {
            switch (c)
            {
                case Color.yellow:
                    return SixLabors.ImageSharp.Color.Yellow;

                case Color.blue:
                    return SixLabors.ImageSharp.Color.Blue;

                case Color.red:
                    return SixLabors.ImageSharp.Color.Red;

                case Color.green:
                    return SixLabors.ImageSharp.Color.Green;

                case Color.orange:
                    return SixLabors.ImageSharp.Color.Orange;

                case Color.white:
                    return SixLabors.ImageSharp.Color.White;

                default:
                    return SixLabors.ImageSharp.Color.DeepPink;
            }
        }

        private void DrawRectangle(Image img, int x, int y, Color c)
        {
            var rect = new Rectangle(x * SQ, y * SQ, SQ, SQ);
            img.Mutate(ctx => ctx.Fill(toIColor(c), rect));
        }

        private void DrawSide(Image img, int x, int y, Face side)
        {
            Font font = SystemFonts.CreateFont("Arial", 10);

            for (int i = 0; i < 9; i++)
            {
                var color = State[(int)side, i];
                var sx = i % 3;
                var sy = (i - sx) / 3;
                DrawRectangle(img, sx + x, sy + y, color);
                img.Mutate(ctx => ctx.DrawText(side.ToString().First().ToString(), font, SixLabors.ImageSharp.Color.Black, new PointF(x: (float)(sx + x + 0.5) * SQ, y: (float)(sy + y + 0.5) * SQ)));

            }

            for (int i = 0; i < 4; i++)
            {
                img.Mutate(c => c.DrawLines(SixLabors.ImageSharp.Color.Black, 1.0f, linePoints(x, y + i, x + 3, y + i)));
            }
            for (int i = 0; i < 4; i++)
            {
                img.Mutate(c => c.DrawLines(SixLabors.ImageSharp.Color.Black, 1.0f, linePoints(x + i, y, x + i, y + 3)));
            }
        }

        private PointF[] linePoints(int x1, int y1, int x2, int y2)
        {
            var points = new PointF[2];
            points[0] = new PointF(
                x: (float)x1 * SQ,
                y: (float)y1 * SQ);
            points[1] = new PointF(
                x: (float)x2 * SQ,
                y: (float)y2 * SQ);
            return points;
        }

        public Corner LetterToCorner(string letter)
        {
            throw new NotImplementedException();
        }

        public string CornerToLetter(Corner buffer)
        {
            throw new NotImplementedException();
        }



        #endregion
    }
}
