using BitCrafts.Core.Contracts.Applications;

namespace BitCrafts.Core.ConsoleApplication;

public class ConsoleUserInteraction : IUserInteraction
{
    public void WriteMessage(string message)
    {
        Console.WriteLine(message);
    }

    public string ReadMessage()
    {
        return Console.ReadLine();
    }

    public void StartInteractionLoop()
    {
        while (true)
        {
            var input = Ask("Enter a command (type 'exit' to quit): ");

            if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                WriteMessage("Exiting console application...");
                break;
            }

            WriteMessage($"You entered: {input}");
        }
    }

    public string Ask(string prompt)
    {
        WriteMessage(prompt);
        return ReadMessage();
    }
}