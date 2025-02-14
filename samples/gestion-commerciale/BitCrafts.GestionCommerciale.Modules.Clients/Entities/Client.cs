namespace BitCrafts.GestionCommerciale.Modules.Clients.Entities;

public class Client
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Adresse { get; set; }
    public string Telephone { get; set; }
    public string Email { get; set; }
    public int GroupeId { get; set; } 
}