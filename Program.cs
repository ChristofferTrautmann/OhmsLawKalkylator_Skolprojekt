using System;

namespace ohmslaw_projektarbete
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Välkommen till Ohms Law Kalkylatorn!");
            Console.Write("Tryck på valfri knapp för att börja: ");
            Console.ReadKey(); //Welcome message, waits for user input to start. 
            
            // Declare Variables //
            bool loop = true; // For Main program loop.
            int[] KnownUnits = new int[2]; // Array to hold the chosen Units.
            int[] Prefixes = new int[2]; // Array to hold the chosen prefixes.
            double[] Values = new double[2]; // Array to hold the values for the chosen units.
            double[] Result = new double[4]; // Array that will hold the result.

            //Loop for main Program
            do
            {
                KnownUnits = TwoUnits(); // User gets to choose his two known units.
                Prefixes = GetPrefixes(KnownUnits); // User gets to choose prefixes for his units
                Values = EnterValues(KnownUnits); // User enters the values for the selected units.
                Unit One = new Unit(Prefixes[0], KnownUnits[0], Values[0]);
                Unit Two = new Unit(Prefixes[1], KnownUnits[1], Values[1]);
                var Values_Tuple = Calculate(One, Two); //Calculate the missing two units. (Returns all 4 Units standardized to their basic prefix.)

                // Apply suitable prefixes to all values, and convert them. 
                var Voltage_result = Apply_Prefix(Values_Tuple.Voltage, 1);  //ApplyPrefixes takes two inputs, first is the value, second is the "unit index".
                var Current_result = Apply_Prefix(Values_Tuple.Current, 2);
                var Resistance_result = Apply_Prefix(Values_Tuple.Resistance, 3);
                var Power_result = Apply_Prefix(Values_Tuple.Power, 4);


                // Show result.
                Console.Clear();
                Console.WriteLine("\nResultat: \nSpänning: {0} {1} \nStrömstyrka: {2} {3} \nResistans: {4} {5} \nEffekt: {6} {7}", Math.Round(Voltage_result.Value, 3), Voltage_result.Prefix, Math.Round(Current_result.Value, 3), Current_result.Prefix, Math.Round(Resistance_result.Value, 3), Resistance_result.Prefix, Math.Round(Power_result.Value, 3), Power_result.Prefix);
                Console.WriteLine("\n \n \n \n \nTryck på valfri knapp för att börja om. . .");
                Console.Write("Tryck på Escape för att avsluta programmet . . .");
                if (Console.ReadKey().Key == ConsoleKey.Escape) //If user presses escape the program terminates, else it restarts.
                {
                    Environment.Exit(0);
                }

            } while (loop == true); // Will always be true.

        }

        //Method to display options to the user and store those in an array.
        static int[] TwoUnits()
        {
            int[] chosen = new int[2]; //Array to be returned with user choices.
            int count = 0; // Counter for do while loop.
            int choice = 100;
            Console.Clear();
            do
            {
                Console.WriteLine("Välj dina två kända värden.");
                Console.WriteLine("Tryck 1 för Spänning");
                Console.WriteLine("Tryck 2 för Strömstyrka");
                Console.WriteLine("Tryck 3 för Resistans");
                Console.WriteLine("Tryck 4 för Effekt");
                Console.WriteLine("\n. . .\n");
                Console.WriteLine("Tryck 9 för att börja om.");
                Console.WriteLine("Tryck 0 för att avsluta programmet.");
                char temp = Console.ReadKey().KeyChar;
                string input_string = temp.ToString();
                if (!int.TryParse(input_string, out choice)) // If user enters a string or symbol that cannot be converted to int, the do loop continues. (restarts)
                {
                    Console.Clear();
                    Console.WriteLine("Ogiltigt val");
                    continue;
                }
                if (chosen[0] == choice) // If the user picks the same options twice, the loop continues. (restarts)
                {
                    Console.Clear();
                    Console.WriteLine("Du kan inte göra samma val två gånger!");
                    continue;
                }
                switch (choice) // Check what the user picked. If valid add it to the array Chosen that will be returned by the function, else the do loop continues. (restarts)
                {
                    case 1:
                        chosen[count] = 1;
                        Console.Clear();
                        Console.WriteLine("Du valde Spänning.\n\n");
                        count++;
                        break;
                    case 2:
                        chosen[count] = 2;
                        Console.Clear();
                        Console.WriteLine("Du valde Strömstyrka\n\n");
                        count++;
                        break;
                    case 3:
                        chosen[count] = 3;
                        Console.Clear();
                        Console.WriteLine("Du valde Resistans\n\n");
                        count++;
                        break;
                    case 4:
                        chosen[count] = 4;
                        Console.Clear();
                        Console.WriteLine("Du valde Effekt\n\n");
                        count++;
                        break;
                    case 9:
                        Main(null);
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Ogiltig inmatning\n\n");
                        break;
                }
            } while (count < 2); // loop continues until user has chosen two valid options
            return chosen; // Returns the array with the two options the user made.
        }

        //User picks what prefix he/she wants to use, store these in an array.
        static int[] GetPrefixes(int[] choices)
        {

            //Declare variables
            int prefix;
            int[] Prefixes = new int[2];
            int goal_count;
            int count = 0;
            char input_char;
            string input_string;

            //Foreach loop lets the user choose prefixes for the selected options.
            foreach (int choice in choices)
            {
                goal_count = count + 1;   //To be able to loop the nested switch case if the user inputs a invalid option.
                do
                {
                    Console.Clear();
                    switch (choice) // Checks what option the user picks, to be able to show relevant prefixes. 
                    {

                        case 1: //User selected Voltage.
                            // Write out instructions
                            Console.WriteLine("Välj Prefix för Spänning");
                            Console.WriteLine("Skriv 2 för Millivolts (mV)");
                            Console.WriteLine("Skriv 3 för Volts (V)");
                            Console.WriteLine("Skriv 4 för kilovolts (kV)");
                            Console.WriteLine("Skriv 5 för Megavolts (MV)");
                            input_char = Console.ReadKey().KeyChar;
                            input_string = input_char.ToString();
                            if (int.TryParse(input_string, out prefix)) //Check if the user submitted a valid number.
                            {

                                    switch (prefix) //switch case that checks what prefix the user selects.
                                    {
                                        case 2:
                                            Prefixes[count] = 2;
                                            count++;
                                            break;
                                        case 3:
                                            Prefixes[count] = 3;
                                            count++;
                                            break;
                                        case 4:
                                            Prefixes[count] = 4;
                                            count++;
                                            break;
                                        case 5:
                                            Prefixes[count] = 5;
                                            count++;
                                            break;
                                        default:
                                            Console.WriteLine("Ogiltigt val, tryck på valfri knapp.");
                                            Console.ReadKey();
                                            break;
                                    }
                            } else //User didn't select a valid number.
                            {
                                Console.WriteLine("Ogiltigt val, tryck på valfri knapp.");
                                Console.ReadKey();
                            }
                            break;
                        case 2: //User selected Current. Same as Case 1 but with differnet Prefixes that are suitable for Current.
                            Console.WriteLine("Välj Prefix för Strömstyrka");
                            Console.WriteLine("Skriv 1 för Microampere (µV)");
                            Console.WriteLine("Skriv 2 för Milliampere (mA)");
                            Console.WriteLine("Skriv 3 för Ampere (A)");
                            input_char = Console.ReadKey().KeyChar;
                            input_string = input_char.ToString();
                            if (int.TryParse(input_string, out prefix))
                            {

                                    switch (prefix)
                                    {
                                        case 1:
                                            Prefixes[count] = 1;
                                            count++;
                                            break;
                                        case 2:
                                            Prefixes[count] = 2;
                                            count++;
                                            break;
                                        case 3:
                                            Prefixes[count] = 3;
                                            count++;
                                            break;
                                        default:
                                            Console.WriteLine("Ogiltigt val, tryck på valfri knapp.");
                                            Console.ReadKey();
                                            break;
                                    }
                            } else
                            {
                                Console.WriteLine("Ogiltigt val, tryck på valfri knapp.");
                                Console.ReadKey();
                            }
                            break;
                        case 3: // User selected Resistance.
                            Console.WriteLine("Välj Prefix för Resistans");
                            Console.WriteLine("Skriv 3 för Ohms (?)");
                            Console.WriteLine("Skriv 4 för Kiloohm (k?)");
                            Console.WriteLine("Skriv 5 för Megaohms (M?)");
                            input_char = Console.ReadKey().KeyChar;
                            input_string = input_char.ToString();
                            if (int.TryParse(input_string, out prefix))
                            {
                                    switch (prefix)
                                    {
                                        case 3:
                                            Prefixes[count] = 3;
                                            count++;
                                            break;
                                        case 4:
                                            Prefixes[count] = 4;
                                            count++;
                                            break;
                                        case 5:
                                            Prefixes[count] = 5;
                                            count++;
                                            break;
                                        default:
                                            Console.WriteLine("Ogiltigt val, tryck på valfri knapp.");
                                            Console.ReadKey();
                                            break;

                                    }
                            } else
                            {
                                Console.WriteLine("Ogiltigt val, tryck på valfri knapp.");
                                Console.ReadKey();
                            }
                            break;
                        case 4: // User selected Power.
                            Console.WriteLine("Välj Prefix för Effekt");
                            Console.WriteLine("Skriv 3 för Watts (W)");
                            Console.WriteLine("Skriv 4 för Kilowatts (kW)");
                            Console.WriteLine("Skriv 5 för Megawatts (MW)");
                            input_char = Console.ReadKey().KeyChar;
                            input_string = input_char.ToString();
                            if (int.TryParse(input_string, out prefix))
                            {
                                    switch (prefix)
                                    {
                                        case 3:
                                            Prefixes[count] = 3;
                                            count++;
                                            break;
                                        case 4:
                                            Prefixes[count] = 4;
                                            count++;
                                            break;
                                        case 5:
                                            Prefixes[count] = 5;
                                            count++;
                                            break;
                                        default:
                                            Console.WriteLine("Ogiltigt val, tryck på valfri knapp.");
                                            Console.ReadKey();
                                            break;
                                    }
                            } else
                            {
                                Console.WriteLine("Ogiltigt val, tryck på valfri knapp.");
                                Console.ReadKey();
                            }
                            break;

                        default: //Should not happen, is there for redundnacy
                            Console.WriteLine("Något gick fel. Tryck på valfri knapp för att starta om."); 
                            Console.ReadKey();
                            Environment.Exit(0);
                            break;
                    } 
                } while (count != goal_count); // If the user doesnt select a valid option the counter will remain the same, and goal_count is count + 1, so the do while loop will continue.
            }
            return Prefixes; // Returns the array with prefixes for both options.
        }

        static double[] EnterValues(int[] Units)
        {
            //Declare Varialbes
            int goal_count;
            int count = 0;
            double Value;
            double[] Return_Values = new double[2];
            foreach (int unit in Units)
            {
                goal_count = count + 1; // Goal count is  always + 1 of the current count.
                do // Do loop continues until user writes in a valid value.
                {
                    Console.Clear();
                    switch (unit) // Switch case depending on what unit the user chose.
                    {
                        case 1:
                            Console.Write("Skriv in värdet för Spänning: ");
                            if (double.TryParse(Console.ReadLine(), out Value))
                            {
                                Return_Values[count] = Value;
                                count++;
                            } else
                            {
                                Console.WriteLine("Ogiltigt Värde, tryck på valfri knapp.");
                                Console.ReadKey();
                            }
                            break;
                        case 2:
                            Console.Write("Skriv in värdet för Strömstryka: ");
                            if (double.TryParse(Console.ReadLine(), out Value))
                            {
                                Return_Values[count] = Value;
                                count++;
                            }
                            else
                            {
                                Console.WriteLine("Ogiltigt Värde, tryck på valfri knapp.");
                                Console.ReadKey();
                            }
                            break;
                        case 3:
                            Console.Write("Skriv in värdet för Resistans: ");
                            if (double.TryParse(Console.ReadLine(), out Value))
                            {
                                Return_Values[count] = Value;
                                count++;
                            }
                            else
                            {
                                Console.WriteLine("Ogiltigt Värde, tryck på valfri knapp.");
                                Console.ReadKey();
                            }
                            break;
                        case 4:
                            Console.Write("Skriv in värdet för Effekt: ");
                            if (double.TryParse(Console.ReadLine(), out Value))
                            {
                                Return_Values[count] = Value;
                                count++;
                            }
                            else
                            {
                                Console.WriteLine("Ogiltigt Värde, tryck på valfri knapp.");
                                Console.ReadKey();
                            }
                            break;
                    }
                } while (goal_count != count); // Since Goal count is + 1 of the current count when the Do while loop is initiated, the loop will continue if the user inputs an invalid value.
            }
            return Return_Values;
        }

        // Begin Calculation
        static (double Voltage, double Current, double Resistance, double Power) Calculate(Unit One, Unit Two)
        {
            //Declare variable
            int Unit_one = One.Get_Option();
            int Unit_two = Two.Get_Option();
            int Prefix_one = One.Get_Prefix();
            int Prefix_two = Two.Get_Prefix();
            double Value_one = One.Get_Value();
            double Value_two = Two.Get_Value();
            //Return values
            double Voltage = 0;
            double Current = 0;
            double Resistance = 0;
            double Power = 0;


            // Prepare values for calculation (Normalize them depending on what prefix the user selected)
            Value_one = Prepare_Calculation(Value_one, Prefix_one); 
            Value_two = Prepare_Calculation(Value_two, Prefix_two);

            /*
             * 
             * Units and Prefixes are not stored as strings, instead I gave them an index, and that index is what determines what choices the user made. Table below shows those indexes.
             * 
             Units:
             Voltage = 1;
             Current = 2;
             Resistance = 3;
             Power = 4;

             Prefixes:
             Mikro: 1
             Milli: 2
             Standard: 3
             Kilo: 4
             Mega: 5
             *
             *
             */

            //Check what the first Unit selected was.
            //May look a bit chaotic, but this was the simplest way I could think of.
            if (One.Get_Option() == 1) // User selected Voltage as their first Unit.
            {
                switch (Two.Get_Option()) //Switch case to check what the second choice was
                {
                    case 2: // User selected Current as their second choice. This means Resistance and Power is unknown.
                        Voltage = Value_one;//Voltage = The value the user entered. 
                        Current = Value_two; // Current = The value the user entered.
                        Resistance = Value_one / Value_two; // Resistance is = to formula based on Ohms Law
                        Power = Value_one * Value_two; // Power is = to formula based on Ohms Law.
                        break;
                    case 3: // User selected Resistance as their Second choice. This means Current and Power is unknown.
                        Voltage = Value_one;
                        Current = Value_one / Value_two;
                        Resistance = Value_two;
                        Power = Math.Pow(Value_one, 2) / Value_two;
                        break;
                    case 4: // User selected Power as their Second choice. This means Current and Resistance is Unkown.
                        Voltage = Value_one;
                        Current = Value_two / Value_one;
                        Resistance = Math.Pow(Value_one, 2) / Value_two;
                        Power = Value_two;
                        break;
                }
            } else if (One.Get_Option() == 2) // User selected Current as their first choice. Same as above.
            {
                switch (Two.Get_Option())
                {
                    case 1:
                        Voltage = Value_two;
                        Current = Value_one;
                        Resistance = Value_two / Value_one;
                        Power = Value_two * Value_one;
                        break;
                    case 3:
                        Voltage = Value_two * Value_one;
                        Current = Value_one;
                        Resistance = Value_two;
                        Power = Value_two * Math.Pow(Value_one, 2);
                        break;
                    case 4:
                        Voltage = Value_two / Value_one;
                        Current = Value_one;
                        Resistance = Math.Pow(Value_one, 2) / Value_two;
                        Power = Value_two;
                        break;
                }
            } else if (One.Get_Option() == 3)
            {
                switch (Two.Get_Option())
                {
                    case 1:
                        Voltage = Value_two;
                        Current = Value_two / Value_one;
                        Resistance = Value_one;
                        Power = Math.Pow(Value_two, 2) / Value_one;
                        break;
                    case 2:
                        Voltage = Value_one * Value_two;
                        Current = Value_two;
                        Resistance = Value_one;
                        Power = Value_one * Math.Pow(Value_two, 2);
                        break;
                    case 4:
                        Voltage = Math.Sqrt(Value_two * Value_one);
                        Current = Math.Sqrt(Value_two / Value_one);
                        Resistance = Value_one;
                        Power = Value_two;
                        break;
                }
            } else if (One.Get_Option() == 4)
            {
                switch (Two.Get_Option())
                {
                    case 1:
                        Voltage = Value_two;
                        Current = Value_one / Value_two;
                        Resistance = Math.Pow(Value_two, 2) / Value_one;
                        Power =  Value_one;
                        break;
                    case 2:
                        Voltage =  Value_one / Value_two;
                        Current =  Value_two;
                        Resistance = Value_one / Math.Pow(Value_two, 2);
                        Power = Value_one;
                        break;
                    case 3:
                        Voltage = Math.Sqrt(Value_one * Value_two);
                        Current = Math.Sqrt(Value_one / Value_two);
                        Resistance = Value_two;
                        Power = Value_one;
                        break;
                }
            }
            return (Voltage, Current, Resistance, Power);
        }

        // Normalizes the value depending on what prefix the user selected. (To be able to calculate using Ohms Law the units need to be of the same prefix.)
        static double Prepare_Calculation(double Value, int Prefix)
        {
            double Return_Value = 0;
            switch(Prefix)
            {
                case 1: // micro
                    Return_Value = Value / 1000000;
                    break;
                case 2: // Milli
                    Return_Value = Value / 1000;
                    break;
                case 3: // Standard.
                    Return_Value = Value;
                    break;
                case 4: // Kilo
                    Return_Value = Value * 1000;
                    break;
                case 5: // Mega
                    Return_Value = Value * 1000000;
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Något gick fel, programmet måste startas om.");
                    Console.WriteLine("Tryck på valfri knapp . . .");
                    Environment.Exit(0);
                    break;
            }
            return Return_Value;
        }
        // After calculation is done and before the result is displayed to the user, the result is converted to a fitting prefix.
        static (string Prefix, double Value) Apply_Prefix(double value_, int unit)
        {
            string Prefix = String.Empty;
            double Value = 0;

            if (value_ >= 1000000)
            {
                Prefix = "M";
                Value = value_ / 1000000;
            } else if (value_ < 1000000 && value_ >= 1000) {
                Prefix = "k";
                Value = value_ / 1000;
            } else if (value_ < 1000 && value_ >= 1)
            {
                Prefix = "";
                Value = value_;
            } else if (value_ < 1 && value_ >= 0.01)
            {
                Prefix = "m";
                Value = value_ * 1000;
            } else if (value_ < 0.01)
            {
                Prefix = "µ";
                Value = value_ * 1000000;
            }

            switch(unit)
            {
                case 1:
                    Prefix += "V";
                    break;
                case 2:
                    Prefix += "A";
                    break;
                case 3:
                    Prefix += "Ohm";
                    break;
                case 4:
                    Prefix += "W";
                    break;
                default: // Redundancy.
                    Console.WriteLine("Något gick fel, programmet måste starta om.");
                    Console.WriteLine("Tryck på valfri knapp för att fortsätta . . .");
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;
            }

            return (Prefix, Value);
        }
    }
    class Unit
    {
        int prefix;
        int option;
        double value;

        public int Get_Prefix() // Returns the index of the chosen prefix
        {
            return prefix;
        }
        public int Get_Option() // Returns the index of the chosen option
        {
            return option;
        }
        public double Get_Value() //Returns the Value.
        {
            return value;
        }

        public Unit(int prefix_, int option_, double value_) //Constructor
        {
            prefix = prefix_;
            option = option_;
            value = value_;
        }
        
    }
}
