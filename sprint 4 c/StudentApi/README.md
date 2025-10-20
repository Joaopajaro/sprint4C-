# API de Produtos — Trabalho de C#

Este repositório contém o código de uma aplicação **ASP.NET Core Web API** que
implementa um **CRUD** completo para produtos, integra com uma API pública de
piadas e utiliza **Swagger/OpenAPI** para gerar documentação interativa. O
objetivo é apresentar uma solução funcional para o trabalho de faculdade,
privilegiando legibilidade e estruturação em vez de complexidade
profissional.

## Requisitos atendidos

* **CRUD completo (35 %)** – O controlador `ProductsController` expõe
  endpoints para **criar**, **ler**, **atualizar** e **deletar** produtos.
  As operações são implementadas no serviço `ProductService` utilizando o
  **Entity Framework Core** com banco de dados em memória.
* **Pesquisas com LINQ (10 %)** – O método `GetAllAsync` do
  `ProductService` recebe um parâmetro `search` e utiliza **LINQ** para
  filtrar produtos cujo nome ou descrição contém o termo procurado.
* **Publicação em ambiente Cloud (15 %)** – O projeto foi configurado com
  Swagger ativo em todos os ambientes, conforme recomendado pela
  documentação da Microsoft【607809691539310†L144-L178】, facilitando a
  publicação em serviços como **Azure App Service**. Consulte a seção
  *Publicação* para instruções sobre como implantar a API na nuvem.
* **Endpoints conectando com outras APIs (20 %)** – O controlador
  `ExternalController` expõe o endpoint `GET /api/external/joke` que utiliza
  `HttpClient` para buscar uma piada aleatória em uma API pública. O
  serviço `JokeService` encapsula a chamada externa.
* **Documentação do projeto (10 %)** – Este `README.md` explica a
  estrutura, os requisitos atendidos, instruções de uso e sugestões de
  publicação. O Swagger/OpenAPI gera documentação interativa acessível em
  `/swagger`.
* **Arquitetura em diagramas (10 %)** – O diagrama abaixo resume a
  arquitetura da solução, mostrando a separação entre controladores,
  serviços, camadas de acesso a dados e integrações externas.

![Diagrama de arquitetura](architecture.png)

## Estrutura do projeto

```
StudentApi/
├── Controllers/
│   ├── ExternalController.cs
│   └── ProductsController.cs
├── Data/
│   └── ApplicationDbContext.cs
├── Models/
│   ├── Joke.cs
│   └── Product.cs
├── Services/
│   ├── JokeService.cs
│   └── ProductService.cs
├── Program.cs
└── StudentApi.csproj
```

- **Models** – Contêm as classes `Product` e `Joke`. `Product` define as
  propriedades do produto com validações por `DataAnnotations`.
  `Joke` representa o modelo retornado pela API externa.
- **Data** – A classe `ApplicationDbContext` herda de `DbContext` e define o
  `DbSet<Product>`.
- **Services** – `ProductService` encapsula a lógica de negócios e o
  acesso ao banco. `JokeService` executa requisições HTTP usando
  `HttpClient` para a API de piadas.
- **Controllers** – `ProductsController` expõe o CRUD e pesquisa por
  produtos. `ExternalController` expõe o endpoint de piada.
- **Program.cs** – Configura serviços, banco em memória, Swagger,
  injeta serviços e inicializa dados de exemplo.

## Como executar localmente

1. Instale o **SDK .NET 8** ou superior.
2. Clone este repositório e navegue até a pasta `StudentApi`.
3. Restaure os pacotes e execute o projeto:

   ```bash
   dotnet restore
   dotnet run
   ```

4. A aplicação será iniciada em `https://localhost:5001` (ou porta similar).
   A documentação interativa estará disponível em
   `https://localhost:5001/swagger`.

Os primeiros produtos são carregados em memória automaticamente. Use a
interface Swagger para testar os endpoints de CRUD e pesquisa.

## Publicação na nuvem

Uma maneira simples de publicar a API é usar o **Azure App Service**. O
passo a passo básico é:

1. Crie uma conta Azure e um serviço App Service.
2. No Visual Studio ou usando a CLI do .NET, publique o projeto
   selecionando *Azure App Service* como destino. As páginas oficiais da
   Microsoft detalham como criar o projeto com suporte a OpenAPI e como
   movê‑lo para a nuvem【607809691539310†L144-L178】.
3. Após a publicação, o Swagger estará acessível no endereço
   `https://<nome‑da‑app>.azurewebsites.net/swagger`.

Alternativamente, você pode configurar um **GitHub Action** para fazer o
deploy contínuo sempre que houver push no branch principal.

## Observações finais

- Este projeto foi desenvolvido com foco didático. A camada de dados usa
  banco em memória para simplificar a configuração; para um cenário real,
  substitua por `UseSqlServer` ou outro provedor.
- A estrutura em camadas (Controllers → Services → Data) favorece a
  legibilidade e facilita a manutenção.
- O repositório utiliza controle de versão git. Para submissão final,
  hospede este código em um repositório privado (GitHub, Azure DevOps ou
  GitLab) e compartilhe o link de leitura com o professor.

Bom estudo e sucesso no seu trabalho!