﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NAI_MPP_1.SourceFiles
{
    class Menu
    {
        public string currentState { get; set; }
        public string learnDataFile { get; set; } = "Data/iris.data";
        public string testDataFile { get; set; } = "Data/iris.test.data";
        public Menu()
        {
            //currentState = "mainMenu";
        }

        private void RunKNN(int option)
        {
            Console.WriteLine("Enter k: ");
            int k = int.Parse(Console.ReadLine());
            KNN_AI ai = new KNN_AI(k, learnDataFile, testDataFile);

            switch (option)
            {
                case 1:
                    ai.Run(1);
                    for (int i = 0; i < ai.results.Count; i++)
                    {
                        Console.WriteLine(ai.results[i] + "\t" + ai.testAnswers[i]);
                    }
                    break;

                case 2:
                    ai.Run(2);
                    break;
            }

            /*for (int i = 0; i < ai.results.Count; i++)
            {
                Console.WriteLine(ai.results[i]);
            }*/
        }

        private void RunPerceptron()
        {
            Console.WriteLine("Enter bias");
            int bias = int.Parse(Console.ReadLine());
            Perceptron p = new Perceptron(learnDataFile, testDataFile, bias, 1);
            p.Run();
        }

        public void DisplayMenu(int menuState)
        {
            switch (menuState)
            {
                case 0:
                    Console.WriteLine("1. Start\n" +
                                  "2. Perceptron\n" +
                                  "3. Exit");
                    break;
                case 1:
                    Console.WriteLine("1. Run test file\n" +
                                      "2. Enter vector manually");
                    break;
                case 2:
                    Console.WriteLine("1. Run again\n" +
                                      "2. Go back to main menu");
                    break;
            }
        }

        private int GetChoice(int menuState)
        {
            DisplayMenu(menuState);
            string choice = Console.ReadLine();
            while (choice.Equals(""))
            {
                Console.WriteLine("Invalid input");
                DisplayMenu(menuState);
            }
            return int.Parse(choice);
        }


        // Unique menus
        public void MainMenu()
        {
            Console.Clear();
            for (int choice = -1; choice != 0;)
            {
                choice = GetChoice(0);

                switch (choice)
                {
                    case 1:
                        StartMenuKNN();
                        break;
                    case 2:
                        StartMenuPerceptron();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
        }

        public void StartMenuKNN()
        {
            Console.Clear();
            for (int choice = -1; choice !=0;)
            {
                choice = GetChoice(1);

                if (choice == 1 || choice == 2)
                {
                    RunKNN(choice);
                    EndMenu();
                }
                else
                    Console.WriteLine("Invalid input");
            }
        }

        public void StartMenuPerceptron()
        {
            Console.Clear();
            RunPerceptron();
        }

        public void EndMenu()
        {
            for (int choice = -1; choice != 0;)
            {
                choice = GetChoice(2);

                switch (choice)
                {
                    case 1:
                        StartMenuKNN();
                        break;
                    case 2:
                        MainMenu();
                        break;
                    default:
                        Console.WriteLine("invalid input");
                        break;
                }
            }
        }
    }
}
