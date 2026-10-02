namespace NumbersGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Game loop runs here
            while (true)
            {
                StartGame();

                Console.WriteLine("Vill du spela igen? ja/nej");
                if (GetYesOrNoInput())
                    continue;

                Console.WriteLine("Hej då!");
                break;
            }
        }

        private static void StartGame()
        {
            // First welcome the user and make them input a difficulty level
            Console.WriteLine("Välkommen! Välj en svårighetsnivå. 1-5");
            int difficulty = GetNumberInput(1, 5);
            // Use decimal to avoid floating-point representation differences that would otherwise affect the rounding of the max attempts
            decimal modifier = (difficulty / 10m) * 2m + 1m;

            Random random = new Random();
            // Define the maximum and minimum depending on the difficulty/modifier
            int randomNumberMax = RoundToNearestInt(10 * modifier);
            int randomNumberMin = 1;
            // Define the actual random number to be guessed
            int randomNumber = random.Next(randomNumberMin, randomNumberMax);

            // Define the maximum attempts depending on the difficulty/modifier
            int maxAttempts = RoundToNearestInt(9 / modifier);
            // The amount of attempts the user currently has used
            int currentAttempts = 0;

            // Different messages for the game to feel more alive
            string[] closeLogs = ["Det bränns!", "Det var nära!"];
            string[] highLogs = ["Tyvärr, du gissade för högt!", "Haha! Det var för högt!", "Bra gissat, men det var för högt."];
            string[] lowLogs = ["Tyvärr, du gissade för lågt!", "Haha! Det var för lågt!", "Bra gissat, men det var för lågt."];

            // The guessing begins
            Console.WriteLine($"Okej! Jag tänker på ett nummer mellan {randomNumberMin} och {randomNumberMax}. Kan du gissa vilket? Du får {maxAttempts} försök.");
            while (true)
            {
                // At the start of each itteration of the loop check if you are out of attempts before continuing
                bool outOfAttempts = currentAttempts >= maxAttempts;
                if (outOfAttempts)
                {
                    Console.WriteLine($"Tyvärr, du lyckades inte gissa talet på {maxAttempts} försök! Talet var {randomNumber}");
                    break;
                }

                // Let the user guess the number
                int userNumber = GetNumberInput(randomNumberMin, randomNumberMax);
                currentAttempts++;

                // Victory!!
                if (userNumber == randomNumber)
                {
                    Console.WriteLine("Wohoo! Du klarade det!");
                    break;
                }

                // Get the non-negative difference between the users guess and the correct number
                int difference = Math.Abs(userNumber - randomNumber);

                // Since the user guessed incorrectly tell them why with a message from an array after shuffleing it
                if (difference == 1)
                {
                    Random.Shared.Shuffle(closeLogs);
                    Console.WriteLine(closeLogs[0]);
                }
                else if (userNumber > randomNumber)
                {
                    Random.Shared.Shuffle(highLogs);
                    Console.WriteLine(highLogs[0]);
                }
                else if (userNumber < randomNumber)
                {
                    Random.Shared.Shuffle(lowLogs);
                    Console.WriteLine(lowLogs[0]);
                }
            }
        }

        private static int GetNumberInput(int min, int max)
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int userInput))
                {
                    if (userInput < min || userInput > max)
                    {
                        Console.WriteLine($"Du måste skriva ett heltal inom intervallet {min}-{max}");
                        continue;
                    }
                    return userInput;
                }
                Console.WriteLine($"Felaktig inmatning. Skriva endast heltal inom intervallet {min}-{max}");
            }
        }

        // Convert Math.Round result which is deciaml to Int32 and use MidpointRounding.AwayFromZero so that values like 4.5 becomes 5
        private static int RoundToNearestInt(decimal value) => Convert.ToInt32(Math.Round(value, MidpointRounding.AwayFromZero));

        private static bool GetYesOrNoInput()
        {
            while (true)
            {
                string userInput = Console.ReadLine() ?? "";

                // ToLower() is used to make the input not case-sensitive
                if (userInput.ToLower() == "ja")
                    return true;

                if (userInput.ToLower() == "nej")
                    return false;

                Console.WriteLine("Du måste skriva JA eller NEJ");
            }
        }
    }
}
