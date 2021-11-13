using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    class Menu
    {
        public static int menu(List<string> menuItems, int startLine, string battleshipsBanner)
        {
            //Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(battleshipsBanner);
            Console.ForegroundColor = ConsoleColor.White;
            int item = 0;
            Console.CursorVisible = false;
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (i == item)
                {
                    Console.SetCursorPosition(0, startLine);
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(menuItems[i]);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(menuItems[i]);
                }
                Console.ResetColor();
            }
            while (true)
            {
                ConsoleKeyInfo ckey = Console.ReadKey();
                if (ckey.Key == ConsoleKey.DownArrow)
                {
                    if (item == menuItems.Count - 1)
                    {
                        item = 0;
                        Console.SetCursorPosition(0, startLine);
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(menuItems[item]);
                        Console.SetCursorPosition(0, startLine + menuItems.Count - 1);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(menuItems[menuItems.Count - 1]);
                    }
                    else
                    {
                        Console.SetCursorPosition(0, startLine + item + 1);
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(menuItems[item + 1]);
                        Console.SetCursorPosition(0, startLine + item);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(menuItems[item]);
                        item++;
                    }
                }
                else if (ckey.Key == ConsoleKey.UpArrow)
                {
                    if (item == 0)
                    {
                        Console.SetCursorPosition(0, startLine);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(menuItems[item]);
                        Console.SetCursorPosition(0, startLine + menuItems.Count - 1);
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(menuItems[menuItems.Count - 1]);
                        item = menuItems.Count - 1;
                    }
                    else
                    {
                        Console.SetCursorPosition(0, startLine + item);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(menuItems[item]);
                        Console.SetCursorPosition(0, startLine + item - 1);
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(menuItems[item - 1]);
                        item--;
                    }
                }
                else if (ckey.Key == ConsoleKey.Enter)
                {
                    Console.ResetColor();
                    return item;
                }
            }
        }
        public static void startGame()
        {
            Console.Clear();
            Board myBoard = new Board();
            Board enemyBoard = new Board();
            myBoard.boardStartX = 0;
            myBoard.boardStartY = 1;
            enemyBoard.boardStartX = 50;
            enemyBoard.boardStartY = 1;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(0, 0);
            Console.Write("Your Board");
            Console.SetCursorPosition(50, 0);
            Console.Write("Enemy Board");
            myBoard.printBoard(myBoard.boardStartX, myBoard.boardStartY);
            enemyBoard.printBoard(enemyBoard.boardStartX, enemyBoard.boardStartY);
            Menu.addShipsType(myBoard, enemyBoard, 14);
            //myBoard.printBoard(myBoard.boardStartX, myBoard.boardStartY);
            Menu.battle(myBoard, enemyBoard);
        }
        public static void addShips(Board myBoard, Board enemyBoard)
        {
            Console.CursorVisible = true;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(0, 12);
            Console.Write("Add your battleships - format = Shiptype-StartingPosition-direction (eg. submarine-A5-right)");
            Console.SetCursorPosition(50, 14);
            Console.Write("Ships you need to add:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(50, 15);
            Console.Write("Carrier\t1");
            Console.SetCursorPosition(50, 16);
            Console.Write("Battleship\t1");
            Console.SetCursorPosition(50, 17);
            Console.Write("Cruiser\t1");
            Console.SetCursorPosition(50, 18);
            Console.Write("Submarine\t2");
            Console.SetCursorPosition(50, 19);
            Console.Write("Destroyer\t3");
            string[] command;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            //myBoard.autoAddShips(); //delete before noraml game
            while (myBoard.isBoardFull() == false)
            {
                Console.SetCursorPosition(0, 14);
                Console.Write("                                  ");
                Console.SetCursorPosition(0, 14);
                command = Console.ReadLine().Split("-");
                if (command.Length != 3)
                {
                    Console.SetCursorPosition(0, 16);
                    Console.Write("                                                  ");
                    Console.SetCursorPosition(0, 16);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("Wrong number of arguments, please try again");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    continue;
                }
                else
                {
                    Console.SetCursorPosition(0, 16);
                    Console.Write("                                                  ");
                }
                char[] temp = command[1].ToUpper().ToCharArray();
                int y = (int)temp[1] - 48;
                int x = (int)temp[0] - 65;
                if (!(0 <= x && x <= 9 && 0 <= y && y <= 9))
                {
                    Console.SetCursorPosition(0, 16);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("Position is out of bounds, please try again");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    continue;
                }
                //Console.WriteLine("x=" + x);
                //Console.WriteLine("y=" + y);
                switch (command[0].ToLower())
                {
                    case "carrier":
                        if (myBoard.car >= 1)
                        {
                            Console.SetCursorPosition(0, 16);
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("Can't add more Carriers");
                            break;
                        }
                        if (myBoard.addShip(5, x, y, command[2].ToLower()) == 0)
                        {
                            //myBoard.car++;
                            myBoard.printBoard(0, 1);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.SetCursorPosition(0, 16);
                            Console.Write("Carrier successfully added!");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.SetCursorPosition(50, 15);
                            Console.Write("Carrier\t0");
                        }
                        else
                        {
                            Console.SetCursorPosition(0, 16);
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("Could not add Carrier");
                        }
                        break;
                    case "battleship":
                        if (myBoard.bat >= 1)
                        {
                            Console.SetCursorPosition(0, 16);
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("Can't add more Battleships");
                            break;
                        }
                        if (myBoard.addShip(4, x, y, command[2].ToLower()) == 0)
                        {
                            //myBoard.bat++;
                            myBoard.printBoard(0, 1);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.SetCursorPosition(0, 16);
                            Console.Write("Battleship successfully added!");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.SetCursorPosition(50, 16);
                            Console.Write("Battleship\t0");
                        }
                        else
                        {
                            Console.SetCursorPosition(0, 16);
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("Could not add Battleship");
                        }
                        break;
                    case "cruiser":
                        if (myBoard.cru >= 1)
                        {
                            Console.SetCursorPosition(0, 16);
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("Can't add more Cruisers");
                            break;
                        }
                        if (myBoard.addShip(3, x, y, command[2].ToLower()) == 0)
                        {
                            //myBoard.cru++;
                            myBoard.printBoard(0, 1);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.SetCursorPosition(0, 16);
                            Console.Write("Cruiser successfully added!");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.SetCursorPosition(50, 17);
                            Console.Write("Cruiser\t0");
                        }
                        else
                        {
                            Console.SetCursorPosition(0, 16);
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("Could not add Cruiser");
                        }
                        break;
                    case "submarine":
                        if (myBoard.sub >= 2)
                        {
                            Console.SetCursorPosition(0, 16);
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("Can't add more Submarines");
                            break;
                        }
                        if (myBoard.addShip(2, x, y, command[2].ToLower()) == 0)
                        {
                            //myBoard.sub++;
                            myBoard.printBoard(0, 1);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.SetCursorPosition(0, 16);
                            Console.Write("Submarine successfully added!");
                            if (myBoard.sub == 2)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.SetCursorPosition(50, 18);
                                Console.Write("Submarine\t0");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.SetCursorPosition(50, 18);
                                Console.Write("Submarine\t1");
                            }
                        }
                        else
                        {
                            Console.SetCursorPosition(0, 16);
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("Could not add Submarine");
                        }
                        break;
                    case "destroyer":
                        if (myBoard.des >= 3)
                        {
                            Console.SetCursorPosition(0, 16);
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("Can't add more Destroyers");
                            break;
                        }
                        if (myBoard.addShip(1, x, y, command[2].ToLower()) == 0)
                        {
                            //myBoard.des++;
                            myBoard.printBoard(0, 1);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.SetCursorPosition(0, 16);
                            Console.Write("Destroyer successfully added!");
                            if (myBoard.des == 3)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.SetCursorPosition(50, 19);
                                Console.Write("Destroyer\t0");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.SetCursorPosition(50, 19);
                                Console.Write("Destroyer\t" + (3 - myBoard.des));
                            }
                        }
                        else
                        {
                            Console.SetCursorPosition(0, 16);
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("Could not add Destroyer");
                        }
                        break;
                    default:
                        Console.SetCursorPosition(0, 16);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Wrong ship name, please try again");
                        break;
                }
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                if (myBoard.isBoardFull())
                {
                    Console.SetCursorPosition(0, 17);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("All ships added, press Enter to continue");
                    while (!(Console.ReadKey().Key == ConsoleKey.Enter)) { }
                    Console.SetCursorPosition(0, 12);
                    for (int i = 0; i < 6; i++)
                    {
                        Console.WriteLine("                                                                                                                      ");
                    }
                }
            }
            enemyBoard.autoAddShips();
            enemyBoard.changeEnemyMarking();
            Console.CursorVisible = false;
        }
        public static void battle(Board myBoard, Board enemyBoard)
        {
            Random rnd = new Random();
            Console.CursorVisible = true;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(35, 1);
            Console.Write("Your ships:");
            Console.SetCursorPosition(85, 1);
            Console.Write("Enemy ships:");
            myBoard.writeShipsAmount(35, 1);
            enemyBoard.writeShipsAmount(85, 1);
            Console.SetCursorPosition(0, 12);
            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine("                                                                                                          ");
            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(enemyBoard.boardStartX, 12);
            Console.Write("Choose field you want to shot (eg. A5)");
            while (!myBoard.isBoardEmpty() && !enemyBoard.isBoardEmpty())
            {
                Console.SetCursorPosition(enemyBoard.boardStartX, 13);
                Console.Write("         ");
                Console.SetCursorPosition(0, 14);
                Console.Write("                                                                    ");
                Console.SetCursorPosition(enemyBoard.boardStartX, 13);
                char[] temp = Console.ReadLine().ToUpper().ToCharArray();
                if (temp.Length == 2)
                {
                    int y = (int)temp[1] - 47;
                    int x = (int)temp[0] - 64;
                    if (!(1 <= x && x <= 10 && 1 <= y && y <= 10))
                    {
                        Console.SetCursorPosition(0, 15);
                        Console.Write("                                                                                                      ");
                        Console.SetCursorPosition(50, 15);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Position is out of bounds, please try again");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        continue;
                    }
                    enemyBoard.shot(x, y, 0);
                    enemyBoard.printBoard(50, 1);
                    while (myBoard.shot(rnd.Next(10) + 1, rnd.Next(10) + 1, 1)) { }
                    myBoard.printBoard(0, 1);
                }
                else
                {
                    Console.SetCursorPosition(enemyBoard.boardStartX, 14);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Field out of bounds");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(0, 13);
            Console.Write("               ");
            Console.SetCursorPosition(0, 14);
            Console.Write("Game ended - press any key to continue                               ");
            Console.ReadKey();
            if (myBoard.isBoardEmpty())
                Menu.battleLost();
            else
                Menu.battleWon();
        }
        public static void info()
        {
            Console.Clear();
            Console.SetWindowSize(130, 25);
            string inf = @"The game is played on four grids, two for each player. 
The grids are typically square – usually 10×10 – and the individual squares in the grid are identified by letter and number. 
On one grid the player arranges ships and records the shots by the opponent. On the other grid the player records their own shots.

Before play begins, each player secretly arranges their ships on their primary grid. 
Each ship occupies a number of consecutive squares on the grid, arranged either horizontally or vertically. 
The number of squares for each ship is determined by the type of the ship. 
The ships cannot overlap (i.e., only one ship can occupy any given square in the grid). 
The types and numbers of ships allowed are the same for each player.

No.	Class of ship	Size    Amount
1	Carrier         5       1
2	Battleship      4       1
3	Cruiser	        3       1
4	Submarine       2       2
5	Destroyer       1       3

Press Enter To Continue";
            Console.Write(inf);
        }
        public static void battleLost()
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetWindowSize(83, 22);
            for(int i = 0; i < 40; i++)
            {
                if (i % 20 < 10)
                    Console.SetCursorPosition(0, i % 20);
                else
                    Console.SetCursorPosition(0, 20 - i % 20);
                Console.Write(@"
                                                                                   
▄██   ▄    ▄██████▄  ███    █▄        ▄█        ▄██████▄     ▄████████     ███     
███   ██▄ ███    ███ ███    ███      ███       ███    ███   ███    ███ ▀█████████▄ 
███▄▄▄███ ███    ███ ███    ███      ███       ███    ███   ███    █▀     ▀███▀▀██ 
▀▀▀▀▀▀███ ███    ███ ███    ███      ███       ███    ███   ███            ███   ▀ 
▄██   ███ ███    ███ ███    ███      ███       ███    ███ ▀███████████     ███     
███   ███ ███    ███ ███    ███      ███       ███    ███          ███     ███     
███   ███ ███    ███ ███    ███      ████    ▄ ███    ███    ▄█    ███     ███     
 ▀█████▀   ▀██████▀  ████████▀       █████▄▄██  ▀██████▀   ▄████████▀     ▄████▀   
                                     ▀                                             
                                                                                   ");
                System.Threading.Thread.Sleep(100);
            }
            Console.ReadKey();
        }
        public static void battleWon()
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetWindowSize(70, 22);
            for (int i = 0; i < 40; i++)
            {
                if (i % 20 < 10)
                    Console.SetCursorPosition(0, i % 20);
                else
                    Console.SetCursorPosition(0, 20 - i % 20);
                Console.Write(@"
                                                                       
▄██   ▄    ▄██████▄  ███    █▄        ▄█     █▄   ▄██████▄  ███▄▄▄▄   
███   ██▄ ███    ███ ███    ███      ███     ███ ███    ███ ███▀▀▀██▄ 
███▄▄▄███ ███    ███ ███    ███      ███     ███ ███    ███ ███   ███ 
▀▀▀▀▀▀███ ███    ███ ███    ███      ███     ███ ███    ███ ███   ███ 
▄██   ███ ███    ███ ███    ███      ███     ███ ███    ███ ███   ███ 
███   ███ ███    ███ ███    ███      ███     ███ ███    ███ ███   ███ 
███   ███ ███    ███ ███    ███      ███ ▄█▄ ███ ███    ███ ███   ███ 
 ▀█████▀   ▀██████▀  ████████▀        ▀███▀███▀   ▀██████▀   ▀█   █▀  
                                                                      ");
                System.Threading.Thread.Sleep(100);
            }
            Console.ReadKey();
        }
        public static void addShipsType(Board myBoard, Board enemyBoard, int menuStartLine)
        {
            List<string> addShipsTypeMenuItems = new List<string>() { "Carrier", "Battleship", "Cruiser", "Submarine", "Destroyer" };
            Console.CursorVisible = true;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(0, 12);
            Console.Write("Add your battleships                             ");
            myBoard.writeShipsToAdd(50, 14);
            //myBoard.autoAddShips();
            while (!myBoard.isBoardFull())
            {
                Console.SetWindowSize(100, 25);
                switch (Menu.menu(addShipsTypeMenuItems, menuStartLine, ""))
                {
                    case 0:
                        if (myBoard.car == 1)
                        {
                            Console.SetCursorPosition(0, menuStartLine + 6);
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("Can't add more Carriers       ");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            break;
                        }
                        Menu.addShipsPosition(myBoard, Carrier.size, menuStartLine);
                        break;
                    case 1:
                        if (myBoard.bat == 1)
                        {
                            Console.SetCursorPosition(0, menuStartLine + 6);
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("Can't add more Battleships       ");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            break;
                        }
                        Menu.addShipsPosition(myBoard, Battleship.size, menuStartLine);
                        break;
                    case 2:
                        if (myBoard.cru == 1)
                        {
                            Console.SetCursorPosition(0, menuStartLine + 6);
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("Can't add more Cruisers       ");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            break;
                        }
                        Menu.addShipsPosition(myBoard, Cruiser.size, menuStartLine);
                        break;
                    case 3:
                        if (myBoard.sub == 2)
                        {
                            Console.SetCursorPosition(0, menuStartLine + 6);
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("Can't add more Submarines       ");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            break;
                        }
                        Menu.addShipsPosition(myBoard, Submarine.size, menuStartLine);
                        break;
                    case 4:
                        if (myBoard.des == 3)
                        {
                            Console.SetCursorPosition(0, menuStartLine + 6);
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("Can't add more Destroyers       ");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            break;
                        }
                        Menu.addShipsPosition(myBoard, Destroyer.size, menuStartLine);
                        break;
                    default:
                        Console.WriteLine("Error while selecting menuItem");
                        break;
                }
                myBoard.printBoard(myBoard.boardStartX, myBoard.boardStartY);
            }
            enemyBoard.autoAddShips();
            enemyBoard.changeEnemyMarking();
            myBoard.printBoard(myBoard.boardStartX, myBoard.boardStartY);
        }
        public static void addShipsPosition(Board myBoard, int shipLength, int menuStartLine)
        {
            int y;
            int x;
            Menu.clearLines(menuStartLine, 7);
            Console.SetCursorPosition(0, 12);
            Console.CursorVisible = true;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("What position do you want your ship to start (eg. A5)           ");
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.SetCursorPosition(0, menuStartLine);
                Console.Write("              ");
                Console.SetCursorPosition(0, menuStartLine);
                char[] position = Console.ReadLine().ToUpper().ToCharArray();
                y = (int)position[1] - 48;
                x = (int)position[0] - 65;
                if (!(0 <= x && x <= 9 && 0 <= y && y <= 9))
                {
                    Console.SetCursorPosition(0, menuStartLine + 6);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("Position is out of bounds, please try again");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                }
                else
                    break;
            }
            if (shipLength == 1)
            {
                if (myBoard.addShip(shipLength, x, y, "up") != 0)
                {
                    Console.SetCursorPosition(0, menuStartLine + 6);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("Could not add Ship, place already occupied        ");
                }
                else
                    myBoard.writeShipsToAdd(50, 14);
            }
            else
                Menu.addShipsDirection(myBoard, shipLength, menuStartLine, x, y);
        }
        public static void addShipsDirection(Board myBoard, int shipLength, int menuStartLine, int x, int y)
        {
            List<string> addShipsDirectionMenuItems = new List<string>() { "Up", "Down", "Left", "Right" };
            int errorMessage = 0;
            Menu.clearLines(menuStartLine, 7);
            Console.SetCursorPosition(0, 12);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("What direction do you want your ship to face                  ");
            switch (Menu.menu(addShipsDirectionMenuItems, menuStartLine, ""))
            {
                case 0:
                    errorMessage = myBoard.addShip(shipLength, x, y, addShipsDirectionMenuItems[0].ToLower());
                    break;
                case 1:
                    errorMessage = myBoard.addShip(shipLength, x, y, addShipsDirectionMenuItems[1].ToLower());
                    break;
                case 2:
                    errorMessage = myBoard.addShip(shipLength, x, y, addShipsDirectionMenuItems[2].ToLower());
                    break;
                case 3:
                    errorMessage = myBoard.addShip(shipLength, x, y, addShipsDirectionMenuItems[3].ToLower());
                    break;
                default:
                    Console.WriteLine("Error while selecting menuItem");
                    break;
            }
            if (errorMessage == 1)
            {
                Console.SetCursorPosition(0, menuStartLine + 6);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("Could not add Ship, place already occupied        ");
            }
            else
                myBoard.writeShipsToAdd(50, 14);
        }
        public static void clearLines(int startLine, int amountOfLines)
        {
            for (int i = 0; i < amountOfLines; i++)
            {
                Console.SetCursorPosition(0, startLine + i);
                Console.Write("                                              ");
            }
        }
    }
}
