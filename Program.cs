using System;
using System.Collections.Generic;

namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            string battleshipsBanner = @"
                                                        
   (            )    )  (              )                
 ( )\     )  ( /( ( /(  )\   (      ( /( (              
 )((_) ( /(  )\()))\())((_) ))\ (   )\()))\  `  )   (   
((_)_  )(_))(_))/(_))/  _  /((_))\ ((_)\((_) /(/(   )\  
 | _ )((_)_ | |_ | |_  | |(_)) ((_)| |(_)(_)((_)_\ ((_) 
 | _ \/ _` ||  _||  _| | |/ -_)(_-<| ' \ | || '_ \)(_-< 
 |___/\__,_| \__| \__| |_|\___|/__/|_||_||_|| .__/ /__/ 
                                            |_|         ";
            List<string> startingMenuItems = new List<string>() { "Start", "Info", "Exit" };
            while(true)
            {
                Console.SetWindowSize(100, 25);
                Console.Clear();
                switch (Menu.menu(startingMenuItems, 11, battleshipsBanner))
                {
                    case 0:
                        Menu.startGame();
                        break;
                    case 1:
                        Menu.info();
                        while (!(Console.ReadKey().Key == ConsoleKey.Enter)) { }
                        Console.Clear();
                        break;
                    case 2:
                        return;
                    default:
                        Console.WriteLine("Error while selecting menuItem");
                        break;
                }
            }
        }
    }
}
