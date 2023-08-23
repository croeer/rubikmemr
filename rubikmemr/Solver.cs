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

            if (edgeMeme.Length % 2 != cornerMeme.Length % 2)
                throw new InvalidOperationException($"Parity error: {edgeMeme}:{cornerMeme}");

            return edgeMeme + "." + (Parity ? "BCB." : String.Empty) + cornerMeme;
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

                if (cube.IsCornerSolved(aktCorner))
                {
                    // get other starting piece
                    aktCorner = GetUnsolvedCorner();
                }
                else
                {
                    cornerMeme.Push("P");
                }
            }

            do
            {
                NewCornerCycle(aktCorner);
                aktCorner = GetUnsolvedCorner();
                if (aktCorner is null)
                    break;

                var letter = cube.CornerToLetter(aktCorner);
                cornerMeme.Push(letter);
                aktCorner = cube.LetterToCorner(letter);


            } while (true);

            return String.Join(String.Empty, cornerMeme.Reverse().ToArray());
        }

        private void NewCornerCycle(Corner startCorner)
        {
            var aktCorner = startCorner;

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
                    break;
                }

                aktCorner = cube.LetterToCorner(letter);
                cornerMeme.Push(letter);

            } while (!visitedCorners.Contains(aktCorner));

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
            Edge aktEdge = buffer;

            if (cube.EdgeToLetter(buffer) == "M" || cube.EdgeToLetter(buffer) == "B")
            {
                aktEdge = cube.LetterToEdge("D");

                if (cube.IsEdgeSolved(aktEdge))
                {
                    // get other starting piece
                    aktEdge = GetUnsolvedEdge();
                }
                else
                {
                    edgeMeme.Push("D");
                }
            }
            do
            {
                NewEdgeCycle(aktEdge);
                aktEdge = GetUnsolvedEdge();
                if (aktEdge is null)
                    break;

                var letter = cube.EdgeToLetter(aktEdge);
                cornerMeme.Push(letter);
                aktEdge = cube.LetterToEdge(letter);

            } while (true);

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
            var aktEdge = startEdge;
            do
            {
                visitedEdges.Add(aktEdge);
                visitedEdges.Add(cube.InverseEdge(aktEdge));

                var letter = cube.EdgeToLetter(aktEdge);

                // buffer piece found? then start new cycle
                if (letter == "B" || letter == "M")
                {
                    break;
                }

                aktEdge = cube.LetterToEdge(letter);
                edgeMeme.Push(letter);

            } while (!visitedEdges.Contains(aktEdge));

        }

        #endregion

    }
}
