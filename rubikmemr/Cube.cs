using System.Drawing;

namespace rubikmemr
{

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

        public Cube(Color[,] state)
        {
            this.State = state;
        }

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
            State[(int)Face.Up, 2] = t1;
            State[(int)Face.Up, 1] = t2;
            State[(int)Face.Up, 0] = t3;

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
            State[(int)Face.Up, 0] = t1;
            State[(int)Face.Up, 1] = t2;
            State[(int)Face.Up, 2] = t3;

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
            State[(int)Face.Up, 0] = State[(int)Face.Back, 2];
            State[(int)Face.Up, 3] = State[(int)Face.Back, 5];
            State[(int)Face.Up, 6] = State[(int)Face.Back, 8];

            // back
            State[(int)Face.Back, 2] = State[(int)Face.Down, 6];
            State[(int)Face.Back, 5] = State[(int)Face.Down, 3];
            State[(int)Face.Back, 8] = State[(int)Face.Down, 0];

            // down
            State[(int)Face.Down, 6] = State[(int)Face.Front, 0];
            State[(int)Face.Down, 3] = State[(int)Face.Front, 3];
            State[(int)Face.Down, 0] = State[(int)Face.Front, 6];

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

        public Image GetBitmap()
        {
            Image image = new Bitmap(2000, 1024);

            Graphics graph = Graphics.FromImage(image);

            graph.Clear(System.Drawing.Color.Azure);

            Pen pen = new Pen(Brushes.Black);

            graph.DrawLines(pen, new Point[] { new Point(10, 10), new Point(800, 900) });

            Rectangle rect = new Rectangle(100, 100, 300, 300);
            graph.DrawRectangle(pen, rect);

            image.Save("myImage.png", System.Drawing.Imaging.ImageFormat.Png);

            return image;
        }

        #endregion
    }
}
