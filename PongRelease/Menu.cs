﻿using Menu;

using static System.Console;

namespace KeyboardMenu
{

    class Game
    {
        class Program
        {
            static void Main(string[] args)
            {
                Console.CursorVisible = false;
                Game myGame = new Game();
                myGame.Start();


            }
        }
        class Menu
        {
            private int SelectedIndex;
            private string[] Options;
            private string WriteText;

            public Menu(string Title, string[] options)
            {

                WriteText = Title;
                Options = options;
                SelectedIndex = 0;
            }


            public void DisplayOptions()
            {

                WriteLine(WriteText);
                for (int i = 0; i < Options.Length; i++)
                {
                    string currentOption = Options[i];
                    string prefix;


                    if (i == SelectedIndex)
                    {
                        prefix = " *";
                        ForegroundColor = ConsoleColor.Black;
                        BackgroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        prefix = "  ";
                        ForegroundColor = ConsoleColor.White;
                        BackgroundColor = ConsoleColor.Black;
                    }

                    WriteLine($"{prefix} << {currentOption} >>\n");

                }
                ResetColor();
            }
            public int Run()
            {



                ConsoleKey keyPressed;
                do
                {
                    Clear();
                    DisplayOptions();

                    ConsoleKeyInfo keyInfo = ReadKey(true);
                    keyPressed = keyInfo.Key;

                    if (keyPressed == ConsoleKey.UpArrow)
                    {
                        SelectedIndex--;
                        if (SelectedIndex == -1)
                        {
                            SelectedIndex = Options.Length - 1;
                        }
                    }
                    else if (keyPressed == ConsoleKey.DownArrow)
                    {
                        SelectedIndex++;
                        if (SelectedIndex == Options.Length)
                        {
                            SelectedIndex = 0;
                        }
                    }

                } while (keyPressed != ConsoleKey.Enter);

                return SelectedIndex;

            }
        }
        public void Start()
        {

            RunMainMenu();

        }
        private void RunMainMenu()
        {
            string Title = @"

██████╗ ██╗███╗   ██╗ ██████╗     ██████╗  ██████╗ ███╗   ██╗ ██████╗      ██████╗  █████╗ ███╗   ███╗███████╗
██╔══██╗██║████╗  ██║██╔════╝     ██╔══██╗██╔═══██╗████╗  ██║██╔════╝     ██╔════╝ ██╔══██╗████╗ ████║██╔════╝
██████╔╝██║██╔██╗ ██║██║  ███╗    ██████╔╝██║   ██║██╔██╗ ██║██║  ███╗    ██║  ███╗███████║██╔████╔██║█████╗  
██╔═══╝ ██║██║╚██╗██║██║   ██║    ██╔═══╝ ██║   ██║██║╚██╗██║██║   ██║    ██║   ██║██╔══██║██║╚██╔╝██║██╔══╝  
██║     ██║██║ ╚████║╚██████╔╝    ██║     ╚██████╔╝██║ ╚████║╚██████╔╝    ╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗
╚═╝     ╚═╝╚═╝  ╚═══╝ ╚═════╝     ╚═╝      ╚═════╝ ╚═╝  ╚═══╝ ╚═════╝      ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝
                ";

            string[] options = { "Play", "About", "Match History", "How 2 Play", "Exit" };
            Menu mainmenu = new Menu(Title, options);
            int selectedindex = mainmenu.Run();


            switch (selectedindex)
            {
                case 0:
                    PlayGame();
                    break;
                case 1:
                    DisplayAboutInfo();
                    break;
                case 2:
                    MatchHistory();
                    break;
                case 3:
                    HowtoPlay();
                    break;
                case 4:
                    ExitGame();
                    break;

            }

        }

        private void PlayGame()
        {
            Clear();
            Session session = new Session(PlayerName(out string Input, 1), PlayerName(out string Input2, 2));
            session.Start();
            using (ApplicationContext db = new())
            {
                db.Database.EnsureCreated();
                Player LeftPlayer = new Player { Name = Input, Points = session.LeftPlayer.Points };
                Player RightPlayer = new Player { Name = Input2, Points = session.RightPlayer.Points };
                db.Players.AddRange(LeftPlayer, RightPlayer);
                db.SaveChanges();
            }
        }
        public string PlayerName(out string Input, int number)
        {
            Clear();
            Console.WriteLine($"Введите имя игрока N{number}");
            Input = Console.ReadLine();
            if (Input == null)
            {
                Input = "DefaultPlayer1";
                return ($"{Input}");
            }
            Clear();
            return Input;

        }
        /*public string PlayerName2(out string Input)
        {
            Clear();
            Console.WriteLine("Введите имя первого игрока");
            Input = Console.ReadLine();
            if (Input == null)
            {
                Input = "DefaultPlayer2";
                return ($"{Input}");
            }
            Clear();
            return Input;
            
        }*/

        private void DisplayAboutInfo()
        {

            Clear();
            WriteLine(@"
 ██████╗ ██╗   ██╗████████╗███████╗██████╗     ██╗  ██╗███████╗ █████╗ ██╗   ██╗███████╗███╗   ██╗
██╔═══██╗██║   ██║╚══██╔══╝██╔════╝██╔══██╗    ██║  ██║██╔════╝██╔══██╗██║   ██║██╔════╝████╗  ██║
██║   ██║██║   ██║   ██║   █████╗  ██████╔╝    ███████║█████╗  ███████║██║   ██║█████╗  ██╔██╗ ██║
██║   ██║██║   ██║   ██║   ██╔══╝  ██╔══██╗    ██╔══██║██╔══╝  ██╔══██║╚██╗ ██╔╝██╔══╝  ██║╚██╗██║
╚██████╔╝╚██████╔╝   ██║   ███████╗██║  ██║    ██║  ██║███████╗██║  ██║ ╚████╔╝ ███████╗██║ ╚████║
 ╚═════╝  ╚═════╝    ╚═╝   ╚══════╝╚═╝  ╚═╝    ╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝  ╚═══╝  ╚══════╝╚═╝  ╚═══╝                                             

                                  
Directed by:

Milushev Timur

Nekrasov Vladislav

Arseniy Mazurenko

Borisov Ivan

");
            ReadKey(true);

            RunMainMenu();

        }

        private void MatchHistory()
        {
            Clear();
            using (ApplicationContext db = new())
            {
                var Players = db.Players.ToList();
                foreach (var item in Players)
                {

                    Console.WriteLine($"Name : {item.Name}, Points : {item.Points}");


                }
            }
            Console.ReadKey();
            RunMainMenu();
            //Вводить данные из БД
        }
        private void HowtoPlay()
        {
            Clear();
            Console.WriteLine(@"

Игровой процесс игры Пинг Понг состоит в том, что игроки передвигают свои ракетки вертикально для защиты своих ворот. 
В начале каждого раунда мячик подаётся одному из игроков, и раунд продолжается до тех пор,
пока один из игроков не заработает очко. 
Это происходит тогда, когда его противник не может отбить мячик.

          Для манипуляции ракетками используются следующие клавиши:

Игрок 1:                                Игрок 2:
W - Переместить ракетку вверх           UpArrow - Переместить ракетку вверх
S - Переместить ракетку вниз            DownArrow - Переместить ракетку вниз

Для дополнительной информации перейдите по ссылке:

https://www.youtube.com/watch?v=dQw4w9WgXcQ

");
            ReadKey(true);

            RunMainMenu();
        }

        private void ExitGame()
        {
            Environment.Exit(0);
        }
    }
}


