using NUnit.Framework;

namespace rubikmemr.Tests
{
    public class SolveTests
    {

        [Test]
        public void R_Complete_Test()
        {
            var cube = new rubikmemr.Cube().
                R();

            var solver = new Solver(cube);
            var meme = solver.Solve();

            Assert.AreEqual("JVT.BCB.PONMP", meme);
            Assert.IsTrue(solver.Parity);
        }

        [Test]
        public void L_Complete_Test()
        {
            var cube = new rubikmemr.Cube().
                L();

            var solver = new Solver(cube);
            var meme = solver.Solve();

            Assert.AreEqual("DRXLD.BCB.SUI", meme);
            Assert.IsTrue(solver.Parity);
        }

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
        public void L_Edges_Test()
        {
            var cube = new rubikmemr.Cube().
                L();

            var solver = new Solver(cube);
            var meme = solver.SolveEdges();

            Assert.AreEqual("DRXLD", meme);
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
        public void Flipped_Corner_Q_Test()
        {

            var cube = new rubikmemr.Cube();

            // turn "Q"
            cube.TurnByString("Rp F"); // Setup
            cube.TurnCornerSwitch();
            cube.TurnByString("Fp R"); // undo Setup

            var solver = new Solver(cube);
            var meme = solver.SolveCorners();

            Assert.AreEqual("Q", meme);
        }

        [Test]
        public void Flipped_Corner_A_Test()
        {
            var initialState = new Color[,] {
                { Color.blue, Color.yellow, Color.yellow,
                  Color.yellow, Color.yellow, Color.green,
                  Color.yellow, Color.red, Color.yellow},
                { Color.orange, Color.blue, Color.blue,
                  Color.blue, Color.blue, Color.blue,
                  Color.blue, Color.blue, Color.blue},
                { Color.red, Color.yellow, Color.red,
                  Color.red, Color.red, Color.red,
                  Color.red, Color.red, Color.white},
                { Color.green, Color.yellow, Color.green,
                  Color.green, Color.green, Color.green,
                  Color.red, Color.green, Color.green},
                { Color.orange, Color.orange, Color.yellow,
                  Color.orange, Color.orange, Color.orange,
                  Color.orange, Color.orange, Color.orange},
                { Color.white, Color.white, Color.green,
                  Color.white, Color.white, Color.white,
                  Color.white, Color.white, Color.white}
            };
            // turn "KP"
            var cube = new rubikmemr.Cube(initialState);

            var solver = new Solver(cube);
            var meme = solver.SolveCorners();

            Assert.AreEqual("PK", meme);
        }


        [Test]
        public void Reverse_Meme_BD_Test()
        {

            var cube = new rubikmemr.Cube();

            // turn "B"
            cube.TurnByString("R Dp"); // Setup
            cube.TurnCornerSwitch();
            cube.TurnByString("D Rp"); // undo Setup

            // turn "D"
            cube.TurnByString("F Rp"); // Setup
            cube.TurnCornerSwitch();
            cube.TurnByString("R Fp"); // undo Setup

            var solver = new Solver(cube);
            var meme = solver.SolveCorners();

            Assert.AreEqual("DB", meme);
        }

        [Test]
        public void UR_Corners_Test()
        {
            var cube = new rubikmemr.Cube().
                U().R();

            var solver = new Solver(cube);
            var meme = solver.SolveCorners();

            Assert.AreEqual("DCKWBN", meme);
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

        [Test]
        public void CB_Test()
        {
            var cube = new rubikmemr.Cube().
                Rp().Dp().R().U().U().Rp().D().R().Up().Rp().Dp().R().Up().Rp().D().R();

            var solver = new Solver(cube);
            var meme = solver.SolveCorners();

            Assert.AreEqual("CB", meme);
            Assert.IsFalse(solver.Parity);
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

            Assert.AreEqual("DEAQCIFLGXHRJPKUNTOVSW", meme);
        }

        [Test]
        public void Flipped_Edge_CI_Test()
        {

            var initialState = new Color[,] {
                { Color.yellow, Color.yellow, Color.yellow,
                  Color.yellow, Color.yellow, Color.green,
                  Color.yellow, Color.red, Color.yellow},
                { Color.blue, Color.blue, Color.blue,
                  Color.blue, Color.blue, Color.blue,
                  Color.blue, Color.blue, Color.blue},
                { Color.red, Color.yellow, Color.red,
                  Color.red, Color.red, Color.red,
                  Color.red, Color.red, Color.red},
                { Color.green, Color.yellow, Color.green,
                  Color.green, Color.green, Color.green,
                  Color.green, Color.green, Color.green},
                { Color.orange, Color.orange, Color.orange,
                  Color.orange, Color.orange, Color.orange,
                  Color.orange, Color.orange, Color.orange},
                { Color.white, Color.white, Color.white,
                  Color.white, Color.white, Color.white,
                  Color.white, Color.white, Color.white}
            };

            // same as turning "CI"
            var cube = new rubikmemr.Cube(initialState);

            var solver = new Solver(cube);
            var meme = solver.SolveEdges();

            Assert.AreEqual("CI", meme);
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
            // T-Perm
            var cube = new rubikmemr.Cube(initialState);

            var solver = new Solver(cube);
            var meme = solver.SolveEdges();

            Assert.AreEqual("DACD", meme);
        }

        [Test]
        public void Boatox_Scramble_Test()
        {
            var cube = new rubikmemr.Cube();
            cube.TurnByString("U R B2 D F2 Dp U F2 R2 F2 Bp R B R2 U2 B R D2 Up Rp");
            var solver = new Solver(cube);
            var edgeMeme = solver.SolveEdges();
            var cornerMeme = solver.SolveCorners();

            Assert.AreEqual("XEWUQOFHJNCI", edgeMeme, "edges failed");
            Assert.AreEqual("XPQDCLOC", cornerMeme, "corners failed");
            Assert.IsFalse(solver.Parity);

            var fullMeme = new Solver(cube).Solve();
            Assert.AreEqual("XEWUQOFHJNCI.XPQDCLOC", fullMeme, "full meme failed");
        }

        [Test]
        public void Edges_Only_Test()
        {
            var cube = new rubikmemr.Cube();
            cube.TurnByString("U2 B2 D2 R B2 R U2 F2 U2 R' D2 F R' D' U B U' L2 B2 R'");
            var solver = new Solver(cube);
            var meme = solver.SolveEdges();

            Assert.AreEqual("SIRFAEXOPNKA", meme);
        }

        [Test]
        public void Corners_Only_Test()
        {
            var cube = new rubikmemr.Cube();
            cube.TurnByString("L' U2 F' L2 F U2 L2 U2 R2 U2 F U2 F2 R F L' F' R' F R2");
            var solver = new Solver(cube);
            var meme = solver.SolveCorners();

            Assert.AreEqual("TQDSPCLJ", meme);
        }
        #endregion
    }
}