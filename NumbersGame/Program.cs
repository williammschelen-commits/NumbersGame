namespace NumbersGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            void StartGame()
            {
                Console.WriteLine("Välkommen! Välj en svårighetsnivå. 1-5");
                int.TryParse(Console.ReadLine(), out int difficulty);
                if (difficulty < 1)
                    difficulty = 1;
                else if (difficulty > 5)
                    difficulty = 5;

                float modifier = (difficulty / 10f) * 2f + 1f;

                Random random = new Random();
                int randomNumberMax = (int)MathF.Round(10 * modifier);
                int randomNumberMin = 1;
                int randomNumber = random.Next(randomNumberMin, randomNumberMax);

                int maxAttempts = (int)MathF.Round(9 / modifier);
                int currentAttempts = 0;
                bool gameOver = false;

                string[] closeLogs = ["Det bränns!", "Det var nära!"];
                string[] highLogs = ["Tyvärr, du gissade för högt!", "Haha! Det var för högt!", "Bra gissat, men det var för högt."];
                string[] lowLogs = ["Tyvärr, du gissade för lågt!", "Haha! Det var för lågt!", "Bra gissat, men det var för lågt."];


                Console.WriteLine($"Okej! Jag tänker på ett nummer mellan {randomNumberMin} och {randomNumberMax}. Kan du gissa vilket? Du får {maxAttempts} försök.");
                while (gameOver != true)
                {
                    bool exceededMaxAttempts = currentAttempts >= maxAttempts;
                    if (exceededMaxAttempts)
                    {
                        gameOver = true;
                        Console.WriteLine($"Tyvärr, du lyckades inte gissa talet på {maxAttempts} försök! Talet var {randomNumber}");
                        break;
                    }
                    currentAttempts++;
                    string userInput = Console.ReadLine();
                    int.TryParse(userInput, out int userNumber);
                    int difference = Math.Abs(userNumber - randomNumber);
                    if (userNumber == randomNumber)
                    {
                        Console.WriteLine("Wohoo! Du klarade det!");
                        gameOver = true;
                        break;
                    }

                    Random.Shared.Shuffle(closeLogs);
                    Random.Shared.Shuffle(highLogs);
                    Random.Shared.Shuffle(lowLogs);
                    if (difference == 1)
                    {
                        Console.WriteLine(closeLogs[0]);
                    }
                    else if (userNumber > randomNumber)
                    {
                        Console.WriteLine(highLogs[0]);
                    }
                    else if (userNumber < randomNumber)
                    {
                        Console.WriteLine(lowLogs[0]);
                    }
                }
                Console.WriteLine("Vill du spela igen? ja/nej");
                string userRestartInput = Console.ReadLine();
                if (userRestartInput?.ToLower() == "ja")
                {
                    StartGame();
                }
            }
            StartGame();
        }
    }
}
