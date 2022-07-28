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

        public string SolveCorners()
        {
            var buffer = cube.LetterToCorner("P");
            Corner aktCorner = buffer;

            if (cube.CornerToLetter(buffer) == "M" || cube.CornerToLetter(buffer) == "B")
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

        private void NewCornerCycle(Corner aktCorner)
        {
            throw new NotImplementedException();
        }

        private Corner GetUnsolvedCorner()
        {
            throw new NotImplementedException();
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

        private void NewEdgeCycle(Edge start)
        {
            var aktSide = start;
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
