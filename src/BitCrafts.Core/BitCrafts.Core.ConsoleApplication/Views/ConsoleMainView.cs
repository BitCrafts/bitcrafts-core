namespace BitCrafts.Core.ConsoleApplication.Views;

public class ConsoleMainView : IMainView
{
    public void Render()
    {
    }

    public void Show()
    {
        Console.WriteLine("Affichage de la vue Console.");
    }

    public void Close()
    {
        Console.WriteLine("Fermeture de la vue Console.");
    }

    public T GetUserControl<T>()
    {
        throw new NotImplementedException();
    }

    public void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }

    public string ReadLine()
    {
        return Console.ReadLine();
    }
}