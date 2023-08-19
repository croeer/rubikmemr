namespace rubikmemr
{
    public class Solver
    {
        private readonly Cube cube;

        public bool Parity;

        Stack<string> edgeMeme = new Stack<string>();
        Stack<string> cornerMeme = new Stack<string>();

        public Solver(Cube cube)
        {
            this.cube = cube;
        }

        public string Solve()
        {
            var edgeMeme = SolveEdges();
            var cornerMeme = SolveCorners();

            return edgeMeme + (Parity ? "CBC" : String.Empty) + cornerMeme;
        }

        #region "Corners"

        HashSet<Corner> visitedCorners = new HashSet<Corner>();

        public string SolveCorners()
        {
            var buffer = cube.LetterToCorner("A");
            Corner aktCorner = buffer;

            if (cube.CornerToLetter(buffer) == "A" || cube.CornerToLetter(buffer) == "R" || cube.CornerToLetter(buffer) == "E")
            {
                aktCorner = cube.LetterToCorner("P");
                cornerMeme.Push("P");
                // get other starting piece
                //aktCorner = GetUnsolvedCorner();
            }

            do
            {
                NewCornerCycle(aktCorner);
                aktCorner = GetUnsolvedCorner();

            } while (aktCorner is not null);

            return String.Join(String.Empty, cornerMeme.Reverse().ToArray());
        }

        private void NewCornerCycle(Corner startCorner)
        {
            var aktCorner = startCorner;

            var startPieces = new HashSet<Corner>();
            startPieces.Add(startCorner);
            foreach (Corner corner in cube.InverseCorners(startCorner))
            {
                startPieces.Add(corner);
            }


            do
            {
                visitedCorners.Add(aktCorner);
                foreach (Corner corner in cube.InverseCorners(aktCorner))
                {
                    visitedCorners.Add(corner);
                }

                var letter = cube.CornerToLetter(aktCorner);

                // buffer piece found? then start new cycle
                if (letter == "A" || letter == "E" || letter == "R")
                {
                    //var lastLetter = cornerMeme.Pop();
                    //var lastPiece = cube.LetterToCorner(lastLetter);
                    //visitedCorners.Remove(lastPiece);
                    //foreach (Corner corner in cube.InverseCorners(lastPiece))
                    //{
                    //    visitedCorners.Remove(corner);
                    //}
                    break;
                }

                aktCorner = cube.LetterToCorner(letter);
                cornerMeme.Push(letter);

            } while (!startPieces.Contains(aktCorner));

        }

        private Corner GetUnsolvedCorner()
        {
            foreach (Corner tempCorner in cube.Corners.Where(x => !visitedCorners.Contains(x))) // && !cube.CornerToLetter(x).Equals("A") && !cube.CornerToLetter(x).Equals("E") && !cube.CornerToLetter(x).Equals("R")))
            {
                var skipIteration = false;

                foreach (Corner inverseCorner in cube.InverseCorners(tempCorner))
                {
                    if (visitedCorners.Contains(inverseCorner))
                    {
                        skipIteration = true;
                    }
                }

                if (skipIteration)
                {
                    continue;
                }

                var color1 = cube.State[(int)tempCorner.position1.face, tempCorner.position1.index];
                var color2 = cube.State[(int)tempCorner.position2.face, tempCorner.position2.index];
                var color3 = cube.State[(int)tempCorner.position3.face, tempCorner.position3.index];
                if (color1 != tempCorner.color1 || color2 != tempCorner.color2 || color3 != tempCorner.color3)
                {
                    return tempCorner;
                }
            }
#pragma warning disable CS8603 // Possible null reference return.
            return null;
#pragma warning restore CS8603 // Possible null reference return.
        }

        #endregion

        #region "Edges"

        HashSet<Edge> visitedEdges = new HashSet<Edge>();

        public string SolveEdges()
        {
            var buffer = cube.LetterToEdge("B");
            Edge aktSide = buffer;

            if (cube.EdgeToLetter(buffer) == "M" || cube.EdgeToLetter(buffer) == "B")
            {
                // get other starting piece
                aktSide = GetUnsolvedEdge();
            }

            do
            {
                NewEdgeCycle(aktSide);
                aktSide = GetUnsolvedEdge();

            } while (aktSide is not null);

            if (edgeMeme.Count() % 2 != 0)
            {
                Parity = true;
            }

            return String.Join(String.Empty, edgeMeme.Reverse().ToArray());
        }

        private Edge GetUnsolvedEdge()
        {
            foreach (Edge tempEdge in cube.Edges.Where(x => !visitedEdges.Contains(x) && !cube.EdgeToLetter(x).Equals("B") && !cube.EdgeToLetter(x).Equals("M")))
            {
                var inverseEdge = cube.InverseEdge(tempEdge);
                if (visitedEdges.Contains(inverseEdge))
                {
                    continue;
                }

                var color1 = cube.State[(int)tempEdge.position1.face, tempEdge.position1.index];
                var color2 = cube.State[(int)tempEdge.position2.face, tempEdge.position2.index];
                if (color1 != tempEdge.color1 || color2 != tempEdge.color2)
                {
                    return tempEdge;
                }
            }
#pragma warning disable CS8603 // Possible null reference return.
            return null;
#pragma warning restore CS8603 // Possible null reference return.
        }

        private void NewEdgeCycle(Edge startEdge)
        {
            //Console.WriteLine(startEdge);
            var aktEdge = startEdge;
            do
            {
                visitedEdges.Add(aktEdge);
                visitedEdges.Add(cube.InverseEdge(aktEdge));

                var letter = cube.EdgeToLetter(aktEdge);
                aktEdge = cube.LetterToEdge(letter);
                edgeMeme.Push(letter);

            } while (!visitedEdges.Contains(aktEdge));

            if (edgeMeme.Peek() == "B" || edgeMeme.Peek() == "M")
            {
                edgeMeme.Pop();
            }
        }

        #endregion

    }
}
