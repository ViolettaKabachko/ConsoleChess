using System.Data;
using System.Text;

namespace ChessUnits
{
    public class ChessBoard
    {
        static private string letters = "abcdefgh";
        static private string nums = "12345678";
        private Unit[,] Map = new Unit[8, 8];
        public List<Unit> PawnsToSpawn = new() {
            new Pawn(1, 2),
            new Pawn(2, 2),
            new Pawn(3, 2),
            new Pawn(4, 2),
            new Pawn(5, 2),
            new Pawn(6, 2),
            new Pawn(7, 2),
            new Pawn(8, 2),
            new Knight(2, 1),
            new Knight(7, 1),
            new King(5, 1),
            new Bishop(3, 1),
            new Bishop(6, 1),
            new Rook(1, 1),
            new Rook(8, 1),
            new Queen(4, 1)
        };

        public void GenerateBoard()
        {
            for (int i = 7; i >= 0; i--)
                for (int j = 0; j < 8; j++)
                {
                    Map[i, j] = new Unit(i, j);
                }
            foreach (var pawn in PawnsToSpawn)
                Map[pawn.Y - 1, pawn.X - 1] = pawn;
        }

        public void PrintBoard()
        {
            for (int i = 7; i >= 0; i--)
            {
                Console.Write(nums[i].ToString() + " ");
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(Map[i, j].GetChar() + " ");
                }
                Console.WriteLine();
            }

            Console.Write("  ");
            foreach (char n in letters)
                Console.Write(n.ToString() + ' ');
            Console.WriteLine();
        }

        public void PrintBoard(int x, int y)
        {
            var ThisUnit = Map[y - 1, x - 1];
            for (int i = 7; i >= 0; i--)
            {
                Console.Write(nums[i].ToString() + " ");
                for (int j = 0; j < 8; j++)
                {
                    if (ThisUnit.CanMoveTo(j + 1, i + 1) && Map[i, j].GetChar() == ".")
                        Console.Write("*" + " ");
                    else
                        Console.Write(Map[i, j].GetChar() + " ");
                }
                Console.WriteLine();
            }
            Console.Write("  ");

            foreach (char n in letters)
                Console.Write(n.ToString() + " ");
            Console.WriteLine();
        }

        public void MakeMove(int xStart, int yStart, int xEnd, int yEnd)
        {
            var thisUnit = Map[yStart - 1, xStart - 1];
            if (thisUnit.CanMoveTo(xEnd, yEnd))
            {
                Map[yStart - 1, xStart - 1] = new Unit(xStart, yStart);
                Map[yEnd - 1, xEnd - 1] = thisUnit;
                thisUnit.MoveTo(xEnd, yEnd);
            }
            else
                Console.WriteLine("Wrong move!");
        }
    }

    public class Unit
    {
        public int X, Y;
        static public char UnitChar = '.';

        public Unit(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public virtual bool CanMoveTo(int x, int y)
        {
            return false;
        }

        public void MoveTo(int x, int y)
        {
            X = x;
            Y = y;
        }

        public virtual string GetChar()
        {
            return UnitChar.ToString();
        }
    }

    public class Pawn : Unit
    {
        static public readonly new char UnitChar = 'P';

        public Pawn(int x, int y) : base(x, y)
        {
            this.X = x;
            this.Y = y;
        }

        public override bool CanMoveTo(int x, int y)
        {
            return x == X && y - Y == 1;
        }

        public override string GetChar()
        {
            return UnitChar.ToString();
        }
    }

    public class Knight : Unit
    {
        static public readonly new char UnitChar = 'N';

        public Knight(int x, int y): base(x, y)
        {
            this.X = x;
            this.Y = y;
        }

        public override bool CanMoveTo(int x, int y)
        {
            return Math.Pow(X - x, 2.0) + Math.Pow(Y - y, 2.0) == 5;
        }

        public override string GetChar()
        {
            return UnitChar.ToString();
        }
    }

    public class King : Unit
    {
        static public readonly new char UnitChar = 'K';

        public King(int x, int y) : base(x, y)
        {
            this.X = x;
            this.Y = y;
        }

        public override bool CanMoveTo(int x, int y)
        {
            return (x == X && Math.Abs(y - Y) == 1) || (y == Y && Math.Abs(x - X) == 1) || (Math.Abs(x - X) == 1 && Math.Abs(y - Y) == 1);
        }

        public override string GetChar()
        {
            return UnitChar.ToString();
        }
    }

    public class Bishop : Unit
    {
        static public readonly new char UnitChar = 'B';
        public Bishop(int x, int y): base(x, y)
        {
            this.X = x;
            this.Y = y;
        }

        public override bool CanMoveTo(int x, int y)
        {
            return Math.Abs(x - X) == Math.Abs(y - Y);
        }

        public override string GetChar()
        {
            return UnitChar.ToString();
        }
    }

    public class Rook : Unit
    {
        static public readonly new char UnitChar = 'R';
            public Rook(int x, int y) : base(x, y)
            {
                this.X = x;
                this.Y = y;
            }

            public override bool CanMoveTo(int x, int y)
            {
                return (Y == y && x != X) || (x == X && y != Y);
            }

            public override string GetChar()
            {
                return UnitChar.ToString();
            }
    }

    public class Queen : Unit
    {
        static public readonly new char UnitChar = 'Q';
        public Queen(int x, int y) : base(x, y)
        {
            this.X = x;
            this.Y = y;
        }

        public override bool CanMoveTo(int x, int y)
        {
            return (Y == y && x != X || x == X && y != Y) || (Math.Abs(x - X) == Math.Abs(y - Y)) || (x == X && Math.Abs(y - Y) == 1) || (y == Y && Math.Abs(x - X) == 1) || (Math.Abs(x - X) == 1 && Math.Abs(y - Y) == 1);
        }

        public override string GetChar()
        {
            return UnitChar.ToString();
        }
    }
}
