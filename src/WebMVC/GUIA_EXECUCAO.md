# 🚀 Guia de Execução - TES WebMVC

Instruções completas para executar o projeto TES WebMVC com Bootstrap CSS.

## 📋 Pré-requisitos

- ✅ .NET 8.0 SDK instalado
- ✅ Visual Studio 2022 (Community+) ou VS Code
- ✅ Git (opcional)

## 🚀 Como Executar

### Opção 1: Visual Studio 2022

#### 1. Abrir Solução
```
1. Abra Visual Studio 2022
2. Clique em "Open a project or solution"
3. Navegue até: C:\TES\BankAccountApi\BankAccountApi\BankAccountApi.sln
4. Clique em "Open"
```

#### 2. Definir Projeto de Inicialização
```
1. No Solution Explorer (lado direito)
2. Clique com botão direito em "TES.WebMVC"
3. Selecione "Set as Startup Project"
```

#### 3. Executar Projeto
```
Opção A: Pressione F5 (com debug)
Opção B: Ctrl + F5 (sem debug)
Opção C: Clique no botão ▶ (Play) na barra de ferramentas
```

#### 4. Acessar Aplicação
```
Navegador abrirá automaticamente:
http://localhost:5051  (sem HTTPS)
https://localhost:7051 (com HTTPS)
```

---

### Opção 2: Visual Studio Code

#### 1. Abrir Pasta do Projeto
```bash
cd C:\TES\BankAccountApi\BankAccountApi
code .
```

#### 2. Terminal Integrado
```bash
# Abra terminal: Ctrl + `
# Ou: Terminal > New Terminal
```

#### 3. Restaurar Dependências
```bash
dotnet restore
```

#### 4. Executar Projeto
```bash
cd src/WebMVC
dotnet run
```

#### 5. Acessar Aplicação
```
http://localhost:5051
ou
https://localhost:7051
```

---

### Opção 3: Terminal (.NET CLI)

#### 1. Navegar até o Projeto
```bash
cd C:\TES\BankAccountApi\BankAccountApi\src\WebMVC
```

#### 2. Executar
```bash
# Modo development (com hot reload)
dotnet run

# Modo release
dotnet run --configuration Release

# Com watch (recompila automaticamente)
dotnet watch
```

#### 3. Acessar
```
http://localhost:5051
```

---

## 🌐 Testando a Aplicação

### Home Page
```
URL: http://localhost:5051/
- Hero section com gradient
- 4 cards de funcionalidades
- Tech stack
- Call-to-action
```

### Criar Conta
```
URL: http://localhost:5051/accounts/create
- Formulário com validação
- Nome do Titular (texto)
- CPF (11 dígitos)
- Botões: Criar / Cancelar
```

**Teste:**
```
Nome: João Silva
CPF: 12345678901
Clique em "Criar Conta"
```

### Visualizar Detalhes
```
URL: http://localhost:5051/accounts/{id}
- Dados da conta
- Status (badge colorida)
- Data de criação
- Botões: Editar / Excluir / Voltar
```

### Editar Conta
```
URL: http://localhost:5051/accounts/{id}/edit
- Nome do Titular (editável)
- Status (dropdown: Ativa / Inativa)
- Botões: Salvar / Cancelar
```

### Excluir Conta
```
URL: http://localhost:5051/accounts/{id}/delete
- Confirmação com dados da conta
- Alert com aviso (irreversível)
- Botões: Confirmar / Cancelar
```

### Listagem
```
URL: http://localhost:5051/accounts/
- Placeholder (em desenvolvimento)
- Link para criar nova conta
- Dica com botão
```

### Privacidade
```
URL: http://localhost:5051/home/privacy
- Política de privacidade
- 6 seções informativas
- Links de navegação
```

---

## 🎨 Componentes Visuais

### Navbar
```
[🏦 TES Fintech] [Home] [Contas] [Nova Conta] [Privacidade]
```
- **Cor**: Dark com gradiente
- **Sticky**: Fica no topo ao scroll
- **Responsive**: Menu colapsível em mobile

### Cards
```
┌─────────────────┐
│  [Ícone]        │
│  Título         │
│  Descrição      │
│  [Botão]        │
└─────────────────┘
```
- **Hover**: Levanta com shadow
- **Shadow**: Sutil por padrão
- **Badges**: Coloridas por status

### Alerts
```
[✓] Sucesso! - Verde
[✕] Erro! - Vermelho
[!] Aviso! - Laranja
[ℹ] Info - Azul
```
- **Auto-dismiss**: Success após 5 segundos
- **Dismissível**: Botão X para fechar
- **Ícones**: Visuais integrados

### Buttons
```
[← Azul] [✏️ Laranja] [🗑️ Vermelho] [↙ Cinza]
```
- **Tamanhos**: sm, md, lg
- **Estilos**: solid, outline
- **Ícones**: Sempre com ícone
- **Estados**: Hover, focus, active

---

## 🔄 Workflow Completo

### Criar e Editar Conta

```
1. Home (/)
   ↓
