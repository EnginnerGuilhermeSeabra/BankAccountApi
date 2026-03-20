# 🎨 Guia de Telas e Componentes Bootstrap - TES WebMVC

Documentação completa das telas e componentes Bootstrap utilizados no projeto TES WebMVC.

## 📋 Índice

- [Layout Principal](#layout-principal)
- [Páginas](#páginas)
- [Componentes](#componentes)
- [Paleta de Cores](#paleta-de-cores)
- [Ícones](#ícones)
- [Responsividade](#responsividade)

---

## 🏗️ Layout Principal

### Estrutura Base

```
┌─────────────────────────────────────┐
│      Navbar (Dark, Sticky)          │
├─────────────────────────────────────┤
│                                     │
│  Main Content                       │
│  - Alerts                           │
│  - Page Content                     │
│                                     │
├─────────────────────────────────────┤
│      Footer (Dark)                  │
└─────────────────────────────────────┘
```

### _Layout.cshtml

**Componentes:**
- **Navbar**: Dark com gradiente, sticky-top
- **Brand**: "TES Fintech" com ícone de banco
- **Links**: Home, Contas, Nova Conta, Privacidade
- **Alerts**: Success, Error, Info com ícones
- **Footer**: Rodapé com informações e links

**Cdn Bootstrap:**
```html
<!-- Bootstrap 5 CSS -->
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">

<!-- Bootstrap Icons -->
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.0/font/bootstrap-icons.css">

<!-- Bootstrap JS Bundle -->
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
```

---

## 📄 Páginas

### 1️⃣ Home/Index.cshtml

**Seções:**
1. **Hero Section** - Gradiente azul com call-to-action
2. **Features Cards** - 4 cards com ícones (Criar, Visualizar, Atualizar, Excluir)
3. **Tech Stack** - Tecnologias utilizadas com ícones
4. **Call-to-Action** - Botão para criar conta

**Componentes:**
- Gradiente: `background: linear-gradient(135deg, #667eea 0%, #764ba2 100%)`
- Cards com hover effect
- Ícones do Bootstrap Icons

**Classes Bootstrap:**
```css
.display-3        /* Título grande */
.lead             /* Parágrafo destacado */
.bg-gradient      /* Fundo com gradiente */
.btn-link         /* Links estilizados */
.card.h-100       /* Cards de altura igual */
```

---

### 2️⃣ Accounts/Index.cshtml

**Seções:**
1. **Header** com título e botão "Nova Conta"
2. **Estatísticas** - Total, Ativas, Bloqueadas
3. **Tabela de Contas** (placeholder por enquanto)
4. **Dica** com card de borda colorida

**Layout:**
```
┌─ Header ─────────────────────┐
│ Título + Nova Conta (btn)    │
├─ Stats ──────────────────────┤
│ Total │ Ativas │ Bloqueadas  │
├─ Tabela ─────────────────────┤
│ (vazio por enquanto)         │
├─ Dica ───────────────────────┤
│ Comece criando...            │
└──────────────────────────────┘
```

**Componentes:**
- Alert info com ícone
- 3 cards de estatísticas
- Border-left-primary

---

### 3️⃣ Accounts/Create.cshtml

**Formulário com:**
- Campo Nome do Titular
- Campo CPF (apenas números, 11 dígitos)
- Validação Bootstrap
- Alert de erros
- Botões Criar / Cancelar

**Componentes:**
- `form-control-lg` - Inputs maiores
- `invalid-feedback` - Mensagens de erro
- `was-validated` - Validação visual
- Placeholder com exemplo

**JavaScript:**
```javascript
// CPF mask na entrada
// Remove caracteres não-numéricos
// Limita a 11 dígitos
```

---

### 4️⃣ Accounts/Details.cshtml

**Exibe:**
- Nome do Titular
- CPF
- Status (com Badge colorida)
- ID da Conta
- Data de Criação
- Último Update

**Componentes:**
- Badges coloridas: `.badge.bg-success`, `.bg-danger`, `.bg-secondary`
- Layout em 2 colunas (desktop), 1 coluna (mobile)
- Info card com dicas à direita

**Status Badges:**
```
✓ Ativa     → bg-success (verde)
⏸ Inativa   → bg-secondary (cinza)
🔒 Bloqueada → bg-danger (vermelho)
```

---

### 5️⃣ Accounts/Edit.cshtml

**Formulário:**
- Nome do Titular (editável)
- Status (dropdown com 3 opções)
- Botões Salvar / Cancelar

**Componentes:**
- `form-select-lg` - Select grande
- ícone de lápis <i class="bi bi-pencil-square"></i>
- Alert info com ID da conta

---

### 6️⃣ Accounts/Delete.cshtml

**Confirmação:**
- Alert danger destacado
- Card com borda vermelha
- Detalhes da conta
- Botões Confirmar / Cancelar
- Alert com informações importantes

**Componentes:**
- `alert-danger` com ícone de exclamação
- `border-danger border-2` - Borda destaque
- `btn-danger` para ação destrutiva
- Links úteis para cancelar

---

### 7️⃣ Home/Privacy.cshtml

**Seções:**
- Introdução
- 6 seções de informações
- Última atualização
- Botão Voltar

**Componentes:**
- Card com shadow
- Separadores `<hr>`
- Listas com ícones
- Footer com data

---

### 8️⃣ Shared/Error.cshtml

**Layout:**
- Ícone de erro grande (4rem)
- Título "Oops!"
- Card com informações
- ID da requisição
- Ambiente (Development/Production)
- Botões de navegação

**Componentes:**
- `min-vh-100` - Preenche altura da viewport
- `align-items-center` - Centralizado verticalmente
- Info boxes com background light
- Font-monospace para ID

---

## 🧩 Componentes Reutilizáveis

### Buttons

```html
<!-- Primary -->
<button class="btn btn-primary btn-lg">
  <i class="bi bi-check-circle"></i> Ação Primária
</button>

<!-- Warning -->
<a class="btn btn-warning btn-lg">
  <i class="bi bi-pencil-square"></i> Editar
</a>

<!-- Danger -->
<a class="btn btn-danger btn-lg">
  <i class="bi bi-trash"></i> Excluir
</a>

<!-- Outline -->
<button class="btn btn-outline-secondary btn-lg">
  <i class="bi bi-arrow-left"></i> Cancelar
</button>
```

### Cards

```html
<!-- Card básico -->
<div class="card border-0 shadow">
  <div class="card-body">Conteúdo</div>
</div>

<!-- Card com hover -->
<div class="card h-100 border-0 shadow-sm hover-shadow">
  <div class="card-body">...</div>
</div>

<!-- Card com header -->
<div class="card border-0 shadow-sm">
  <div class="card-header bg-white border-bottom">
    <h5><i class="bi bi-table"></i> Título</h5>
  </div>
  <div class="card-body">...</div>
</div>
```

### Alerts

```html
<!-- Success -->
<div class="alert alert-success alert-dismissible fade show">
  <i class="bi bi-check-circle"></i> Sucesso!
  <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
</div>

<!-- Danger -->
<div class="alert alert-danger alert-dismissible fade show">
  <i class="bi bi-exclamation-triangle"></i> Erro!
</div>

<!-- Info -->
<div class="alert alert-info">
  <i class="bi bi-info-circle"></i> Informação
</div>

<!-- Warning -->
<div class="alert alert-warning">
  <i class="bi bi-exclamation-lg"></i> Aviso
</div>
```

### Forms

```html
<!-- Input com label -->
<div class="mb-4">
  <label class="form-label fw-bold">
    <i class="bi bi-person"></i> Nome
  </label>
  <input type="text" class="form-control form-control-lg" 
         placeholder="Ex: João Silva" />
  <small class="text-muted">Informe o nome completo</small>
</div>

<!-- Select -->
<div class="mb-4">
  <label class="form-label fw-bold">Status</label>
  <select class="form-select form-select-lg">
    <option value="Ativa">Ativa</option>
    <option value="Inativa">Inativa</option>
  </select>
</div>
```

### Badges

```html
<!-- Success -->
<span class="badge bg-success">✓ Ativa</span>

<!-- Danger -->
<span class="badge bg-danger">🔒 Bloqueada</span>

<!-- Secondary -->
<span class="badge bg-secondary">⏸ Inativa</span>
```

---

## 🎨 Paleta de Cores

```css
--primary: #667eea        /* Azul roxo */
--primary-dark: #5568d3   /* Azul roxo escuro */
--secondary: #764ba2      /* Roxo */
--success: #10b981        /* Verde */
--warning: #f59e0b        /* Amarelo/Laranja */
--danger: #ef4444         /* Vermelho */
--light: #f3f4f6          /* Cinza claro */
--dark: #1f2937           /* Cinza escuro */
```

### Gradientes

```css
/* Primário */
background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);

/* Dark Navbar/Footer */
background: linear-gradient(135deg, #1f2937 0%, #111827 100%);
```

---

## 🏷️ Ícones Bootstrap Icons

### Comumente utilizados:

```
Gerais
<i class="bi bi-bank"></i>              🏦 Banco
<i class="bi bi-bank2"></i>             🏢 Banco 2
<i class="bi bi-credit-card"></i>       💳 Cartão de crédito
<i class="bi bi-credit-card2-front"></i> 💳 Cartão frente

Ações
<i class="bi bi-plus-circle"></i>       ➕ Adicionar
<i class="bi bi-pencil-square"></i>     ✏️ Editar
<i class="bi bi-trash"></i>             🗑️ Deletar
<i class="bi bi-check-circle"></i>      ✅ Sucesso
<i class="bi bi-exclamation-triangle"></i> ⚠️ Aviso
<i class="bi bi-info-circle"></i>       ℹ️ Informação

Navegação
<i class="bi bi-arrow-left"></i>        ⬅️ Voltar
<i class="bi bi-arrow-right-circle"></i> ➡️ Próximo
<i class="bi bi-house"></i>             🏠 Home

Dados
<i class="bi bi-person"></i>            👤 Pessoa
<i class="bi bi-card-text"></i>         🎫 Cartão
<i class="bi bi-calendar-event"></i>    📅 Data
<i class="bi bi-clock-history"></i>     🕐 Relógio

Status
<i class="bi bi-check-lg"></i>          ✓ Check
<i class="bi bi-circle-fill"></i>       ● Status ativo
<i class="bi bi-pause-circle"></i>      ⏸ Pausa
<i class="bi bi-lock-circle"></i>       🔒 Bloqueado

Outros
<i class="bi bi-hourglass-split"></i>   ⏳ Carregando
<i class="bi bi-heart-fill"></i>        ❤️ Coração
<i class="bi bi-gear"></i>              ⚙️ Configurações
```

---

## 📱 Responsividade

### Breakpoints Bootstrap

```css
xs    < 576px    /* Mobile */
sm    ≥ 576px    /* Tablet pequeno */
md    ≥ 768px    /* Tablet */
lg    ≥ 992px    /* Desktop */
xl    ≥ 1200px   /* Desktop grande */
xxl   ≥ 1400px   /* Desktop muito grande */
```

### Exemplos

```html
<!-- Coluna diferente em cada tamanho -->
<div class="col-12 col-sm-6 col-lg-3">
  Conteúdo
</div>

<!-- Ocultar em mobile -->
<div class="d-none d-md-block">
  Visível apenas em tablet+
</div>

<!-- Mostrar apenas em mobile -->
<div class="d-md-none">
  Visível apenas em mobile
</div>
```

---

## 🚀 JavaScript Integrado

### Funcionalidades

1. **CPF Mask** - Formata CPF automaticamente
2. **Form Validation** - Validação Bootstrap
3. **Tooltips** - Tooltips interativas
4. **Auto-dismiss** - Alerts desaparecem automaticamente
5. **Scroll Animations** - Cards animam ao scroll
6. **Loading States** - Botões mostram estado de carregamento

### Exemplo de uso:

```javascript
// Notificações
showNotification('Sucesso!', 'success');
showNotification('Erro!', 'error');

// Confirmação
confirmAction('Tem certeza?');

// Formatação
formatDate(new Date());
formatCPF('12345678901');
```

---

## 🎯 Boas Práticas

### 1. Sempre usar classes Bootstrap

```html
<!-- ✅ Bom -->
<div class="btn btn-primary">Ação</div>

<!-- ❌ Ruim -->
<div style="background: blue; color: white;">Ação</div>
```

### 2. Incluir ícones em botões

```html
<!-- ✅ Bom -->
<button class="btn btn-danger">
  <i class="bi bi-trash"></i> Excluir
</button>

<!-- ❌ Ruim -->
<button class="btn btn-danger">Excluir</button>
```

### 3. Usar containers adequados

```html
<!-- ✅ Bom - Responsivo -->
<div class="container">
  <div class="row">
    <div class="col-md-6">...</div>
  </div>
</div>

<!-- ❌ Ruim - Largura fixa -->
<div style="width: 800px;">...</div>
```

### 4. Validação no cliente

```html
<!-- ✅ Bom -->
<form novalidate>
  <input required />
  <input pattern="\d{11}" />
</form>

<!-- ❌ Ruim - Sem validação -->
<form>
  <input />
</form>
```

---

## 📚 Recursos Úteis

- **Bootstrap 5 Docs**: https://getbootstrap.com/docs/5.3/
- **Bootstrap Icons**: https://icons.getbootstrap.com/
- **CDN Bootstrap**: https://cdn.jsdelivr.net/

---

## 🎬 Próximos Passos

- [ ] Implementar listagem de contas com paginação
- [ ] Adicionar temas (light/dark)
- [ ] Melhorar animações
- [ ] Adicionar mais ícones customizados
- [ ] Criar componentes reutilizáveis
- [ ] Testes de responsividade

---

**Atualizado**: 19 de Março de 2026  
**Versão**: 1.0  
**Framework**: Bootstrap 5.3.0
