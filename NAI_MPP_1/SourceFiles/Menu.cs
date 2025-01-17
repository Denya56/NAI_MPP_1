﻿using System;

namespace NAI_MPP_1.SourceFiles
{
    class Menu
    {
        public string currentState { get; set; }
        public string KNNlearnDataFile { get; set; } = "Data/iris.data";
        public string KNNtestDataFile { get; set; } = "Data/iris.test.data";
        public string PerclearnDataFile { get; set; } = "Data/perceptron.data";
        public string PerctestDataFile { get; set; } = "Data/perceptron.test.data";
        public string NaivelearnDataFile { get; set; } = "Data/agaricus-lepiota.test.data";
        public string NaivetestDataFile { get; set; } = "Data/agaricus-lepiota.data";
        public Menu()
        {
            //currentState = "mainMenu";
        }

        private void RunKNN(int option)
        {
            Console.WriteLine("Enter k: ");
            int k = int.Parse(Console.ReadLine());
            KNN_AI ai = new KNN_AI(k, KNNlearnDataFile, KNNtestDataFile);
            ai.Run(option);


            /*int countCorrectAnswers = 0;
            for (int i = 0; i < ai.results.Count; i++)
            {
                if (ai.results[i].Equals(ai.testAnswers[i]))
                    countCorrectAnswers++;
                Console.WriteLine(ai.results[i] + "\t" + ai.testAnswers[i]);
            }
            Console.WriteLine(countCorrectAnswers / ai.results.Count * 100 + "%");*/
        }

        private void RunPerceptron(int option)
        {
            /*Console.WriteLine("Enter alpha");
            double alpha = double.Parse(Console.ReadLine());*/
            Perceptron p = new Perceptron(PerclearnDataFile, PerctestDataFile, 1, 0.01, 2);
            p.Run(option);
        }

        private void RunNaive()
        {
            /*Console.WriteLine("Enter alpha");
            double alpha = double.Parse(Console.ReadLine());*/
            NaiveBayes np = new NaiveBayes(NaivelearnDataFile, NaivetestDataFile);
            
            np.Run();
            foreach (var item in np.result)
            {
                Console.WriteLine(item);
            }
        }

        public void DisplayMenu(int menuState)
        {
            switch (menuState)
            {
                case 0:
                    Console.WriteLine("1. KNN\n" +
                                  "2. Perceptron\n" +
                                  "3. Naive\n" +
                                  "4. Exit");
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
                        StartMenuNaive();
                        break;
                    case 4:
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
            for (int choice = -1; choice != 0;)
            {
                choice = GetChoice(1);

                if (choice == 1 || choice == 2)
                {
                    RunKNN(choice);
                    EndMenu(1);
                }
                else
                    Console.WriteLine("Invalid input");
            }
        }

        public void StartMenuPerceptron()
        {
            Console.Clear();
            for (int choice = -1; choice != 0;)
            {
                choice = GetChoice(1);

                if (choice == 1 || choice == 2)
                {
                    RunPerceptron(choice);
                    EndMenu(2);
                }
                else
                    Console.WriteLine("Invalid input");
            }
        }
        public void StartMenuNaive()
        {
            Console.Clear();
            
            RunNaive();
            EndMenu(2);

        }

        public void EndMenu(int option)
        {
            for (int choice = -1; choice != 0;)
            {
                choice = GetChoice(2);

                switch (choice)
                {
                    case 1:
                        if (option == 1) StartMenuKNN();
                        else StartMenuPerceptron();
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
