using System.Text.Json;
using GestorDeClientes.Models;

namespace GestorDeClientes.Repositories
{
    public class ClienteRepository
    {
        private readonly string caminho = "Data/clients.json";
            public List<Cliente> Carregar()
            {
                try
                {
                    // Verifica se o arquivo existe
                    if (File.Exists("clients.json"))
                    {
                        // Lê todo o conteúdo do arquivo
                        string json = File.ReadAllText("clients.json");

                        // Converte o JSON para lista de clientes
                        List<Cliente> clientes = JsonSerializer.Deserialize<List<Cliente>>(json);

                        // Verifica se deu null após desserializar
                        if (clientes != null)
                        {
                            return clientes;
                        }
                        else
                        {
                            return new List<Cliente>();
                        }
                    }
                    else
                    {
                        // Se o arquivo não existir, retorna lista vazia
                        return new List<Cliente>();
                    }
                }
                catch
                {
                    // Se der qualquer erro, retorna lista vazia
                    return new List<Cliente>();
                }
            }

        public void Salvar(List<Cliente> clientes)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(clientes, options);

            Directory.CreateDirectory("Data"); // garante pasta
            File.WriteAllText(caminho, json);
        }
    }
}