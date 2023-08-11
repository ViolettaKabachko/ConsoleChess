using ChessUnits;

ChessBoard MainBoard = new();
MainBoard.GenerateBoard();
string letters = "abcdefgh";

while (true)
{
    Console.Clear();
    MainBoard.PrintBoard();
    Console.Write("Chose the unit in format <letter> <number>: ");
    string[] input = Console.ReadLine().Split();

    while (!letters.Contains(input[0]) || !"12345678".Contains(input[1]))
    {
        Console.Write("Chose the unit in format <letter> <number>: ");
        input = Console.ReadLine().Split();
    }

    int x = letters.IndexOf(input[0]) + 1;
    int y = int.Parse(input[1]);
    Console.Clear();
    MainBoard.PrintBoard(x, y);
    Console.Write("Chose a point to move in format <letter> <number>: ");
    string[] coords = Console.ReadLine().Split();

    while (!letters.Contains(coords[0]) && !"12345678".Contains(coords[2]))
    {
        Console.Write("Chose a point to move in format <letter> <number>: ");
        input = Console.ReadLine().Split();
    }

    int new_x = letters.IndexOf(coords[0]) + 1;
    int new_y = int.Parse(coords[1]);
    MainBoard.MakeMove(x, y, new_x, new_y);
}
