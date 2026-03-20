# TES.WebMVC - Aplicação MVC com .NET 8

Projeto web ASP.NET Core MVC para gerenciamento de contas bancárias, seguindo a arquitetura do TES.WebAPI mas com interface gráfica (Views).

## 🏗️ Arquitetura

```
src/WebMVC/
├── Controllers/          # Controladores MVC
│   ├── HomeController.cs
│   └── AccountsController.cs
├── Models/              # ViewModels e modelos locais
│   └── EditAccountViewModel.cs
├── Views/               # Templates Razor
│   ├── Home/
│   ├── Accounts/
│   └── Shared/
├── Extensions/          # Middlewares e extensões
│   └── ExceptionMiddleware.cs
├── Properties/
│   └── launchSettings.json
├── Program.cs           # Configuração da aplicação
├── appsettings.json     # Configurações
└── TES.WebMVC.csproj    # Definição do projeto
```

## 🎯 Funcionalidades

### Contas Bancárias
- ✅ **Criar** - Novo formulário com validação
- ✅ **Ler** - Visualizar detalhes de uma conta
- ✅ **Atualizar** - Editar nome do titular e status
- ✅ **Deletar** - Excluir com confirmação
- ✅ **List** - Listagem de contas (placeholder)

## 🛠️ Tecnologias

- **.NET 8.0** - Framework
- **ASP.NET Core MVC** - Padrão de apresentação
- **Entity Framework Core 8.0** - ORM
- **MediatR 12.2.0** - CQRS, mediador de requisições
- **FluentValidation** - Validação de dados
- **In-Memory Database** - Banco de dados em memória

## 📋 Dependências de Projeto

```xml
<ProjectReference Include="..\Application\TES.Application.csproj" />
<ProjectReference Include="..\Infrastructure\TES.Infrastructure.csproj" />
```

Reutiliza toda a lógica de negócio dos projetos Application e Infrastructure.

## 🚀 Como Executar

### Opção 1: Via Visual Studio

1. Abra `BankAccountApi.sln`
2. Defina **TES.WebMVC** como projeto de inicialização
3. Pressione `F5` ou clique em "Iniciar"
4. A aplicação abrirá em `https://localhost:7051`

### Opção 2: Via Terminal

```bash
cd src/WebMVC
dotnet run
```

Acesse: `http://localhost:5051` ou `https://localhost:7051` (com HTTPS)

## 🔌 Endpoints HTTP

Base URL: `http://localhost:5051`

| Ação | Método | URL | Resultado |
|------|--------|-----|-----------|
| Home | GET | `/` | Página inicial |
| Lista de Contas | GET | `/accounts` | Lista vazia (em desenvolvimento) |
| Criar Conta | GET | `/accounts/create` | Formulário |
| Criar Conta | POST | `/accounts/create` | Redireciona para detalhes |
| Detalhes | GET | `/accounts/{id}` | Visualizar conta |
| Editar | GET | `/accounts/{id}/edit` | Formulário de edição |
| Editar | POST | `/accounts/{id}/edit` | Atualiza conta |
| Excluir | GET | `/accounts/{id}/delete` | Confirmação |
| Excluir | POST | `/accounts/{id}/delete` | Remove conta |

## 🎨 Views (Templates Razor)

### Shared
- `_Layout.cshtml` - Template principal com navbar
- `_ViewImports.cshtml` - Imports globais
- `_ViewStart.cshtml` - Executa antes de cada view
- `_ValidationScriptsPartial.cshtml` - Scripts de validação

### Home
- `Index.cshtml` - Dashboard principal
- `Privacy.cshtml` - Página de privacidade

### Accounts
- `Index.cshtml` - Listagem (placeholder)
- `Create.cshtml` - Formulário de criação
- `Details.cshtml` - Visualizar detalhes
- `Edit.cshtml` - Formulário de edição
- `Delete.cshtml` - Confirmação de exclusão

## 💾 Configuração de Banco de Dados

O projeto usa **In-Memory Database** do Entity Framework Core:

```csharp
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseInMemoryDatabase("TesDb"));
```

**Nota**: Os dados são perdidos ao reiniciar a aplicação. Para persistência, configure um banco SQL Server/PostgreSQL.

## 🔐 Tratamento de Exceções

Middleware centralizado em `ExceptionMiddleware.cs`:

- `ValidationException` → 422 Unprocessable Entity
- `DomainException` → 400 Bad Request
- `NotFoundException` → 404 Not Found
- Genérica → 500 Internal Server Error

## 🧪 Validação

As validações são executadas via FluentValidation, herdadas do projeto Application:

```csharp
builder.Services.AddValidatorsFromAssemblyContaining<CreateAccountCommandValidator>();
```

## 📊 Serviços Injetados

- `IMediator` - Mediador CQRS (Commands e Queries)
- `AppDbContext` - Contexto do banco de dados
- `IAccountRepository` - Repositório de contas
- `IAccountCacheService` - Serviço de cache
- `IMemoryCache` - Cache em memória

## 🚦 Configuração de Inicialização

Arquivo `Properties/launchSettings.json`:

```json
{
  "profiles": {
    "http": {
      "applicationUrl": "http://localhost:5051"
    },
    "https": {
      "applicationUrl": "https://localhost:7051"
    }
  }
}
```

## 📝 Exemplo: Criar Conta

1. Acesse `/accounts/create`
2. Preencha o formulário:
   - **Nome do Titular**: "João Silva"
   - **CPF**: "12345678901"
3. Clique em "Criar"
4. Será redirecionado para os detalhes da conta criada

## 🔄 Padrão CQRS

Todas as operações usam **Commands** e **Queries** via MediatR:

- **CreateAccountCommand** - Cria nova conta
- **UpdateAccountCommand** - Atualiza conta
- **DeleteAccountCommand** - Deleta conta
- **GetAccountByIdQuery** - Busca conta por ID

Exemplo de uso no controlador:

```csharp
var result = await _mediator.Send(new CreateAccountCommand
{
    NomeTitular = "João Silva",
    Cpf = "12345678901"
}, ct);
```

## 📦 Estrutura de Namespaces

```
TES.WebMVC
├── Controllers
├── Models
├── Views
├── Extensions
└── Properties
```

Reutiliza namespaces de:
- `TES.Application` - Lógica de aplicação
- `TES.Domain` - Entidades de domínio
- `TES.Infrastructure` - Persistência

## ⚙️ Próximos Passos

- [ ] Implementar listagem de contas com paginação
- [ ] Adicionar autenticação/autorização
- [ ] Integrar com banco de dados SQL Server
- [ ] Adicionar mais validações no cliente (JavaScript)
- [ ] Melhorar UI com CSS/Bootstrap
- [ ] Adicionar testes integrados
- [ ] Implementar notificações com SignalR
- [ ] Adicionar exportação de relatórios

## 🤝 Relação com TES.WebAPI

| Aspecto | WebMVC | WebAPI |
|---------|--------|--------|
| Apresentação | Razor Views | JSON/REST |
| Tipo | Tradicional MVC | API RESTful |
| Protocolo | HTTP/HTTPS | HTTP/HTTPS |
| Resposta | HTML | JSON |
| Cliente | Navegador | Postman/SPA |
| Camada | Web | Serviço |

Ambos compartilham:
- Application layer (CQRS)
- Domain layer (Entidades)
- Infrastructure layer (Persistência)

## 📄 Licença

MIT
