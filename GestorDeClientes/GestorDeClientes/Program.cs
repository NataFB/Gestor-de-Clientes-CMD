using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json; //Importando o serializador de Json

namespace GestorDeClientes
{
    class Program
    {
        [System.Serializable]
        struct Cliente
        {
            public string nome { get; set; }
            public string email { get; set; }
            public string cpf { get; set; }
        }

        static List<Cliente> clientes = new List<Cliente>();
        enum Menu { Listagem = 1, Adicionar, Remover, Sair }
        static void Main(string[] args)
        {
            Carregar();
            bool escolheuSair = false;
            while (!escolheuSair)
            {
                Console.WriteLine("Sistema de clientes - Bem vindo!");
                Console.WriteLine("1-Listagem\n2-Adicionar\n3-Remover\n4-Sair");
                Menu opcao = (Menu)int.Parse(Console.ReadLine()); //Passando para int e convertendo para o enum

                switch (opcao)
                {
                    case Menu.Listagem:
                        Listagem();
                        break;
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        escolheuSair = true;
                        break;
                }
                Console.Clear();
            }
        }

        static void Adicionar()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de cliente: ");
            Console.WriteLine("Nome do cliente");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Email do cliente: ");
            cliente.email = Console.ReadLine();
            Console.WriteLine("CPF do cliente: ");
            cliente.cpf = Console.ReadLine();

            clientes.Add(cliente); //adicionando cliente a lista de clientes
            Salvar();

            Console.WriteLine("Cadastro concluído, aperte enter para sair.");
            Console.ReadLine();
        }

        static void Listagem()
        {
            if (clientes.Count > 0)
            {
                int i = 0;
                Console.WriteLine("Lista de clientes: ");
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: {cliente.nome}");
                    Console.WriteLine($"Email: {cliente.email}");
                    Console.WriteLine($"CPF: {cliente.cpf}");
                    i++;
                    Console.WriteLine("=================================");
                }
                Console.WriteLine("Aperte enter para sair da lista.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Nenhum cliente cadastrado! Aperte enter para sair.");
                Console.ReadLine(); 
            }
        }

        static void Remover()
        {
            Listagem();
            Console.WriteLine("Digite o ID do cliente que você quer remover:");
            int id = int.Parse(Console.ReadLine());

            if(id >= 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id);
                Salvar();
            }
            else
            {
                Console.WriteLine("Id digitado é inválido, tente novamente!");
                Console.WriteLine();
            }
        }

        static void Salvar()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(clientes, options); //transforma a lista de clientes em json

            File.WriteAllText("clients.json", json);//WriteAllText cria ou sobrescreve o arquivo
        }

        static void Carregar()
        {
            try
            {
                if (File.Exists("clients.json"))
                {
                    string json = File.ReadAllText("clients.json"); //le todo o arquivo de json

                    clientes = JsonSerializer.Deserialize<List<Cliente>>(json); //descerializa o arquivo e passa para a lista
                }

                if (clientes == null)
                {
                    clientes = new List<Cliente>();
                }
            }
            catch
            {
                clientes = new List<Cliente>();
            }
        }
    }    
}