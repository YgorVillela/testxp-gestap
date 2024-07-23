{
  "ConnectionStrings": {
    "DefaultConnection": "Server=SEU_SERVIDOR;Database=InvestmentDB;Trusted_Connection=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}

### Executando a Aplicação

1 - Restaure os pacotes NuGet:

dotnet restore

2 - Execute as migrações do banco de dados:

dotnet ef database update

3 - Inicie a Aplicação:

dotnet run

### Endpoints da API

Usuário
GET /api/Usuarios: Retorna todos os usuários.
POST /api/Usuarios: Cria um novo usuário.
GET /api/Usuarios/{id}: Retorna um usuário específico.
PUT /api/Usuarios/{id}: Atualiza um usuário específico.
DELETE /api/Usuarios/{id}: Deleta um usuário específico.

Produto Financeiro
GET /api/ProdutosFinanceiros: Retorna todos os produtos financeiros.
POST /api/ProdutosFinanceiros: Cria um novo produto financeiro.
GET /api/ProdutosFinanceiros/{id}: Retorna um produto financeiro específico.
PUT /api/ProdutosFinanceiros/{id}: Atualiza um produto financeiro específico.
DELETE /api/ProdutosFinanceiros/{id}: Deleta um produto financeiro específico.

Transações
POST /api/Transacoes/Comprar: Compra um produto financeiro.
POST /api/Transacoes/Vender: Vende um produto financeiro.
GET /api/Usuarios/{id}/Extrato: Retorna o extrato de um usuário.
