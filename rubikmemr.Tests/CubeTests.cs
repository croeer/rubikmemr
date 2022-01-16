using NUnit.Framework;
using rubikmemr;

namespace rubikmemr.Tests
{
    public class CubeTests
    {
        [SetUp]
        public void Setup()
        {
        }

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
          
            Assert.AreEqual(defaultCube.State,defaultCube.F().Fp().State);
            Assert.AreEqual(defaultCube.State,defaultCube.Fp().F().State);
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

    }
}