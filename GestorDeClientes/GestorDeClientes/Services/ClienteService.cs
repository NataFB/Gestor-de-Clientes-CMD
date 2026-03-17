using GestorDeClientes.Models;       
using GestorDeClientes.Repositories;  

namespace GestorDeClientes.Services
{
    // Classe responsável pelas regras de negócio do sistema
    public class ClienteService
    {
        // Lista que armazena os clientes em memória
        private List<Cliente> clientes;

        // Objeto responsável por salvar e carregar os dados (JSON)
        private ClienteRepository repository;

        // Construtor da classe
        public ClienteService()
        {
            
            repository = new ClienteRepository();

            // Carrega os clientes do arquivo JSON para a lista
            clientes = repository.Carregar();
        }

        // Método que retorna a lista de clientes
        public List<Cliente> Listar()
        {
            return clientes;
        }

        // Método para adicionar um novo cliente
        public void Adicionar(Cliente cliente)
        {
            // Adiciona o cliente na lista
            clientes.Add(cliente);

            // Salva a lista atualizada no arquivo JSON
            repository.Salvar(clientes);
        }

        // Método para remover um cliente pelo ID (posição na lista)
        public bool Remover(int id)
        {
            // Verifica se o ID é válido (não pode ser negativo nem maior que o tamanho da lista)
            if (id >= 0 && id < clientes.Count)
            {
                // Remove o cliente da lista pela posição
                clientes.RemoveAt(id);

                // Salva a lista atualizada
                repository.Salvar(clientes);

                // Retorna verdadeiro indicando sucesso
                return true;
            }

            // Se o ID for inválido, retorna falso
            return false;
        }
    }
}