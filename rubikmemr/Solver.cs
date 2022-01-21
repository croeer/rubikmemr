using System.Text;

namespace rubikmemr
{
    public class Solver
    {
        private readonly Cube cube;

        public bool Parity;

        Stack<string> meme = new Stack<string>();

        public Solver(Cube cube)
        {
            this.cube = cube;
        }

        HashSet<Edge> visitedSides = new HashSet<Edge>();

        public string SolveSides()
        {
            var buffer = cube.LetterToEdge("B");
            Edge aktSide = buffer;

            if (cube.EdgeToLetter(buffer) == "M" || cube.EdgeToLetter(buffer) == "B")
            {
                // get other starting piece
                aktSide = GetUnsolvedSide();
            }

            do
            {
                NewCycle(aktSide);
                aktSide = GetUnsolvedSide();

            } while (aktSide is not null);

            return String.Join(String.Empty, meme.Reverse().ToArray());
        }

        private Edge GetUnsolvedSide()
        {
            foreach(Edge tempSide in cube.Edges.Where(x => !visitedSides.Contains(x) && !cube.EdgeToLetter(x).Equals("B") && !cube.EdgeToLetter(x).Equals("M")))
            {
                var inverseSide = cube.InverseSide(tempSide);
                if(visitedSides.Contains(inverseSide))
                {
                    continue;
                }

                var color1 = cube.State[(int)tempSide.position1.face, tempSide.position1.index];
                var color2 = cube.State[(int)tempSide.position2.face, tempSide.position2.index];
                if(( color1 != tempSide.color1 || color2 != tempSide.color2 )) // && (color1 != tempSide.color2 || color2 != tempSide.color1))
                {
                    return tempSide;
                }
            }
#pragma warning disable CS8603 // Possible null reference return.
            return null;
#pragma warning restore CS8603 // Possible null reference return.
        }

        private void NewCycle(Edge start)
        {
            var aktSide = start;
            do
            {
                visitedSides.Add(aktSide);

                var letter = cube.EdgeToLetter(aktSide);
                aktSide = cube.LetterToEdge(letter);
                meme.Push(letter);

            } while (!visitedSides.Contains(aktSide));

            if(meme.Peek() == "B")
            {
                meme.Pop();
            }
        }

    }
}
