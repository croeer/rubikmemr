using System.Text;

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

        #region "Corners"

        HashSet<Corner> visitedCorners = new HashSet<Corner>();

        public string SolveCorners()
        {
            var buffer = cube.LetterToCorner("P");
            Corner aktCorner = buffer;

            if (cube.CornerToLetter(buffer) == "K" || cube.CornerToLetter(buffer) == "P" || cube.CornerToLetter(buffer) == "V")
            {
                // get other starting piece
                aktCorner = GetUnsolvedCorner();
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
            do
            {
                visitedCorners.Add(aktCorner);

                var letter = cube.CornerToLetter(aktCorner);
                aktCorner = cube.LetterToCorner(letter);
                cornerMeme.Push(letter);

            } while (!visitedCorners.Contains(aktCorner));

            if (cornerMeme.Peek() == "P")
            {
                cornerMeme.Pop();
            }
        }

        private Corner GetUnsolvedCorner()
        {
            foreach (Corner tempCorner in cube.Corners.Where(x => !visitedCorners.Contains(x) && !cube.CornerToLetter(x).Equals("K") && !cube.CornerToLetter(x).Equals("P") && !cube.CornerToLetter(x).Equals("V")))
            {
                foreach (Corner inverseCorner in cube.InverseCorners(tempCorner))
                {
                    if (visitedCorners.Contains(inverseCorner))
                    {
                        continue;
                    }
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
            var aktSide = startEdge;
            do
            {
                visitedEdges.Add(aktSide);

                var letter = cube.EdgeToLetter(aktSide);
                aktSide = cube.LetterToEdge(letter);
                edgeMeme.Push(letter);

            } while (!visitedEdges.Contains(aktSide));

            if (edgeMeme.Peek() == "B")
            {
                edgeMeme.Pop();
            }
        }

        #endregion

    }
}
