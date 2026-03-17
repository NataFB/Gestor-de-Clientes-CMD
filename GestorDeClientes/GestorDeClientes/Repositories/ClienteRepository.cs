using System.Text.Json; // Biblioteca usada para manipular arquivos json
using GestorDeClientes.Models; 

namespace GestorDeClientes.Repositories
{
    public class ClienteRepository
    {
        // Variável que guarda o caminho completo do arquivo clients.json
        private readonly string caminho;

        // Construtor da classe
        public ClienteRepository()
        {
            // Caminho base onde o programa está rodando (bin/Debug/netX/)
            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            // Cria um objeto que representa a pasta atual
            DirectoryInfo dir = new DirectoryInfo(basePath);

            // Loop que sobe pastas até encontrar o arquivo .csproj (raiz do projeto)
            while (dir != null && dir.GetFiles("*.csproj").Length == 0)
            {
                // Vai para a pasta pai (subindo nível)
                dir = dir.Parent;
            }

            // Se não encontrou a pasta do projeto, lança erro
            if (dir == null)
            {
                throw new Exception("Pasta do projeto não encontrada.");
            }

            // Monta o caminho final: /Data/clients.json combinando o caminho base + a pasta e arquivo a ser adicionados
            caminho = Path.Combine(dir.FullName, "Data", "clients.json");
        }

        // Método responsável por carregar os clientes do arquivo
        public List<Cliente> Carregar()
        {
            try
            {
                // Verifica se o arquivo existe
                if (File.Exists(caminho))
                {
                    // Lê todo o conteúdo do arquivo JSON
                    string json = File.ReadAllText(caminho);

                    // Converte o JSON em uma lista de Cliente
                    List<Cliente> clientes = JsonSerializer.Deserialize<List<Cliente>>(json);

                    // Verifica se a conversão deu certo (não retornou null)
                    if (clientes != null)
                    {
                        return clientes; // Retorna a lista carregada
                    }
                    else
                    {
                        // Se deu null, retorna lista vazia para evitar erro
                        return new List<Cliente>();
                    }
                }
                else
                {
                    // Se o arquivo não existir, retorna lista vazia
                    return new List<Cliente>();
                }
            }
            catch (Exception ex)
            {
                // Se ocorrer qualquer erro, exibe a mensagem no console
                Console.WriteLine("Erro ao carregar: " + ex.Message);

                // Retorna lista vazia para o programa não quebrar
                return new List<Cliente>();
            }
        }

        // Método responsável por salvar os clientes no arquivo
        public void Salvar(List<Cliente> clientes)
        {
            try
            {
                // Pega o caminho da pasta (sem o nome do arquivo)
                string pasta = Path.GetDirectoryName(caminho);

                // Verifica se a pasta existe
                if (!Directory.Exists(pasta))
                {
                    // Se não existir, cria a pasta automaticamente
                    Directory.CreateDirectory(pasta);
                }

                // Configuração para deixar o JSON formatado (bonitinho)
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                // Converte a lista de clientes para JSON
                string json = JsonSerializer.Serialize(clientes, options);

                // Salva o JSON no arquivo (sobrescreve se já existir, isso na aplicação não da erro pois antes foi tudo carregado então salvaria tudo denovo).
                File.WriteAllText(caminho, json);
            }
            catch (Exception ex)
            {
                // Se ocorrer erro ao salvar, mostra a mensagem
                Console.WriteLine("Erro ao salvar: " + ex.Message);
            }
        }
    }
}