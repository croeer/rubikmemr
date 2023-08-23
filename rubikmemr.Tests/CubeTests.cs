using NUnit.Framework;

namespace rubikmemr.Tests
{
    public class CubeTests
    {

        [Test]
        public void DefaultCube_Test()
        {
            var cube = new rubikmemr.Cube();

            var expectedState = new Color[,] {
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

            Assert.AreEqual(expectedState, cube.State);
        }

        [Test]
        public void FLipped_Corner_Detection_Test()
        {
            var cube = new rubikmemr.Cube();
            cube.U().R();

            var flippedCorner = cube.LetterToCorner("B");
            Assert.IsTrue(cube.IsCornerFlipped(flippedCorner));
        }


        #region "turn by string"

        [Test]
        public void String_R_Test()
        {
            var cube = new rubikmemr.Cube();

            cube.TurnByString("R");

            var expectedState = new Color[,] {
                { Color.yellow, Color.yellow, Color.red,
                  Color.yellow, Color.yellow, Color.red,
                  Color.yellow, Color.yellow, Color.red},
                { Color.blue, Color.blue, Color.blue,
                  Color.blue, Color.blue, Color.blue,
                  Color.blue, Color.blue, Color.blue},
                { Color.red, Color.red, Color.white,
                  Color.red, Color.red, Color.white,
                  Color.red, Color.red, Color.white},
                { Color.green, Color.green, Color.green,
                  Color.green, Color.green, Color.green,
                  Color.green, Color.green, Color.green},
                { Color.yellow, Color.orange, Color.orange,
                  Color.yellow, Color.orange, Color.orange,
                  Color.yellow, Color.orange, Color.orange},
                { Color.white, Color.white, Color.orange,
                  Color.white, Color.white, Color.orange,
                  Color.white, Color.white, Color.orange}
            };

            Assert.AreEqual(expectedState, cube.State);
        }

        [Test]
        public void String_R_U_D_Rp_D2_Test()
        {
            var cube1 = new rubikmemr.Cube();
            cube1.TurnByString("R U D R' D2");

            var cube2 = new rubikmemr.Cube().
                R().U().D().Rp().D().D();

            Assert.AreEqual(cube2.State, cube1.State);
        }

        #endregion

        #region "manual tests for debugging"

        [Test]
        public void Dp_Dp_Test()
        {

            var cube1 = new rubikmemr.Cube().
               L().L().U().U().R().R().Up().B().B().Up().
               L().L().R().R().U().B().B().F().F().U().U().
               Lp().D().Bp().L().L().B().U().Fp().Up().B().
               Dp();

            var cube2 = new rubikmemr.Cube().
               L().L().U().U().R().R().Up().B().B().Up().
               L().L().R().R().U().B().B().F().F().U().U().
               Lp().D().Bp().L().L().B().U().Fp().Up().B().
               Dp();

            Assert.AreEqual(cube2.State, cube1.State);

        }

        [Test]
        public void Dp_F_Test()
        {

            var cube = new rubikmemr.Cube().
            Dp().F();

            var expectedState = new Color[,] {
                { Color.yellow, Color.yellow, Color.yellow,
                  Color.yellow, Color.yellow, Color.yellow,
                  Color.red, Color.blue, Color.blue},
                { Color.blue, Color.blue, Color.white,
                  Color.blue, Color.blue, Color.white,
                  Color.red, Color.red, Color.white},
                { Color.green, Color.red, Color.red,
                  Color.green, Color.red, Color.red,
                  Color.green, Color.red, Color.red},
                { Color.yellow, Color.green, Color.green,
                  Color.yellow, Color.green, Color.green,
                  Color.yellow, Color.orange, Color.orange},
                { Color.orange, Color.orange, Color.orange,
                  Color.orange, Color.orange, Color.orange,
                  Color.blue, Color.blue, Color.blue},
                { Color.orange, Color.green, Color.green,
                  Color.white, Color.white, Color.white,
                  Color.white, Color.white, Color.white}
            };

            Assert.AreEqual(expectedState, cube.State);
        }

        [Test]
        public void Dp_Dp_F_Test()
        {

            var cube = new rubikmemr.Cube().
            Dp().Dp().F();

            var expectedState = new Color[,] {
                { Color.yellow, Color.yellow, Color.yellow,
                  Color.yellow, Color.yellow, Color.yellow,
                  Color.green, Color.blue, Color.blue},
                { Color.blue, Color.blue, Color.white,
                  Color.blue, Color.blue, Color.white,
                  Color.green, Color.green, Color.white},
                { Color.orange, Color.red, Color.red,
                  Color.orange, Color.red, Color.red,
                  Color.orange, Color.red, Color.red},
                { Color.yellow, Color.green, Color.green,
                  Color.yellow, Color.green, Color.green,
                  Color.yellow, Color.blue, Color.blue},
                { Color.orange, Color.orange, Color.orange,
                  Color.orange, Color.orange, Color.orange,
                  Color.red, Color.red, Color.red},
                { Color.blue, Color.green, Color.green,
                  Color.white, Color.white, Color.white,
                  Color.white, Color.white, Color.white}
            };

            Assert.AreEqual(expectedState, cube.State);
        }


        [Test]
        public void fehlersuche1_Test()
        {
            var cube = new rubikmemr.Cube().
            Lp().U().U().Fp().L().L().F().U().U().L().L().U();

            var expectedState = new Color[,] {
                { Color.blue, Color.yellow, Color.orange,
                  Color.yellow, Color.yellow, Color.blue,
                  Color.green, Color.orange, Color.green},
                { Color.yellow, Color.red, Color.orange,
                  Color.blue, Color.blue, Color.yellow,
                  Color.blue, Color.blue, Color.orange},
                { Color.yellow, Color.blue, Color.yellow,
                  Color.orange, Color.red, Color.red,
                  Color.yellow, Color.red, Color.red},
                { Color.red, Color.yellow, Color.white,
                  Color.green, Color.green, Color.green,
                  Color.green, Color.green, Color.green},
                { Color.blue, Color.green, Color.red,
                  Color.orange, Color.orange, Color.white,
                  Color.orange, Color.orange, Color.white},
                { Color.blue, Color.white, Color.white,
                  Color.red, Color.white, Color.white,
                  Color.red, Color.white, Color.white}
            };

            Assert.AreEqual(expectedState, cube.State);
        }

        [Test]
        public void fehlersuche2_Test()
        {
            var cube = new rubikmemr.Cube().
            Lp().U().U().Fp().L().L();

            var expectedState = new Color[,] {
                { Color.green, Color.yellow, Color.red,
                  Color.orange, Color.yellow, Color.red,
                  Color.orange, Color.green, Color.green},
                { Color.yellow, Color.blue, Color.blue,
                  Color.yellow, Color.blue, Color.blue,
                  Color.red, Color.green, Color.green},
                { Color.yellow, Color.red, Color.red,
                  Color.yellow, Color.red, Color.red,
                  Color.red, Color.white, Color.white},
                { Color.white, Color.blue, Color.blue,
                  Color.white, Color.green, Color.green,
                  Color.orange, Color.green, Color.green},
                { Color.white, Color.red, Color.orange,
                  Color.orange, Color.orange, Color.orange,
                  Color.orange, Color.orange, Color.yellow},
                { Color.yellow, Color.blue, Color.blue,
                  Color.yellow, Color.white, Color.white,
                  Color.blue, Color.white, Color.white}
            };

            Assert.AreEqual(expectedState, cube.State);
        }

        [Test]
        public void fehlersuche3_Test()
        {
            var cube = new rubikmemr.Cube().
            Lp().U().U();//.Fp().L().L();

            var expectedState = new Color[,] {
                { Color.yellow, Color.yellow, Color.red,
                  Color.yellow, Color.yellow, Color.red,
                  Color.yellow, Color.yellow, Color.red},
                { Color.green, Color.green, Color.green,
                  Color.blue, Color.blue, Color.blue,
                  Color.blue, Color.blue, Color.blue},
                { Color.orange, Color.orange, Color.yellow,
                  Color.white, Color.red, Color.red,
                  Color.white, Color.red, Color.red},
                { Color.blue, Color.blue, Color.blue,
                  Color.green, Color.green, Color.green,
                  Color.green, Color.green, Color.green},
                { Color.white, Color.red, Color.red,
                  Color.orange, Color.orange, Color.yellow,
                  Color.orange, Color.orange, Color.yellow},
                { Color.orange, Color.white, Color.white,
                  Color.orange, Color.white, Color.white,
                  Color.orange, Color.white, Color.white}
            };

            Assert.AreEqual(expectedState, cube.State);
        }

        [Test]
        public void fehlersuche4_Test()
        {
            var cube = new rubikmemr.Cube().
            Lp().U().U().Fp();

            var expectedState = new Color[,] {
                { Color.yellow, Color.yellow, Color.red,
                  Color.yellow, Color.yellow, Color.red,
                  Color.blue, Color.green, Color.green},
                { Color.green, Color.green, Color.red,
                  Color.blue, Color.blue, Color.yellow,
                  Color.blue, Color.blue, Color.yellow},
                { Color.yellow, Color.red, Color.red,
                  Color.orange, Color.red, Color.red,
                  Color.orange, Color.white, Color.white},
                { Color.white, Color.blue, Color.blue,
                  Color.white, Color.green, Color.green,
                  Color.orange, Color.green, Color.green},
                { Color.white, Color.red, Color.red,
                  Color.orange, Color.orange, Color.yellow,
                  Color.orange, Color.orange, Color.yellow},
                { Color.green, Color.blue, Color.blue,
                  Color.orange, Color.white, Color.white,
                  Color.orange, Color.white, Color.white}
            };

            Assert.AreEqual(expectedState, cube.State);
        }

        #endregion

        #region "corners only"

        [Test]
        public void Corners_Only_Test()
        {
            /*
             * L' U2 F' L2 F U2 L2 U2 R2 U2 F U2 F2 R F L' F' R' F R2
             * https://cstimer.net/
             * N4IgzgxgTgYgNgFxALhAbQDoggeygOwFMowsAafAVzjgF0QzxoAVATwAdCVs8iSGQCHDjhgUCKJUIBfIA
            */

            var cube = new rubikmemr.Cube().
            Lp().U().U().Fp().L().L().F().U().U().L().L().U().
            U().R().R().U().U().F().U().U().F().F().R().F().
            Lp().Fp().Rp().F().R().R();

            var expectedState = new Color[,] {
                { Color.orange, Color.yellow, Color.blue,
                  Color.yellow, Color.yellow, Color.yellow,
                  Color.orange, Color.yellow, Color.red},
                { Color.white, Color.blue, Color.white,
                  Color.blue, Color.blue, Color.blue,
                  Color.white, Color.blue, Color.green},
                { Color.blue, Color.red, Color.blue,
                  Color.red, Color.red, Color.red,
                  Color.red, Color.red, Color.yellow},
                { Color.white, Color.green, Color.red,
                  Color.green, Color.green, Color.green,
                  Color.blue, Color.green, Color.yellow},
                { Color.yellow, Color.orange, Color.green,
                  Color.orange, Color.orange, Color.orange,
                  Color.orange, Color.orange, Color.green},
                { Color.yellow, Color.white, Color.orange,
                  Color.white, Color.white, Color.white,
                  Color.red, Color.white, Color.green}
            };

            Assert.AreEqual(expectedState, cube.State);
        }

        #endregion

        #region "scrambles"

        [Test]
        public void Scramble1_Test()
        {
            /*
             * L2 U2 R2 U' B2 U' L2 R2 U B2 F2 U2 L' D B' L2 B U F' U' B
             * https://cstimer.net/
             * N4IgzgxgTg0gpgTwLIgFwBcoFc4BoToD2hANmGpjvpFAGInpogDaAOiAJYB2ADluu1xcsJEgF0Q1aABUEPOE259G+CKQhYARgtQgAxADMjegAwmDhzQFZDBgGx6ALAYCMegCZmQAXyA
            */

            var cube = new rubikmemr.Cube().
            L().L().U().U().R().R().Up().B().B().Up().
            L().L().R().R().U().B().B().F().F().U().U().
            Lp().D().Bp().L().L().B().U().Fp().Up().B();

            var expectedState = new Color[,] {
                { Color.orange, Color.blue, Color.blue,
                  Color.yellow, Color.yellow, Color.green,
                  Color.red, Color.white, Color.orange},
                { Color.yellow, Color.blue, Color.white,
                  Color.white, Color.blue, Color.yellow,
                  Color.blue, Color.blue, Color.yellow},
                { Color.green, Color.green, Color.yellow,
                  Color.red, Color.red, Color.green,
                  Color.green, Color.green, Color.green},
                { Color.green, Color.red, Color.orange,
                  Color.orange, Color.green, Color.orange,
                  Color.orange, Color.blue, Color.yellow},
                { Color.white, Color.orange, Color.blue,
                  Color.white, Color.orange, Color.red,
                  Color.red, Color.yellow, Color.white},
                { Color.red, Color.yellow, Color.white,
                  Color.red, Color.white, Color.white,
                  Color.red, Color.orange, Color.blue}
            };

            Assert.AreEqual(expectedState, cube.State);
        }

        #endregion

        #region "special moves"

        [Test]
        public void SexyMove_6_Default_Test()
        {
            var defaultCube = new Cube();

            var sexyCube = new Cube();
            for (int i = 0; i < 6; i++)
            {
                sexyCube.R().U().Rp().Up();
            }
            sexyCube.OutputBitmap();

            Assert.AreEqual(defaultCube.State, sexyCube.State);
        }


        [Test]
        public void SexyMoveInvert_6_Default_Test()
        {
            var defaultCube = new Cube();

            var sexyCube = new Cube();
            for (int i = 0; i < 6; i++)
            {
                sexyCube.Up().Rp().U().R();
            }

            Assert.AreEqual(defaultCube.State, sexyCube.State);
        }

        #endregion

        #region "F_Moves"

        [Test]
        public void F_Test()
        {
            var cube = new rubikmemr.Cube().F();

            var expectedState = new Color[,] {
                { Color.yellow, Color.yellow, Color.yellow,
                  Color.yellow, Color.yellow, Color.yellow,
                  Color.blue, Color.blue, Color.blue},
                { Color.blue, Color.blue, Color.white,
                  Color.blue, Color.blue, Color.white,
                  Color.blue, Color.blue, Color.white},
                { Color.red, Color.red, Color.red,
                  Color.red, Color.red, Color.red,
                  Color.red, Color.red, Color.red},
                { Color.yellow, Color.green, Color.green,
                  Color.yellow, Color.green, Color.green,
                  Color.yellow, Color.green, Color.green},
                { Color.orange, Color.orange, Color.orange,
                  Color.orange, Color.orange, Color.orange,
                  Color.orange, Color.orange, Color.orange},
                { Color.green, Color.green, Color.green,
                  Color.white, Color.white, Color.white,
                  Color.white, Color.white, Color.white}
            };

            Assert.AreEqual(expectedState, cube.State);
        }

        [Test]
        public void Fprime_Test()
        {
            var cube = new rubikmemr.Cube().Fp();

            var expectedState = new Color[,] {
                { Color.yellow, Color.yellow, Color.yellow,
                  Color.yellow, Color.yellow, Color.yellow,
                  Color.green, Color.green, Color.green},
                { Color.blue, Color.blue, Color.yellow,
                  Color.blue, Color.blue, Color.yellow,
                  Color.blue, Color.blue, Color.yellow},
                { Color.red, Color.red, Color.red,
                  Color.red, Color.red, Color.red,
                  Color.red, Color.red, Color.red},
                { Color.white, Color.green, Color.green,
                  Color.white, Color.green, Color.green,
                  Color.white, Color.green, Color.green},
                { Color.orange, Color.orange, Color.orange,
                  Color.orange, Color.orange, Color.orange,
                  Color.orange, Color.orange, Color.orange},
                { Color.blue, Color.blue, Color.blue,
                  Color.white, Color.white, Color.white,
                  Color.white, Color.white, Color.white}
            };

            Assert.AreEqual(expectedState, cube.State);
        }

        [Test]
        public void F_Fprime_Default_Test()
        {
            var defaultCube = new Cube();

            Assert.AreEqual(defaultCube.State, defaultCube.F().Fp().State);
            Assert.AreEqual(defaultCube.State, defaultCube.Fp().F().State);
        }

        [Test]
        public void F2_FF_Test()
        {
            var cube1 = new Cube().F2();
            var cube2 = new Cube().F().F();

            Assert.AreEqual(cube1.State, cube2.State);
        }

        #endregion

        #region "U moves"

        [Test]
        public void U_Test()
        {
            var cube = new rubikmemr.Cube().U();

            var expectedState = new Color[,] {
                { Color.yellow, Color.yellow, Color.yellow,
                  Color.yellow, Color.yellow, Color.yellow,
                  Color.yellow, Color.yellow, Color.yellow},
                { Color.red, Color.red, Color.red,
                  Color.blue, Color.blue, Color.blue,
                  Color.blue, Color.blue, Color.blue},
                { Color.green, Color.green, Color.green,
                  Color.red, Color.red, Color.red,
                  Color.red, Color.red, Color.red},
                { Color.orange, Color.orange, Color.orange,
                  Color.green, Color.green, Color.green,
                  Color.green, Color.green, Color.green},
                { Color.blue, Color.blue, Color.blue,
                  Color.orange, Color.orange, Color.orange,
                  Color.orange, Color.orange, Color.orange},
                { Color.white, Color.white, Color.white,
                  Color.white, Color.white, Color.white,
                  Color.white, Color.white, Color.white}
            };

            Assert.AreEqual(expectedState, cube.State);
        }

        [Test]
        public void Uprime_Test()
        {
            var cube = new rubikmemr.Cube().Up();

            var expectedState = new Color[,] {
                { Color.yellow, Color.yellow, Color.yellow,
                  Color.yellow, Color.yellow, Color.yellow,
                  Color.yellow, Color.yellow, Color.yellow},
                { Color.orange, Color.orange, Color.orange,
                  Color.blue, Color.blue, Color.blue,
                  Color.blue, Color.blue, Color.blue},
                { Color.blue, Color.blue, Color.blue,
                  Color.red, Color.red, Color.red,
                  Color.red, Color.red, Color.red},
                { Color.red, Color.red, Color.red,
                  Color.green, Color.green, Color.green,
                  Color.green, Color.green, Color.green},
                { Color.green, Color.green, Color.green,
                  Color.orange, Color.orange, Color.orange,
                  Color.orange, Color.orange, Color.orange},
                { Color.white, Color.white, Color.white,
                  Color.white, Color.white, Color.white,
                  Color.white, Color.white, Color.white}
            };


            Assert.AreEqual(expectedState, cube.State);
        }


        [Test]
        public void U_Uprime_Default_Test()
        {
            var defaultCube = new Cube();

            Assert.AreEqual(defaultCube.State, defaultCube.U().Up().State);
            Assert.AreEqual(defaultCube.State, defaultCube.Up().U().State);
        }


        [Test]
        public void U2_UU_Test()
        {
            var cube1 = new Cube().U2();
            var cube2 = new Cube().U().U();

            Assert.AreEqual(cube1.State, cube2.State);
        }

        #endregion

        #region "R moves"

        [Test]
        public void R_Test()
        {
            var cube = new rubikmemr.Cube().R();

            var expectedState = new Color[,] {
                { Color.yellow, Color.yellow, Color.red,
                  Color.yellow, Color.yellow, Color.red,
                  Color.yellow, Color.yellow, Color.red},
                { Color.blue, Color.blue, Color.blue,
                  Color.blue, Color.blue, Color.blue,
                  Color.blue, Color.blue, Color.blue},
                { Color.red, Color.red, Color.white,
                  Color.red, Color.red, Color.white,
                  Color.red, Color.red, Color.white},
                { Color.green, Color.green, Color.green,
                  Color.green, Color.green, Color.green,
                  Color.green, Color.green, Color.green},
                { Color.yellow, Color.orange, Color.orange,
                  Color.yellow, Color.orange, Color.orange,
                  Color.yellow, Color.orange, Color.orange},
                { Color.white, Color.white, Color.orange,
                  Color.white, Color.white, Color.orange,
                  Color.white, Color.white, Color.orange}
            };

            Assert.AreEqual(expectedState, cube.State);
        }

        [Test]
        public void Rprime_Test()
        {
            var cube = new rubikmemr.Cube().Rp();


            var expectedState = new Color[,] {
                { Color.yellow, Color.yellow, Color.orange,
                  Color.yellow, Color.yellow, Color.orange,
                  Color.yellow, Color.yellow, Color.orange},
                { Color.blue, Color.blue, Color.blue,
                  Color.blue, Color.blue, Color.blue,
                  Color.blue, Color.blue, Color.blue},
                { Color.red, Color.red, Color.yellow,
                  Color.red, Color.red, Color.yellow,
                  Color.red, Color.red, Color.yellow},
                { Color.green, Color.green, Color.green,
                  Color.green, Color.green, Color.green,
                  Color.green, Color.green, Color.green},
                { Color.white, Color.orange, Color.orange,
                  Color.white, Color.orange, Color.orange,
                  Color.white, Color.orange, Color.orange},
                { Color.white, Color.white, Color.red,
                  Color.white, Color.white, Color.red,
                  Color.white, Color.white, Color.red}
            };


            Assert.AreEqual(expectedState, cube.State);
        }


        [Test]
        public void R_Rprime_Default_Test()
        {
            var defaultCube = new Cube();

            Assert.AreEqual(defaultCube.State, defaultCube.R().Rp().State);
            Assert.AreEqual(defaultCube.State, defaultCube.Rp().R().State);
        }

        #endregion

        #region "B moves"

        [Test]
        public void B_Test()
        {
            var cube = new rubikmemr.Cube().B();

            var expectedState = new Color[,] {
                { Color.green, Color.green, Color.green,
                  Color.yellow, Color.yellow, Color.yellow,
                  Color.yellow, Color.yellow, Color.yellow},
                { Color.yellow, Color.blue, Color.blue,
                  Color.yellow, Color.blue, Color.blue,
                  Color.yellow, Color.blue, Color.blue},
                { Color.red, Color.red, Color.red,
                  Color.red, Color.red, Color.red,
                  Color.red, Color.red, Color.red},
                { Color.green, Color.green, Color.white,
                  Color.green, Color.green, Color.white,
                  Color.green, Color.green, Color.white},
                { Color.orange, Color.orange, Color.orange,
                  Color.orange, Color.orange, Color.orange,
                  Color.orange, Color.orange, Color.orange},
                { Color.white, Color.white, Color.white,
                  Color.white, Color.white, Color.white,
                  Color.blue, Color.blue, Color.blue}
            };

            Assert.AreEqual(expectedState, cube.State);
        }

        [Test]
        public void Bprime_Test()
        {
            var cube = new rubikmemr.Cube().Bp();

            var expectedState = new Color[,] {
                { Color.blue, Color.blue, Color.blue,
                  Color.yellow, Color.yellow, Color.yellow,
                  Color.yellow, Color.yellow, Color.yellow},
                { Color.white, Color.blue, Color.blue,
                  Color.white, Color.blue, Color.blue,
                  Color.white, Color.blue, Color.blue},
                { Color.red, Color.red, Color.red,
                  Color.red, Color.red, Color.red,
                  Color.red, Color.red, Color.red},
                { Color.green, Color.green, Color.yellow,
                  Color.green, Color.green, Color.yellow,
                  Color.green, Color.green, Color.yellow},
                { Color.orange, Color.orange, Color.orange,
                  Color.orange, Color.orange, Color.orange,
                  Color.orange, Color.orange, Color.orange},
                { Color.white, Color.white, Color.white,
                  Color.white, Color.white, Color.white,
                  Color.green, Color.green, Color.green}
            };

            Assert.AreEqual(expectedState, cube.State);
        }


        [Test]
        public void B_Bprime_Default_Test()
        {
            var defaultCube = new Cube();

            Assert.AreEqual(defaultCube.State, defaultCube.B().Bp().State);
            Assert.AreEqual(defaultCube.State, defaultCube.Bp().B().State);
        }

        #endregion

        #region "D moves"

        [Test]
        public void D_Test()
        {
            var cube = new rubikmemr.Cube().D();

            var expectedState = new Color[,] {
                { Color.yellow, Color.yellow, Color.yellow,
                  Color.yellow, Color.yellow, Color.yellow,
                  Color.yellow, Color.yellow, Color.yellow},
                { Color.blue, Color.blue, Color.blue,
                  Color.blue, Color.blue, Color.blue,
                  Color.orange, Color.orange, Color.orange},
                { Color.red, Color.red, Color.red,
                  Color.red, Color.red, Color.red,
                  Color.blue, Color.blue, Color.blue},
                { Color.green, Color.green, Color.green,
                  Color.green, Color.green, Color.green,
                  Color.red, Color.red, Color.red},
                { Color.orange, Color.orange, Color.orange,
                  Color.orange, Color.orange, Color.orange,
                  Color.green, Color.green, Color.green},
                { Color.white, Color.white, Color.white,
                  Color.white, Color.white, Color.white,
                  Color.white, Color.white, Color.white}
            };

            Assert.AreEqual(expectedState, cube.State);
        }

        [Test]
        public void Dprime_Test()
        {
            var cube = new rubikmemr.Cube().Dp();

            var expectedState = new Color[,] {
                { Color.yellow, Color.yellow, Color.yellow,
                  Color.yellow, Color.yellow, Color.yellow,
                  Color.yellow, Color.yellow, Color.yellow},
                { Color.blue, Color.blue, Color.blue,
                  Color.blue, Color.blue, Color.blue,
                  Color.red, Color.red, Color.red},
                { Color.red, Color.red, Color.red,
                  Color.red, Color.red, Color.red,
                  Color.green, Color.green, Color.green},
                { Color.green, Color.green, Color.green,
                  Color.green, Color.green, Color.green,
                  Color.orange, Color.orange, Color.orange},
                { Color.orange, Color.orange, Color.orange,
                  Color.orange, Color.orange, Color.orange,
                  Color.blue, Color.blue, Color.blue},
                { Color.white, Color.white, Color.white,
                  Color.white, Color.white, Color.white,
                  Color.white, Color.white, Color.white}
            };

            Assert.AreEqual(expectedState, cube.State);
        }


        [Test]
        public void D_Dprime_Default_Test()
        {
            var defaultCube = new Cube();

            Assert.AreEqual(defaultCube.State, defaultCube.D().Dp().State);
            Assert.AreEqual(defaultCube.State, defaultCube.Dp().D().State);
        }

        #endregion

        #region "L moves"

        [Test]
        public void L_Test()
        {
            var cube = new rubikmemr.Cube().L();

            var expectedState = new Color[,] {
                { Color.orange, Color.yellow, Color.yellow,
                  Color.orange, Color.yellow, Color.yellow,
                  Color.orange, Color.yellow, Color.yellow},
                { Color.blue, Color.blue, Color.blue,
                  Color.blue, Color.blue, Color.blue,
                  Color.blue, Color.blue, Color.blue},
                { Color.yellow, Color.red, Color.red,
                  Color.yellow, Color.red, Color.red,
                  Color.yellow, Color.red, Color.red},
                { Color.green, Color.green, Color.green,
                  Color.green, Color.green, Color.green,
                  Color.green, Color.green, Color.green},
                { Color.orange, Color.orange, Color.white,
                  Color.orange, Color.orange, Color.white,
                  Color.orange, Color.orange, Color.white},
                { Color.red, Color.white, Color.white,
                  Color.red, Color.white, Color.white,
                  Color.red, Color.white, Color.white}
            };

            Assert.AreEqual(expectedState, cube.State);
        }

        [Test]
        public void Lprime_Test()
        {
            var cube = new rubikmemr.Cube().Lp();

            var expectedState = new Color[,] {
                { Color.red, Color.yellow, Color.yellow,
                  Color.red, Color.yellow, Color.yellow,
                  Color.red, Color.yellow, Color.yellow},
                { Color.blue, Color.blue, Color.blue,
                  Color.blue, Color.blue, Color.blue,
                  Color.blue, Color.blue, Color.blue},
                { Color.white, Color.red, Color.red,
                  Color.white, Color.red, Color.red,
                  Color.white, Color.red, Color.red},
                { Color.green, Color.green, Color.green,
                  Color.green, Color.green, Color.green,
                  Color.green, Color.green, Color.green},
                { Color.orange, Color.orange, Color.yellow,
                  Color.orange, Color.orange, Color.yellow,
                  Color.orange, Color.orange, Color.yellow},
                { Color.orange, Color.white, Color.white,
                  Color.orange, Color.white, Color.white,
                  Color.orange, Color.white, Color.white}
            };

            Assert.AreEqual(expectedState, cube.State);
        }


        [Test]
        public void L_Lprime_Default_Test()
        {
            var defaultCube = new Cube();

            Assert.AreEqual(defaultCube.State, defaultCube.L().Lp().State);
            Assert.AreEqual(defaultCube.State, defaultCube.Lp().L().State);
        }

        #endregion

        #region "composite moves"

        [Test]
        public void Superflip_Test()
        {
            var cube = new rubikmemr.Cube().
                U().R().R().F().B().R().B().B().R().
                U().U().L().B().B().R().Up().Dp().R().
                R().F().Rp().L().B().B().U().U().F().F();

            var expectedState = new Color[,] {
                { Color.yellow, Color.orange, Color.yellow,
                  Color.blue, Color.yellow, Color.green,
                  Color.yellow, Color.red, Color.yellow},
                { Color.blue, Color.yellow, Color.blue,
                  Color.orange, Color.blue, Color.red,
                  Color.blue, Color.white, Color.blue},
                { Color.red, Color.yellow, Color.red,
                  Color.blue, Color.red, Color.green,
                  Color.red, Color.white, Color.red},
                { Color.green, Color.yellow, Color.green,
                  Color.red, Color.green, Color.orange,
                  Color.green, Color.white, Color.green},
                { Color.orange, Color.yellow, Color.orange,
                  Color.green, Color.orange, Color.blue,
                  Color.orange, Color.white, Color.orange},
                { Color.white, Color.red, Color.white,
                  Color.blue, Color.white, Color.green,
                  Color.white, Color.orange, Color.white}
            };

            Assert.AreEqual(expectedState, cube.State);
        }

        [Test]
        public void R_U_Dp_Test()
        {
            var index = 0;
            var cube = new rubikmemr.Cube().R();
            index += 1;
            cube.OutputBitmap(index);
            cube.U();
            index += 1;
            cube.OutputBitmap(index);
            cube.Dp();
            index += 1;
            cube.OutputBitmap(index);

            var expectedState = new Color[,] {
                { Color.yellow, Color.yellow, Color.yellow,
                  Color.yellow, Color.yellow, Color.yellow,
                  Color.red, Color.red, Color.red},
                { Color.red, Color.red, Color.white,
                  Color.blue, Color.blue, Color.blue,
                  Color.red, Color.red, Color.white},
                { Color.green, Color.green, Color.green,
                  Color.red, Color.red, Color.white,
                  Color.green, Color.green, Color.green},
                { Color.yellow, Color.orange, Color.orange,
                  Color.green, Color.green, Color.green,
                  Color.yellow, Color.orange, Color.orange},
                { Color.blue, Color.blue, Color.blue,
                  Color.yellow, Color.orange, Color.orange,
                  Color.blue, Color.blue, Color.blue},
                { Color.orange, Color.orange, Color.orange,
                  Color.white, Color.white, Color.white,
                  Color.white, Color.white, Color.white}
            };

            Assert.AreEqual(expectedState, cube.State);
        }

        #endregion

        #region "letters"

        [Test]
        public void Letter_A_Test()
        {
            var cube = new rubikmemr.Cube();

            Edge sideA = cube.LetterToEdge("A");
            Assert.AreEqual((Face.Up, 1), sideA.position1);
            Assert.AreEqual(Color.yellow, sideA.color1);
            Assert.AreEqual(Color.orange, sideA.color2);
        }
        #endregion

        #region "corners"
        [Test]
        public void IsCornerSolved_DefaultCube_Test()
        {
            var cube = new rubikmemr.Cube();

            Assert.IsTrue(cube.IsCornerSolved(cube.LetterToCorner("A")));
            Assert.IsTrue(cube.IsCornerSolved(cube.LetterToCorner("B")));
            Assert.IsTrue(cube.IsCornerSolved(cube.LetterToCorner("C")));
            Assert.IsTrue(cube.IsCornerSolved(cube.LetterToCorner("D")));

            Assert.IsTrue(cube.IsCornerSolved(cube.LetterToCorner("E")));
            Assert.IsTrue(cube.IsCornerSolved(cube.LetterToCorner("F")));
            Assert.IsTrue(cube.IsCornerSolved(cube.LetterToCorner("G")));
            Assert.IsTrue(cube.IsCornerSolved(cube.LetterToCorner("H")));

            Assert.IsTrue(cube.IsCornerSolved(cube.LetterToCorner("I")));
            Assert.IsTrue(cube.IsCornerSolved(cube.LetterToCorner("J")));
            Assert.IsTrue(cube.IsCornerSolved(cube.LetterToCorner("K")));
            Assert.IsTrue(cube.IsCornerSolved(cube.LetterToCorner("L")));

            Assert.IsTrue(cube.IsCornerSolved(cube.LetterToCorner("M")));
            Assert.IsTrue(cube.IsCornerSolved(cube.LetterToCorner("N")));
            Assert.IsTrue(cube.IsCornerSolved(cube.LetterToCorner("O")));
            Assert.IsTrue(cube.IsCornerSolved(cube.LetterToCorner("P")));

            Assert.IsTrue(cube.IsCornerSolved(cube.LetterToCorner("Q")));
            Assert.IsTrue(cube.IsCornerSolved(cube.LetterToCorner("R")));
            Assert.IsTrue(cube.IsCornerSolved(cube.LetterToCorner("S")));
            Assert.IsTrue(cube.IsCornerSolved(cube.LetterToCorner("T")));

            Assert.IsTrue(cube.IsCornerSolved(cube.LetterToCorner("U")));
            Assert.IsTrue(cube.IsCornerSolved(cube.LetterToCorner("V")));
            Assert.IsTrue(cube.IsCornerSolved(cube.LetterToCorner("W")));
            Assert.IsTrue(cube.IsCornerSolved(cube.LetterToCorner("X")));

        }

        #endregion
    }
}