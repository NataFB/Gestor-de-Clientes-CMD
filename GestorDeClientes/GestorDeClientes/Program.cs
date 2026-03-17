using GestorDeClientes.Models;
using GestorDeClientes.Services;

namespace GestorDeClientes
{
    class Program
    {
        // Enum para representar as opções do menu
        enum Menu { Listagem = 1, Adicionar, Remover, Sair }

        static void Main(string[] args)
        {
            // Cria o serviço (carrega os clientes automaticamente)
            ClienteService service = new ClienteService();

            bool sair = false;

            // Loop principal do sistema (fica rodando até sair = true)
            while (!sair)
            {
                Console.WriteLine("Sistema de clientes - Bem vindo!");
                Console.WriteLine("1-Listagem\n2-Adicionar\n3-Remover\n4-Sair");

                Menu opcao = (Menu)int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case Menu.Listagem:
                        Listagem(service);
                        break;

                    case Menu.Adicionar:
                        Adicionar(service);
                        break;

                    case Menu.Remover:
                        Remover(service);
                        break;

                    case Menu.Sair:
                        sair = true;
                        break;
                }

                Console.Clear();
            }
        }

        // Menu de adicionar um cliente
        static void Adicionar(ClienteService service)
        {
            // Cria um objeto Cliente
            Cliente cliente = new Cliente();

            //Solicitações das informações:
            Console.WriteLine("Nome:");
            cliente.Nome = Console.ReadLine();

            Console.WriteLine("Email:");
            cliente.Email = Console.ReadLine();

            Console.WriteLine("CPF:");
            cliente.Cpf = Console.ReadLine();

            service.Adicionar(cliente); //Envia o cliente para o service adicionar e salvar

            Console.WriteLine("Cadastrado! Enter...");
            Console.ReadLine();
        }

        // Menu responsável por listar os clientes
        static void Listagem(ClienteService service)
        {
            // Pega a lista de clientes do service
            var clientes = service.Listar();

            //Verifica se está vazia 
            if (clientes.Count == 0)
            {
                Console.WriteLine("Nenhum cliente.");
            }
            else
            {
                //Pecorre a lista de clientes exibindo os dados
                for (int i = 0; i < clientes.Count; i++)
                {
                    var c = clientes[i];

                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: {c.Nome}");
                    Console.WriteLine($"Email: {c.Email}");
                    Console.WriteLine($"CPF: {c.Cpf}");
                    Console.WriteLine("=====================");
                }
            }

            Console.ReadLine();
        }

        //Menu de remoção de cliente
        static void Remover(ClienteService service)
        {
            Listagem(service); // mostra a lista antes de remover

            Console.WriteLine("ID para remover:");
            int id = int.Parse(Console.ReadLine());
            //tenta remover o cliente
            if (!service.Remover(id))
            {
                //caso o ID seja inválido
                Console.WriteLine("ID inválido!");
                Console.ReadLine();
            }
        }
    }
}