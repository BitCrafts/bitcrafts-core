namespace BitCrafts.Core.Contracts.Applications;

public interface IUserInteraction
{
    void WriteMessage(string message);

    string ReadMessage();

    void StartInteractionLoop();
}