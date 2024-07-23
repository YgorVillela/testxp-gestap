# Investment Portfolio Management

## Configura��o

### Banco de Dados

Antes de iniciar a aplica��o, configure o arquivo `appsettings.json` com as informa��es do seu servidor de banco de 
dados.

```json
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

### Executando a Aplica��o

1 - Restaure os pacotes NuGet:

dotnet restore

2 - Execute as migra��es do banco de dados:

dotnet ef database update

3 - Inicie a Aplica��o:

dotnet run

### Endpoints da API

Usu�rio
GET /api/Usuarios: Retorna todos os usu�rios.
POST /api/Usuarios: Cria um novo usu�rio.
GET /api/Usuarios/{id}: Retorna um usu�rio espec�fico.
PUT /api/Usuarios/{id}: Atualiza um usu�rio espec�fico.
DELETE /api/Usuarios/{id}: Deleta um usu�rio espec�fico.

Produto Financeiro
GET /api/ProdutosFinanceiros: Retorna todos os produtos financeiros.
POST /api/ProdutosFinanceiros: Cria um novo produto financeiro.
GET /api/ProdutosFinanceiros/{id}: Retorna um produto financeiro espec�fico.
PUT /api/ProdutosFinanceiros/{id}: Atualiza um produto financeiro espec�fico.
DELETE /api/ProdutosFinanceiros/{id}: Deleta um produto financeiro espec�fico.

Transa��es
POST /api/Transacoes/Comprar: Compra um produto financeiro.
POST /api/Transacoes/Vender: Vende um produto financeiro.
GET /api/Usuarios/{id}/Extrato: Retorna o extrato de um usu�rio.
