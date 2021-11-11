using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    class Board
    {
        char[,] ocean = new char[11, 11];
        string[,] ships = new string[11, 11];
        List<Ship> shipList = new List<Ship>();
        public int car = 0;
        public int bat = 0;
        public int cru = 0;
        public int sub = 0;
        public int des = 0;

        public int boardStartX;
        public int boardStartY;

        public Board()
        {
            ocean[0, 0] = ' ';
            char x = 'A';
            for (int i = 1; i < 11; i++)
            {
                ocean[i, 0] = x++;
                ocean[0, i] = (char)(48 + i - 1);
            }
            for (int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    ocean[i, j] = '~';
                }
            }
        }
        public void printBoard(int consolePositionX, int consolePositionY)
        {
            for (int i = 0; i < 11; i++)
            {
                Console.SetCursorPosition(consolePositionX, consolePositionY + i);
                for (int j = 0; j < 11; j++)
                {
                    if (i > 0 && j > 0)
                    {
                        if (ocean[i, j] == '~' || ocean[i, j] == 'e')
                            Console.ForegroundColor = ConsoleColor.Blue; //water
                        else if (ocean[i, j] == 'O')
                            Console.ForegroundColor = ConsoleColor.Green; //ship not hit
                        else if (ocean[i, j] == '*')
                            Console.ForegroundColor = ConsoleColor.Red; //ship hit/destroyed
                        else if (ocean[i, j] == 'X')
                            Console.ForegroundColor = ConsoleColor.Gray; //miss
                    }
                    else
                        Console.ForegroundColor = ConsoleColor.DarkYellow; //border
                    char temp;
                    if (ocean[i, j] == 'e')
                        temp = '~';
                    else
                        temp = ocean[i, j];
                    Console.Write("[" +  temp + "]");
                }
            }
            Console.ResetColor();
        }
        public char getWhatOnPoint(int x, int y)
        {
            if (!(0 <= x && x <= 9 && 0 <= y && y <= 9))
                return 'E';
            return ocean[x + 1, y + 1];
        }
        int setOnPoint(int x, int y, char pointType)
        {
            if (!(0 <= x && x <= 9 && 0 <= y && y <= 9))
                return 1;
            this.ocean[x + 1, y + 1] = pointType;
            return 0;
        }
        public int addShip(int shipLength, int x, int y, string direction)
        {
            string shipName;
            switch (shipLength)
            {
                case 1:
                    shipName = "des" + (des + 1);
                    break;
                case 2:
                    shipName = "sub" + (sub + 1);
                    break;
                case 3:
                    shipName = "cru" + (cru + 1);
                    break;
                case 4:
                    shipName = "bat" + (bat + 1);
                    break;
                case 5:
                    shipName = "car" + (car + 1);
                    break;
                default:
                    shipName = null;
                    break;
            }
            switch (direction)
            {
                case "up":
                    for (int i = 0; i < shipLength; i++)
                    {
                        if (getWhatOnPoint(x, y - i) != '~')
                            return 1;
                    }
                    for (int i = 0; i < shipLength; i++)
                    {
                        if (setOnPoint(x, y - i, 'O') == 1)
                            Environment.Exit(100);
                        ships[x + 1, y - i + 1] = shipName;
                    }
                    break;
                case "down":
                    for (int i = 0; i < shipLength; i++)
                    {
                        if (getWhatOnPoint(x, y + i) != '~')
                            return 1;
                    }
                    for (int i = 0; i < shipLength; i++)
                    {
                        if (setOnPoint(x, y + i, 'O') == 1)
                            Environment.Exit(100);
                        ships[x + 1, y + i + 1] = shipName;
                    }
                    break;
                case "left":
                    for (int i = 0; i < shipLength; i++)
                    {
                        if (getWhatOnPoint(x - i, y) != '~')
                            return 1;
                    }
                    for (int i = 0; i < shipLength; i++)
                    {
                        if (setOnPoint(x - i, y, 'O') == 1)
                            Environment.Exit(100);
                        ships[x - i + 1, y + 1] = shipName;
                    }
                    break;
                case "right":
                    for (int i = 0; i < shipLength; i++)
                    {
                        if (getWhatOnPoint(x + i, y) != '~')
                            return 1;
                    }
                    for (int i = 0; i < shipLength; i++)
                    {
                        if (setOnPoint(x + i, y, 'O') == 1)
                            Environment.Exit(100);
                        ships[x + i + 1, y + 1] = shipName;
                    }
                    break;
                default:
                    return 1;
            }
            Ship newShip = new Ship();
            newShip.name = shipName;
            newShip.leftToDestroy = shipLength;
            shipList.Add(newShip);
            switch (shipLength)
            {
                case 1:
                    des++;
                    break;
                case 2:
                    sub++;
                    break;
                case 3:
                    cru++;
                    break;
                case 4:
                    bat++;
                    break;
                case 5:
                    car++;
                    break;
                default:
                    break;
            }
            return 0;
        }
        public Boolean isBoardFull()
        {
            if (car == 1 && bat == 1 && cru == 1 && sub == 2 && des == 3)
                return true;
            return false;
        }
        public Boolean isBoardEmpty()
        {
            if (car == 0 && bat == 0 && cru == 0 && sub == 0 && des == 0)
                return true;
            return false;
        }
        public void writeShipsAmount(int consolePositionX, int consolePositionY)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(consolePositionX, consolePositionY + 1);
            if (car == 0)
                Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Carrier\t" + car);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(consolePositionX, consolePositionY + 2);
            if (bat == 0)
                Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Battleship\t" + bat);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(consolePositionX, consolePositionY + 3);
            if (cru == 0)
                Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Cruiser\t" + cru);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(consolePositionX, consolePositionY + 4);
            if (sub == 0)
                Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Submarine\t" + sub);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(consolePositionX, consolePositionY + 5);
            if (des == 0)
                Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Destroyer\t" + des);
            Console.ResetColor();
        }
        public void autoAddShips()
        {
            //Carrier
            Random rnd = new Random();
            //Carrier
            while (this.addShip(5, rnd.Next(10), rnd.Next(10), randDirection(rnd)) == 1) { }
            //Battleship
            while (this.addShip(4, rnd.Next(10), rnd.Next(10), randDirection(rnd)) == 1) { }
            //Cruiser
            while (this.addShip(3, rnd.Next(10), rnd.Next(10), randDirection(rnd)) == 1) { }
            //Submarine
            while (this.addShip(2, rnd.Next(10), rnd.Next(10), randDirection(rnd)) == 1) { }
            while (this.addShip(2, rnd.Next(10), rnd.Next(10), randDirection(rnd)) == 1) { }
            //Destroyer
            while (this.addShip(1, rnd.Next(10), rnd.Next(10), randDirection(rnd)) == 1) { }
            while (this.addShip(1, rnd.Next(10), rnd.Next(10), randDirection(rnd)) == 1) { }
            while (this.addShip(1, rnd.Next(10), rnd.Next(10), randDirection(rnd)) == 1) { }
        }
        public string randDirection(Random rnd)
        {
            switch (rnd.Next(4))
            {
                case 0:
                    return "up";
                case 1:
                    return "down";
                case 3:
                    return "right";
                case 4:
                    return "left";
            }
            return null;
        }
        public void changeEnemyMarking()
        {
            for (int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    if (ocean[i, j] == 'O')
                        ocean[i, j] = 'e';
                }
            }
        }
        public Boolean shot(int x, int y, int whichBoard)
        {
            if(ocean[x, y] == 'e' || ocean[x, y] == 'O')
            {
                ocean[x, y] = '*';
                shipList.Where(i => i.name == ships[x, y]).FirstOrDefault().leftToDestroy--;
                if (shipList.Where(i => i.name == ships[x, y]).FirstOrDefault().leftToDestroy == 0)
                {
                    switch (ships[x, y].Substring(0, 3))
                    {
                        case "des":
                            des--;
                            break;
                        case "sub":
                            sub--;
                            break;
                        case "cru":
                            cru--;
                            break;
                        case "bat":
                            bat--;
                            break;
                        case "car":
                            car--;
                            break;
                        default:
                            Environment.Exit(99);
                            break;
                    }
                    writeShipsAmount(boardStartX + 35, boardStartY);
                }
                if (whichBoard == 0)
                {
                    Console.SetCursorPosition(0, 15);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Hit at: " + ((char)(x + 64)) + ((char)(y + 47)) + "!                           ");
                    return false;
                }
                else
                {
                    Console.SetCursorPosition(0, 16);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Enemy hit at: " + ((char)(x + 64)) + ((char)(y + 47)) + "!                           ");
                    return false;
                }
            }
            else if (ocean[x, y] == 'X' || ocean[x, y] == '*')
            {
                if (whichBoard == 0)
                {
                    Console.SetCursorPosition(0, 15);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Miss at: " + ((char)(x + 64)) + ((char)(y + 47)) + "!  You already shot at this field     ");
                    return false;
                }
                return true;
            }
            else if (ocean[x, y] == '~')
            {
                
                if (ocean[x, y] == '~')
                    ocean[x, y] = 'X';
                if (whichBoard == 0)
                {
                    Console.SetCursorPosition(0, 15);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Missed at: " + ((char)(x + 64)) + ((char)(y + 47)) + "!                      ");
                }
                else
                {
                    Console.SetCursorPosition(0, 16);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Enemy missed at: " + ((char)(x + 64)) + ((char)(y + 47)) + "!                           ");
                }
                return false;
            }
            return true;
        }
    }
}
