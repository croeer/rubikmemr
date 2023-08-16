using NUnit.Framework;

namespace rubikmemr.Tests
{
    public class SolveTests
    {

        [Test]
        public void R_Edges_Test()
        {
            var cube = new rubikmemr.Cube().
                R();

            var solver = new Solver(cube);
            var meme = solver.SolveEdges();

            Assert.AreEqual("JVT", meme);
            Assert.IsTrue(solver.Parity);
        }

        [Test]
        public void L_Corners_Test()
        {
            var cube = new rubikmemr.Cube().
                L();

            var solver = new Solver(cube);
            var meme = solver.SolveCorners();

            Assert.AreEqual("SUI", meme);
        }

        [Test]
        public void R_Corners_Test()
        {
            var cube = new rubikmemr.Cube().
                R();

            var solver = new Solver(cube);
            var meme = solver.SolveCorners();

            Assert.AreEqual("PONMP", meme);
        }

        #region "Cycles"

        [Test]
        public void Uperm_Test()
        {
            var cube = new rubikmemr.Cube().
                R().Up().R().U().R().U().R().Up().Rp().Up().Rp().Rp();

            var solver = new Solver(cube);
            var meme = solver.SolveEdges();

            Assert.AreEqual("CD", meme);
        }

        [Test]
        public void Superflip_meme_Test()
        {
            var cube = new rubikmemr.Cube().
                U().R().R().F().B().R().B().B().R().
                U().U().L().B().B().R().Up().Dp().R().
                R().F().Rp().L().B().B().U().U().F().F();

            var solver = new Solver(cube);
            var meme = solver.SolveEdges();

            Assert.AreEqual("AQCIDEFLGXHRJPKUNTOVSW", meme);
        }

        [Test]
        public void SolvedBufferPiece_Test()
        {

            var initialState = new Color[,] {
                { Color.yellow, Color.yellow, Color.yellow,
                  Color.yellow, Color.yellow, Color.yellow,
                  Color.yellow, Color.yellow, Color.yellow},
                { Color.blue, Color.orange, Color.blue,
                  Color.blue, Color.blue, Color.blue,
                  Color.blue, Color.blue, Color.blue},
                { Color.red, Color.blue, Color.red,
                  Color.red, Color.red, Color.red,
                  Color.red, Color.red, Color.red},
                { Color.green, Color.green, Color.green,
                  Color.green, Color.green, Color.green,
                  Color.green, Color.green, Color.green},
                { Color.orange, Color.red, Color.orange,
                  Color.orange, Color.orange, Color.orange,
                  Color.orange, Color.orange, Color.orange},
                { Color.white, Color.white, Color.white,
                  Color.white, Color.white, Color.white,
                  Color.white, Color.white, Color.white}
            };

            var cube = new rubikmemr.Cube(initialState);

            var solver = new Solver(cube);
            var meme = solver.SolveEdges();

            Assert.AreEqual("ACD", meme);
        }

        [Test]
        public void Manual_Scramble_Test()
        {

            var initialState = new Color[,] {
                { Color.orange, Color.yellow, Color.yellow,
                  Color.orange, Color.yellow, Color.yellow,
                  Color.red, Color.orange, Color.white},
                { Color.white, Color.blue, Color.green,
                  Color.blue, Color.blue, Color.white,
                  Color.red, Color.blue, Color.blue},
                { Color.yellow, Color.white, Color.red,
                  Color.red, Color.red, Color.red,
                  Color.orange, Color.green, Color.orange},
                { Color.blue, Color.green, Color.green,
                  Color.yellow, Color.green, Color.green,
                  Color.blue, Color.blue, Color.red},
                { Color.orange, Color.orange, Color.green,
                  Color.orange, Color.orange, Color.red,
                  Color.blue, Color.red, Color.white},
                { Color.yellow, Color.white, Color.white,
                  Color.yellow, Color.white, Color.white,
                  Color.green, Color.green, Color.yellow}
            };

            var cube = new rubikmemr.Cube(initialState);

            var solver = new Solver(cube);
            var meme = solver.SolveEdges();

            Assert.AreEqual("ACD", meme);
        }

        [Test]
        public void Boatox_Scramble_Test()
        {
            var cube = new rubikmemr.Cube();
            cube.TurnByString("U R B2 D F2 Dp U F2 R2 F2 Bp R B R2 U2 B R D2 Up Rp");
            var solver = new Solver(cube);
            var edgeMeme = solver.SolveEdges();
            var cornerMeme = solver.SolveEdges();

            Assert.AreEqual("SIRFAEXOPNK", edgeMeme);
            Assert.AreEqual("SIRFAEXOPNK", cornerMeme);
        }

        [Test]
        public void Edges_Only_Test()
        {
            var cube = new rubikmemr.Cube();
            cube.TurnByString("U2 B2 D2 R B2 R U2 F2 U2 R' D2 F R' D' U B U' L2 B2 R'");
            var solver = new Solver(cube);
            var meme = solver.SolveEdges();

            Assert.AreEqual("SIRFAEXOPNK", meme);
        }

        [Test]
        public void Corners_Only_Test()
        {
            var cube = new rubikmemr.Cube();
            cube.TurnByString("L' U2 F' L2 F U2 L2 U2 R2 U2 F U2 F2 R F L' F' R' F R2");
            var solver = new Solver(cube);
            var meme = solver.SolveCorners();

            Assert.AreEqual("TQDSPCL", meme);
        }
        #endregion
    }
}