2. Clique em "Nova Conta"
   ↓
3. Preencha formulário
   - Nome: "João Silva"
   - CPF: "12345678901"
   ↓
4. Clique em "Criar Conta"
   ↓
5. Alert de Sucesso
   ↓
6. Redireciona para Details
   - Visualiza dados criados
   - Status: Ativa (badge verde)
   ↓
7. Clique em "Editar"
   ↓
8. Mude o status para "Inativa"
   ↓
9. Clique em "Salvar Alterações"
   ↓
10. Alert de Sucesso
    ↓
11. Volta para Details
    - Status atualizado (badge cinza)
```

### Deletar Conta

```
1. Em Details, clique em "Excluir"
   ↓
2. Alert vermelho com aviso
   ↓
3. Confirme os dados
   ↓
4. Clique em "Confirmar Exclusão"
   ↓
5. Alert de Sucesso
   ↓
6. Redireciona para Listagem
```

---

## 🛠️ Troubleshooting

### Porta já está em uso
```bash
# Mude a porta em launchSettings.json
# Procure por "applicationUrl"
# Mude 5051 e 7051 para outras portas
```

### Erro ao restaurar dependências
```bash
dotnet nuget locals all --clear
dotnet restore
```

### Cache do navegador
```
Pressione: Ctrl + Shift + Delete
Limpe cache do site
```

### SSL/TLS error
```
# Confie no certificado .NET
dotnet dev-certs https --clean
dotnet dev-certs https --trust
```

---

## 📊 Estrutura de Pastas

```
src/WebMVC/
├── Controllers/
│   ├── HomeController.cs
│   └── AccountsController.cs
├── Models/
│   └── EditAccountViewModel.cs
├── Views/
│   ├── Home/
│   │   ├── Index.cshtml
│   │   └── Privacy.cshtml
│   ├── Accounts/
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   ├── Details.cshtml
│   │   ├── Edit.cshtml
│   │   └── Delete.cshtml
│   └── Shared/
│       ├── _Layout.cshtml
│       ├── _ViewImports.cshtml
│       ├── _ViewStart.cshtml
│       ├── Error.cshtml
│       └── _ValidationScriptsPartial.cshtml
├── Extensions/
│   └── ExceptionMiddleware.cs
├── wwwroot/
│   ├── css/
│   │   └── site.css (400+ linhas de CSS)
│   ├── js/
│   │   └── site.js (300+ linhas de JS)
│   └── robots.txt
├── Properties/
│   └── launchSettings.json
├── Program.cs
├── appsettings.json
├── appsettings.Development.json
├── TES.WebMVC.csproj
├── README.md
└── TELAS_BOOTSTRAP.md
```

---

## 🎯 Recursos do Projeto

### Frontend
✅ Bootstrap 5.3.0 via CDN
✅ Bootstrap Icons
✅ Responsivo (mobile-first)
✅ Validação no cliente
✅ CPF mask
✅ Animações CSS
✅ Estilos customizados

### Backend
✅ .NET 8.0 MVC
✅ Entity Framework Core 8.0
✅ MediatR (CQRS)
✅ FluentValidation
✅ In-Memory Database
✅ Exception Middleware

---

## 📝 Dicas úteis

### 1. Desenvolvimento Rápido
```bash
# Use dotnet watch para hot reload
dotnet watch
```

### 2. Debug no VS
```
Pressione: F5
Defina breakpoints clicando na linha
```

### 3. Console JS
```
Abra DevTools: F12
Console mostra logs customizados
```

### 4. Limpar Database
```
Reinicie a aplicação (database é em-memória)
```

---

## 🚀 Build para Produção

```bash
# Build release
dotnet build --configuration Release

# Publish
dotnet publish -c Release -o ./publish

# Executar publicado
./publish/TES.WebMVC.exe
```

---

## 📚 Próximos Passos

1. Implementar listagem de contas com paginação
2. Adicionar autenticação
3. Integrar com banco de dados SQL Server
4. Adicionar testes automatizados
5. Melhorar UX/UI
6. Deploy em cloud

---

## 🆘 Suporte

Se encontrar problemas:

1. Verifique os erros no Console (F12)
2. Veja logs no Output do Visual Studio
3. Consulte [TELAS_BOOTSTRAP.md](TELAS_BOOTSTRAP.md)
4. Consulte [README.md](README.md)

---

**Última atualização**: 19 de Março de 2026  
**Versão**: 1.0  
**Status**: ✅ Pronto para uso
