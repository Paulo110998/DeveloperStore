# 🛒 DeveloperStore - Sistema de Vendas (Avaliação Técnica)

Este projeto faz parte de uma **avaliação técnica** para a vaga de **Desenvolvedor .NET SR**, tendo como objetivo implementar uma nova feature em uma solution existente com arquitetura baseada em **DDD (Domain-Driven Design)**.

Foi desenvolvida uma API RESTful responsável pelo **CRUD completo de Vendas e Itens de Venda**, respeitando as regras de negócio fornecidas e utilizando boas práticas como **separação por camadas**, **injeção de dependência**, **desnormalização com identidades externas** e **eventos de domínio**.

---

## 🧠 Domínio do Caso de Uso

A API de vendas manipula os seguintes dados:

- Número da venda
- Data da venda
- Cliente (nome desnormalizado)
- Valor total da venda
- Filial (nome desnormalizado)
- Produtos (nome desnormalizado)
- Quantidades
- Preços unitários
- Descontos aplicados
- Valor total de cada item
- Status de cancelamento (cancelado ou não)

---

## ✅ Regras de Negócio

1. Compras com **4+ itens idênticos** têm **10% de desconto**
2. Compras entre **10 a 20 itens idênticos** têm **20% de desconto**
3. Não é possível vender **mais de 20 itens idênticos**
4. Compras com **menos de 4 itens** não têm desconto

---

## 🧱 Estrutura dos Projetos

### 📦 Ambev.DeveloperEvaluation.Domain

Responsável pelas **entidades, eventos e interfaces de repositório**:

- `Entities/Sale.cs`: entidade principal de venda
- `Entities/SaleItem.cs`: entidade de item da venda
- `Events/SaleEvent.cs`: classe abstrata base para eventos (VendaCriada, VendaCancelada, etc.)
- `Repositories/ISaleRepository.cs`: interface para repositório de vendas
- `Repositories/ISaleItemRepository.cs`: interface para repositório de itens

---

### 💡 Ambev.DeveloperEvaluation.Application

Contém os **DTOs, Services, Mapeamentos e Interfaces**:

- `DTOs/`
  - `SaleDto.cs`
  - `SaleItemDto.cs`
- `Interfaces/`
  - `ISaleService.cs`
  - `ISaleItemService.cs`
  - `IEventPublisher.cs`
- `Services/`
  - `SaleService.cs`
  - `SaleItemService.cs`
  - `LoggingEventPublisher.cs`
- `Mappings/DomainToDTOMappingProfile.cs`: Mapeamento via AutoMapper

---

### 🗃 Ambev.DeveloperEvaluation.ORM

Responsável pelo **acesso ao banco de dados** com Entity Framework Core:

- `Repositories/`
  - `SaleRepository.cs`
  - `SaleItemRepository.cs`
- `DefaultContext.cs`: DbSets e configuração geral
- `YourDbContextFactory.cs`: classe de factory para uso com migrations

> **DbSets incluídos**:
> - `Sales`
> - `SaleItems`

---

### ⚙️ Ambev.DeveloperEvaluation.IoC

Contém a configuração de **injeção de dependência**:

- `InfrastructureModuleInitializer.cs`:
  - Repositórios
  - Serviços
  - Eventos
  - AutoMapper

---

### 🌐 Ambev.DeveloperEvaluation.WebApi

Exposição da **API RESTful** com endpoints organizados por controllers:

- `Controllers/SalesController.cs`: CRUD de vendas
- `Controllers/SalesItemController.cs`: CRUD de itens
- `appsettings.json`: configuração da connection string

#### 🧪 Endpoints disponíveis:

##### 🔹 Vendas (`/api/Sales`)

- `GET /api/Sales`
- `POST /api/Sales`
- `GET /api/Sales/{id}`
- `PUT /api/Sales/{id}`
- `DELETE /api/Sales/{id}`
- `PUT /api/Sales/{id}/cancel/status`

##### 🔸 Itens de Venda (`/api/SalesItem`)

- `GET /api/SalesItem`
- `POST /api/SalesItem`
- `GET /api/SalesItem/{id}`
- `PUT /api/SalesItem/{id}`
- `DELETE /api/SalesItem/{id}`
- `PUT /api/SalesItem/{id}/cancelItem/status`

---

### 🧪 Ambev.DeveloperEvaluation.Unit

Testes unitários com **XUnit**:

- `Domain/Entities/SaleTest.cs`
- `Domain/Entities/SaleItemTest.cs`

---

## 📤 Publicação de Eventos

  Sistema de eventos no padrão **Domain Events**, publicando logs via `LoggingEventPublisher.cs`.

Eventos suportados:

- `VendaCriada`
- `VendaModificada`
- `VendaCancelada`
- `ItemCancelado`

---

## 🚀 Como usar

1. Clone o repositório:
   
  - `git clone https://github.com/Paulo110998/DeveloperStore.git`
  - `cd DeveloperStore`

2. Crie o banco de dados PostgreSQL com o nome "DeveloperEvaluation".
   
3. Atualize o banco com a migration:
   
  - `dotnet ef database update --project src/Ambev.DeveloperEvaluation.ORM --startup-project src/Ambev.DeveloperEvaluation.WebApi`

4. Execute a aplicação:

  - `cd src/Ambev.DeveloperEvaluation.WebApi`
  - `dotnet run`

5. Acesse o Swagger:
   
  - `Disponível em: https://localhost:7181/swagger/index.html`
