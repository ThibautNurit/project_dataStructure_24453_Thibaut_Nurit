using System;
using System.IO;
using System.Collections.Generic;

namespace project_dataStructure_24453_Thibaut_Nurit
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Nim !");
            Console.WriteLine("Pour faire un choix, il suffit de taper le chiffre affiché devant l'instruction que vous souhaitez !");
            Console.WriteLine("Enjoy :D\n");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Console.Clear();

            LogInMenu(); 
        }

        static void LogInMenu() 
        {
            Console.Title = "Game of Nim";
            int choice = 0;
            do
            {
                Console.WriteLine("GAME OF NIM\n");
                Console.WriteLine("1. Log in");
                Console.WriteLine("2. Sign in");
                Console.WriteLine("3. Exit");
                choice = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
            } while (choice < 1 || choice > 3);

            switch (choice)
            {
                case 1:
                    string pseudo1, pw1;
                    Console.WriteLine("Please, write your pseudo");
                    pseudo1 = Console.ReadLine();
                    Console.WriteLine("Please, write your password");
                    pw1 = Console.ReadLine();
                    SearchIDJ1("id.txt", pseudo1, pw1);
                    break;
                case 2:
                    AddPlayer1("id.txt");
                    break;
                case 3:
                    Console.WriteLine("See you soon !");
                    break;
                default:
                    LogInMenu();
                    break;
            }
        }

        static void SearchIDJ1(string fileName, string pseudo, string pw) 
        {
            try
            {
                bool cond = false;
                StreamReader sr = new StreamReader(fileName);
                string line = "";
                while (sr.EndOfStream == false)
                {
                    line = sr.ReadLine();
                    string[] temp = line.Split(',');
                    if (temp[0] == pseudo && temp[1] == pw)
                    {
                        cond = true;
                    }
                }
                sr.Close();
                if (cond == true)
                {
                    Console.Clear();
                    gameMenu(pseudo);
                }
                if (cond == false)
                {
                    Console.WriteLine("Error, pseudo or password wrong");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                    LogInMenu();
                }
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                LogInMenu();
            }
        }

        static void AddPlayer1(string fileName) 
        {
            string pseudo = ""; string pw = ""; int nbWins = 0; int nbGames = 0;
            Console.Write("Pseudo: ");
            pseudo = Console.ReadLine();
            Console.Write("Password : ");
            pw = Console.ReadLine();
            try
            {
                StreamWriter sw = new StreamWriter(fileName, true);
                sw.WriteLine(pseudo + "," + pw + "," + nbWins + "," + nbGames);
                sw.Close();
            }
            catch
            {
                Console.WriteLine("No account registered, create one to log in");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                Console.Clear();
                LogInMenu();
            }
            Console.WriteLine("Account registered, you can now log in !");
            LogInMenu();
        }


        static void gameMenu(string pseudo1)
        {
            int choice;
            do
            {
                choice = SelectChoiceGameMenu();
                switch (choice)
                {
                    case 1:
                        Play(pseudo1);
                        break;
                    case 2:
                        Rules();
                        break;
                    case 3:
                        Console.Clear();
                        top5Victories("id.txt");
                        break;
                    case 4:
                        Console.WriteLine("See you soon !");
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            } while (choice < 4);

        }

        static int SelectChoiceGameMenu() 
        {
            int n = 0;

            while (n < 1 || n > 4)
            {
                Console.WriteLine("GAME OF NIM\n");
                Console.WriteLine("1. PLay");
                Console.WriteLine("2. The rules");
                Console.WriteLine("3. Top 5 players");
                Console.WriteLine("4. Leave");
                do
                {
                    Console.WriteLine("Please select an action");
                    n = Convert.ToInt32(Console.ReadLine());
                } while (n < 1 || n > 4);              
            }
            return n;
        }

        static void Rules()  
        {
            Console.Clear();
            Console.WriteLine("Nim is a two player game\n");
            Console.WriteLine("Players have a number of matches. The players, one after another, have to remove 1 to 3 matches\n");
            Console.WriteLine("The player that takes the last match is the winner\n");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Console.Clear();
        }

        static void top5Victories(string fileName)
        {
            int[] array = null;
            string[] pseudoPlayers = null;
            Console.WriteLine("\nHere are the 5 players with the most victories !\n");
            try
            {
                StreamReader sr1 = new StreamReader(fileName);
                int length = 0;
                string line = "";
                while (sr1.EndOfStream == false)
                {
                    line = sr1.ReadLine();
                    string[] temp = line.Split(',');
                    length++;
                }
                sr1.Close();

                pseudoPlayers = new string[length];
                array = new int[length];


                int i = 0;
                StreamReader sr2 = new StreamReader(fileName);

                while (sr2.EndOfStream == false)
                {
                    line = sr2.ReadLine();
                    string[] temp = line.Split(',');
                    pseudoPlayers[i] = temp[0];
                    array[i] = Convert.ToInt32(temp[2]);
                    i++;
                }
                sr2.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            int[] tempo = new int[array.Length];
            for (int i = 0; i < tempo.Length; i++)
            {
                tempo[i] = array[i];
            }
            array = SelectionSort(array);

            if (5 > tempo.Length)
            {
                int[] tab2 = new int[tempo.Length];
                for (int i = 0; i < tempo.Length; i++)
                {
                    tab2[i] = array[i];
                }
                for (int i = tab2[0]; i >= tab2[tempo.Length - 1]; i--)
                {
                    for (int j = 0; j < tempo.Length; j++)
                    {
                        if (i == tempo[j])
                        {
                            Console.WriteLine(pseudoPlayers[j] + " : " + i);
                        }
                    }
                }
            }
            else
            {
                int[] tab2 = new int[5];
                for (int i = 0; i < 5; i++)
                {
                    tab2[i] = array[i];
                }
                for (int i = tab2[0]; i >= tab2[4]; i--)
                {
                    for (int j = 0; j < tempo.Length; j++)
                    {
                        if (i == tempo[j])
                        {
                            Console.WriteLine(pseudoPlayers[j] + " : " + i);
                        }
                    }
                }
            }

            Console.WriteLine();
        }

        static int[] SelectionSort(int[] tab) 
        {
            if (tab != null)
            {
                for (int i = 0; i < tab.Length - 1; i++)
                {
                    int MaxVal = tab[i];
                    int MaxInd = i;
                    for (int j = i + 1; j < tab.Length; j++)
                    {
                        if (MaxVal < tab[j])
                        {
                            MaxVal = tab[j];
                            MaxInd = j;
                        }
                    }
                    if (i != MaxInd)
                    {
                        int inter = tab[i];
                        tab[i] = tab[MaxInd];
                        tab[MaxInd] = inter;
                    }
                }
            }

            return tab;
        }

        static void Play(string pseudo1)
        {
            Console.Clear();
            Console.WriteLine("Who do you want to play with ?\n");
            Console.WriteLine("1. Other player");
            Console.WriteLine("2. Computer");
            int choice = 0;
            choice = PlayChoice();
            switch (choice)
            {
                case 1:
                    Console.Clear();
                    PlayWithJ2(pseudo1);
                    break;

                case 2:
                    Console.Clear();
                    ChoiceIAMode(pseudo1);
                    break;
                default:
                    Console.WriteLine("Wrong input");
                    break;
            }
        }

        static int PlayChoice() 
        {
            int n = 0;

            while (n < 1 || n > 2)
            {
                Console.WriteLine();
                n = Convert.ToInt32(Console.ReadLine());

                if (n < 1 || n > 2)
                {
                    Console.WriteLine("Error. Please write 1 or 2");
                }
            }
            return n;
        }

        static void ChoiceIAMode(string pseudo)  
        {
            int choice;

            Console.WriteLine("What mode do you want to play with ?\n");
            Console.WriteLine("1. Easy IA");
            Console.WriteLine("2. Normal IA");
            Console.WriteLine("3. Hard IA");
            Console.WriteLine("4. No IA.");
            choice = SelectMode();
            switch (choice)
            {
                case 1:
                    PlayComputer(4, pseudo);
                    break;
                case 2:
                    PlayComputer(2, pseudo);
                    break;

                case 3:
                    PlayComputer(1, pseudo);
                    break;
                case 4:
                    PlayComputer(0, pseudo);
                    break;
            }
        }

        static int SelectMode() 
        {
            int choice = 0;
            do
            {
                Console.WriteLine("Please, enter your choice");
                choice = Convert.ToInt32(Console.ReadLine());
            } while (choice < 1 || choice > 4);

            return choice;
        }

        static void PlayComputer(int mode, string pseudo)  
        {
            Console.Clear();
            int choice = 0;
            Console.WriteLine("How do you want to remove the matches\n1. By group\n2. Freely");  // ex : 1 2 3 4 5 / by group I remove 2 and one next to 2, freely, I can remove 2 and 5
            choice = PlayChoice();
            Console.Clear();
            int n;
            int countIA = 0;
            n = SelectNbMatches();
            string[] b1 = null;
            string[] b2 = null;
            CreationMatches(n, ref b1, ref b2);
            Console.Clear();
            int nbMatchesPlayer1 = 0;
            int nbMatchesComputer = 0;
            Random Alea = new Random();
            string LineToChange = null;
            string LineChanged = null;

            Console.WriteLine("You can remove 1 to 3 matches\n");
            System.Diagnostics.Stopwatch gameTime = new System.Diagnostics.Stopwatch();
            gameTime.Start();
            while (n > 0)
            {

                Console.WriteLine(pseudo + "'s turn\n");
                Console.WriteLine("Matches available : " + n + "\n");
                DisplayMatches(b1, b2);
                nbMatchesPlayer1 = SelectMatches(n);
                n = n - nbMatchesPlayer1;

                if (choice == 1)
                {
                    RemoveMatchesGroup(ref b1, ref b2, nbMatchesPlayer1);
                }
                else
                {
                    for (int i = 1; i <= nbMatchesPlayer1; i++)
                    {
                        RemoveMatchesFreely(ref b1, ref b2);
                    }
                }

                if (n == 0)
                {
                    gameTime.Stop();
                    TimeSpan ts = gameTime.Elapsed;
                    string TempsEcPartie = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                    Console.WriteLine("Duration of the game : " + TempsEcPartie + "\n");
                    Console.WriteLine(pseudo + " wins !\n");
                    AddWin("id.txt", pseudo, ref LineToChange, ref LineChanged);
                    WriteInFile("id.txt", LineToChange, LineChanged);
                    top5Victories("id.txt");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Computer's turn\n");
                    Console.WriteLine("Matches available : " + n + "\n");
                    DisplayMatches(b1, b2);
                    if (mode == 0)
                    {
                        nbMatchesComputer = RandomRemove(n);
                    }
                    else
                    {
                        nbMatchesComputer = Program.nbMatchesComputer(n, ref countIA, mode);
                    }

                    n = n - nbMatchesComputer;
                    Console.WriteLine("Computer removed " + nbMatchesComputer + " matches");
                    for (int i = 1; i <= nbMatchesComputer; i++)
                    {
                        RemoveMatchesComputer(ref b1, ref b2, 0);
                    }

                    if (n == 0)
                    {
                        gameTime.Stop();
                        TimeSpan ts = gameTime.Elapsed;
                        string TempsEcPartie = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                        Console.WriteLine("Duration of the game : " + TempsEcPartie);
                        AddGame("id.txt", pseudo, ref LineToChange, ref LineChanged);
                        WriteInFile("id.txt", LineToChange, LineChanged);
                        Console.WriteLine("Computer wins !");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                Console.Clear();
            }

        }

        static int SelectNbMatches() 
        {
            int nb = 0;
            while (nb <= 0)
            {
                Console.WriteLine("Please enter the number of matches for this game : ");
                nb = Convert.ToInt32(Console.ReadLine());
            }
            return nb;
        }

        static void CreationMatches(int n, ref string[] b1, ref string[] b2)  
        {
            b1 = new string[n];
            for (int i = 0; i < b1.Length; i++)
            {
                b1[i] = "  O  ";

            }
            b2 = new string[n];
            for (int i = 0; i < b2.Length; i++)
            {
                b2[i] = "  |  ";
            }
        }

        static void DisplayMatches(string[] b1, string[] b2)  
        {
            Console.BackgroundColor = ConsoleColor.White;
            string[] bg = new string[b1.Length];
            for (int i = 0; i < bg.Length; i++)
            {
                bg[i] = "     ";
                Console.Write(bg[i]);
            }
            Console.WriteLine();
            for (int i = 0; i < b1.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                if (i < 9)
                {
                    Console.Write("  " + (i + 1) + "  ");
                }
                else
                {
                    Console.Write(" " + (i + 1) + "  ");
                }
            }
            Console.WriteLine();
            for (int i = 0; i < bg.Length; i++)
            {
                Console.Write(bg[i]);
            }
            Console.WriteLine();
            for (int i = 0; i < b1.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(b1[i]);
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            for (int i = 0; i < b2.Length; i++)
            {
                Console.Write(b2[i]);
            }
            Console.WriteLine();
            for (int i = 0; i < b2.Length; i++)
            {
                Console.Write(b2[i]);
            }
            Console.WriteLine();
            for (int i = 0; i < b2.Length; i++)
            {
                Console.Write(b2[i]);
            }
            Console.WriteLine();

            for (int i = 0; i < bg.Length; i++)
            {
                Console.Write(bg[i]);
            }
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine();
        }

        static void RemoveMatchesGroup(ref string[] b1, ref string[] b2, int nb) 
        {
            int x = SelectPosition(b1);
            int choice;
            if (b1[x - 1] != "     ")
            {
                b1[x - 1] = "     ";
                b2[x - 1] = "     ";
                if (nb == 2 || nb == 3)
                {
                    Console.WriteLine("What is the 2nd match that you want to remove ?");
                    int y = x - 1;
                    do
                    {
                        if (y <= 0)
                        {
                            y = b1.Length;
                        }
                        if (b1[y - 1] == "     ")
                        {
                            y = y - 1;
                        }
                        if (y <= 0)
                        {
                            y = b1.Length;
                        }
                    } while (b1[y - 1] == "     ");
                    int z = x + 1;
                    do
                    {
                        if (z > b1.Length)
                        {
                            z = 1;
                        }
                        if (b1[z - 1] == "     ")
                        {
                            z = z + 1;
                        }
                        if (z > b1.Length)
                        {
                            z = 1;
                        }
                    } while (b1[z - 1] == "     ");

                    Console.WriteLine("1. Match " + (y));
                    Console.WriteLine("2. Match " + (z));
                    choice = SelectChoiceMatches();
                    switch (choice)
                    {
                        case 1:
                            RemoveMatches(ref b1, ref b2, y - 1);
                            break;

                        case 2:
                            RemoveMatches(ref b1, ref b2, z - 1);
                            break;
                    }
                    if (nb == 3)
                    {
                        Console.WriteLine();
                        Console.WriteLine("What is the 3rd match that you want to remove ?");
                        if (choice == 1)
                        {
                            y = y - 1;
                            do
                            {
                                if (y <= 0)
                                {
                                    y = b1.Length;
                                }
                                if (b1[y - 1] == "     ")
                                {
                                    y = y - 1;
                                }
                                if (y <= 0)
                                {
                                    y = b1.Length;
                                }
                            } while (b1[y - 1] == "     ");

                            Console.WriteLine("1. Match " + (y));
                            Console.WriteLine("2. Match " + (z));
                            choice = SelectChoiceMatches();

                            switch (choice)
                            {
                                case 1:
                                    RemoveMatches(ref b1, ref b2, y - 1);
                                    break;
                                case 2:
                                    RemoveMatches(ref b1, ref b2, z - 1);
                                    break;
                            }
                        }
                        else
                        {
                            z = z + 1;
                            do
                            {
                                if (z > b1.Length)
                                {
                                    z = 1;
                                }

                                if (b1[z - 1] == "     ")
                                {
                                    z = z + 1;
                                }
                                if (z > b1.Length)
                                {
                                    z = 1;
                                }
                            } while (b1[z - 1] == "     ");

                            Console.WriteLine("1. Match " + (y));
                            Console.WriteLine("2. Match " + (z));
                            choice = SelectChoiceMatches();

                            switch (choice)
                            {
                                case 1:
                                    RemoveMatches(ref b1, ref b2, y - 1);
                                    break;
                                case 2:
                                    RemoveMatches(ref b1, ref b2, z - 1);
                                    break;
                            }
                        }
                    }
                }
            }
            else
            {
                RemoveMatchesGroup(ref b1, ref b2, nb);
            }
        }

        static int SelectPosition(string[] tab) 
        {
            int nb = 0;
            while (nb <= 0 || nb > tab.Length)
            {
                Console.WriteLine("Please, select the position of the match you want to remove : ");
                nb = Convert.ToInt32(Console.ReadLine());
            }
            return nb;
        }

        static int SelectChoiceMatches() 
        {
            int n = 0;

            while (n < 1 || n > 2)
            {
                n = Convert.ToInt32(Console.ReadLine());

                if (n < 1 || n > 2)
                {
                    Console.WriteLine("Error. Please enter 1 or 2");
                }
            }
            return n;
        }

        static void RemoveMatches(ref string[] b1, ref string[] b2, int nb)  
        {
            if (b1[nb] != "     ")
            {
                b1[nb] = "     ";
                b2[nb] = "     ";
            }
        }

        static int SelectMatches(int n) 
        {
            int nb = 0;
            do
            {
                do
                {
                    Console.WriteLine("How many matches do you want to remove ? (1 to 3) ");
                    nb = Convert.ToInt32(Console.ReadLine());
                } while (nb <= 0 || nb >= 4);
            } while (nb > n);
            return nb;
        }

        static void RemoveMatchesFreely(ref string[] b1, ref string[] b2)  
        {
            int position = SelectPosition(b1) - 1;
            if (b1[position] != "     ")
            {
                b1[position] = "     ";
                b2[position] = "     ";
            }
            else
            {
                RemoveMatchesFreely(ref b1, ref b2);
            }
        }

        static void RemoveMatchesComputer(ref string[] b1, ref string[] b2, int y) 
        {
            if (b1[y] == "     ")
            {
                RemoveMatchesComputer(ref b1, ref b2, y + 1);
            }
            else
            {
                b1[y] = "     ";
                b2[y] = "     ";
            }
        }

        static int nbMatchesComputer(int nbMatches, ref int counter, int mode)  
        {
            bool res = false;
            int choice = 1;
            int nb = -1;
            do
            {
                if (counter % mode == 0)
                {
                    if ((nbMatches - choice) % 4 == 0 && res == false)
                    {
                        res = true;
                        nb = choice;
                    }
                    choice = 2;
                    if ((nbMatches - choice) % 4 == 0 && res == false)
                    {
                        res = true;
                        nb = choice;
                    }
                    choice = 3;
                    if ((nbMatches - choice) % 4 == 0 && res == false)
                    {
                        res = true;
                        nb = choice;
                    }
                    if (res == false)
                    {
                        choice = 1;
                        nb = choice;
                    }
                }
                else
                {
                    Random Alea = new Random();
                    nb = Alea.Next(1, 4);
                }
            } while (nb > nbMatches);
            counter++;
            return nb;
        }

        static int RandomRemove(int n)
        {
            int nb = 0;
            do
            {
                Random Alea = new Random();
                nb = Alea.Next(1, 4);
            } while (nb > n);
            return nb;
        }

        static void AddWin(string fileName, string pseudo, ref string LineToChange, ref string LineChanged)  
        {
            try
            {
                string line = "";
                int nbWins = 0;
                int nbGames = 0;
                StreamReader sr = new StreamReader(fileName);
                while (sr.EndOfStream == false)
                {
                    line = sr.ReadLine();
                    string[] temp = line.Split(',');
                    if (temp[0] == pseudo)
                    {
                        LineToChange = line;
                        nbWins = Convert.ToInt32(temp[2]);
                        nbGames = Convert.ToInt32(temp[3]);
                        nbGames = nbGames + 1;
                        nbWins = nbWins + 1;
                        LineChanged = temp[0] + "," + temp[1] + "," + nbWins + "," + nbGames;
                        Console.WriteLine(pseudo + " has " + nbWins + " wins for " + nbGames + " games !");
                    }
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void AddGame(string fileName, string pseudo, ref string LineToChange, ref string LineChanged) 
        {
            try
            {
                string line = "";
                int nbGames = 0;
                StreamReader sr = new StreamReader(fileName);
                while (sr.EndOfStream == false)
                {
                    line = sr.ReadLine();
                    string[] temp = line.Split(',');
                    if (temp[0] == pseudo)
                    {
                        LineToChange = line;
                        nbGames = Convert.ToInt32(temp[3]);
                        nbGames = nbGames + 1;
                        LineChanged = temp[0] + "," + temp[1] + "," + temp[2] + "," + nbGames;
                    }
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void WriteInFile(string fileName, string LineToChange, string LineChanged)  
        {
            string finalText = null;
            StreamReader sr = new StreamReader(fileName);
            string ReadingLine = null;
            while (sr.EndOfStream == false)
            {
                ReadingLine = sr.ReadLine();
                if (sr.EndOfStream == false)
                {
                    if (ReadingLine == LineToChange)
                    {
                        finalText = finalText + LineChanged + "\n";
                    }
                    else
                    {
                        finalText = finalText + ReadingLine + "\n";
                    }
                }
                else
                {
                    if (ReadingLine == LineToChange)
                    {
                        finalText = finalText + LineChanged;
                    }
                    else
                    {
                        finalText = finalText + ReadingLine;
                    }
                }
            }
            sr.Close();
            StreamWriter sw = new StreamWriter(fileName);
            sw.WriteLine(finalText);
            sw.Close();
        }

        static void PlayWithJ2(string pseudo1) 
        {

            Console.WriteLine("Player 2, Please log in\n");
            Console.WriteLine("1. Log in");
            Console.WriteLine("2. Sign in");
            int choice;
            choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine();
                    Console.WriteLine("Please, write your pseudo");
                    string pseudo2 = Console.ReadLine();
                    Console.WriteLine("Please, write your password");
                    string pw2 = Console.ReadLine();
                    SearchIDPlayer2("id.txt", pseudo2, pw2, pseudo1);
                    break;

                case 2:
                    AddPlayer2("id.txt", pseudo1);
                    break;

            }
        }

        static void SearchIDPlayer2(string fileName, string pseudo2, string pw2, string pseudo1) 
        {
            int i = 1;
            try
            {
                StreamReader sr = new StreamReader(fileName);
                string line = "";
                bool cond = true;
                while (sr.EndOfStream == false && i == 1)
                {
                    line = sr.ReadLine();
                    string[] temp = line.Split(',');
                    if (temp[0] == pseudo2 && temp[1] == pw2)
                    {
                        i = 2;
                        cond = true;
                    }
                    else
                    {
                        cond = false;
                    }
                }
                sr.Close();
                if (cond == false)
                {
                    i = 3;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (i == 2)
            {
                Multiplayer(pseudo1, pseudo2);
            }
            if (i == 3)
            {
                Console.Clear();
                Console.WriteLine("Error, wrong pseudo or password\n");

                PlayWithJ2(pseudo1);
            }
        }

        static void AddPlayer2(string fileName, string pseudo1) 
        {
            {
                string pseudo = ""; string pw = ""; int nbWins = 0; int nbGames = 0;
                Console.Write("Pseudo: ");
                pseudo = Console.ReadLine();
                Console.Write("Password : ");
                pw = Console.ReadLine();
                try
                {
                    StreamWriter sw = new StreamWriter(fileName, true);
                    sw.WriteLine(pseudo + "," + pw + "," + nbWins + "," + nbGames);
                    sw.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("Account registered, you can now log in !");
                PlayWithJ2(pseudo1);
            }
        }

        static void Multiplayer(string pseudo1, string pseudo2)  
        {
            Console.Clear();
            int choice = 0;
            Console.WriteLine("How do you want to remove the matches\n1. By group\n2. Freely");
            choice = PlayChoice();
            int n = 0;
            Console.Clear();
            n = SelectNbMatches();
            string[] b1 = null;
            string[] b2 = null;
            CreationMatches(n, ref b1, ref b2);
            Console.Clear();
            int nbMatchesP1 = 0;
            int nbMatchesP2 = 0;
            string lineToChange = null;
            string lineChanged = null;

            Console.WriteLine("You can remove 1 to 3 matches\n");
            System.Diagnostics.Stopwatch gameTime = new System.Diagnostics.Stopwatch();
            gameTime.Start();
            while (n > 0)
            {
                Console.WriteLine(pseudo1 + "'s turn\n");
                Console.WriteLine("Matches available : " + n + "\n");
                DisplayMatches(b1, b2);
                System.Diagnostics.Stopwatch TimeP1 = new System.Diagnostics.Stopwatch();
                TimeP1.Start();
                nbMatchesP1 = SelectMatches(n);
                n = n - nbMatchesP1;
                if (choice == 1)
                {
                    RemoveMatchesGroup(ref b1, ref b2, nbMatchesP1);
                }
                else
                {
                    for (int i = 1; i <= nbMatchesP1; i++)
                    {
                        RemoveMatchesFreely(ref b1, ref b2);
                    }
                }
                TimeP1.Stop();
                TimeSpan ts1 = TimeP1.Elapsed;
                string TimeWP1 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts1.Hours, ts1.Minutes, ts1.Seconds, ts1.Milliseconds / 10);
                Console.WriteLine("Time during player 1 turns : " + TimeWP1 + "\n");
                if (n == 0)
                {
                    gameTime.Stop();
                    TimeSpan ts = gameTime.Elapsed;
                    string TimeWPGame = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                    Console.WriteLine("Duration of the game : " + TimeWPGame);
                    Console.WriteLine(pseudo1 + " wins !");
                    AddWin("id.txt", pseudo1, ref lineToChange, ref lineChanged);
                    WriteInFile("id.txt", lineToChange, lineChanged);
                    AddGame("id.txt", pseudo2, ref lineToChange, ref lineChanged);
                    WriteInFile("id.txt", lineToChange, lineChanged);
                    top5Victories("id.txt");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine(pseudo2 + "'s turn\n");
                    Console.WriteLine("Matches available : " + n + "\n");
                    DisplayMatches(b1, b2);
                    System.Diagnostics.Stopwatch TimeP2 = new System.Diagnostics.Stopwatch();
                    TimeP2.Start();
                    nbMatchesP2 = SelectMatches(n);
                    n = n - nbMatchesP2;
                    if (choice == 1)
                    {
                        RemoveMatchesGroup(ref b1, ref b2, nbMatchesP2);
                    }
                    else
                    {
                        for (int i = 1; i <= nbMatchesP2; i++)
                        {
                            RemoveMatchesFreely(ref b1, ref b2);
                        }
                    }
                    TimeP2.Stop();
                    TimeSpan ts2 = TimeP2.Elapsed;
                    string TimeWPP2 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts2.Hours, ts2.Minutes, ts2.Seconds, ts2.Milliseconds / 10);
                    Console.WriteLine("Time during player 1 turns : " + TimeWPP2);
                    if (n == 0)
                    {
                        gameTime.Stop();
                        TimeSpan ts = gameTime.Elapsed;
                        string TimeWPGame = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                        Console.WriteLine("Duration of the game : " + TimeWPGame + "\n");
                        Console.WriteLine(pseudo2 + " wins !");
                        AddWin("id.txt", pseudo2, ref lineToChange, ref lineChanged);
                        WriteInFile("id.txt", lineToChange, lineChanged);
                        AddGame("id.txt", pseudo1, ref lineToChange, ref lineChanged);
                        WriteInFile("id.txt", lineToChange, lineChanged);
                        top5Victories("id.txt");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                    }
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                Console.Clear();
            }
        }


    }
}
