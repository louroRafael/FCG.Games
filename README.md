# 🎮 FIAP Cloud Games – Games API

API responsável pelo **gerenciamento de jogos** no ecossistema **FIAP Cloud Games**, centralizando o cadastro, consulta e busca avançada de jogos, além da indexação no **Elasticsearch** para pesquisas eficientes, recomendações e métricas.

---

## 🚀 Tech Challenge – FIAP (Fase 3)

Este projeto faz parte do **Tech Challenge** do curso de pós-graduação em **Arquitetura de Sistemas .NET**, aplicando conceitos de **microsserviços** e **DDD**.

---

## 🧩 Visão Geral da Solução

A **Games API** é um microsserviço independente, responsável exclusivamente pelo domínio de jogos.

---

## 🏗️ Arquitetura do Microsserviço

O projeto está organizado em camadas (DDD), contendo os seguintes projetos:

- **FCG.Games.API** — Expõe endpoints e recebe requisições do cliente.
- **FCG.Games.Service** — Executa regras de negócio, casos de uso e orquestra integrações.
- **FCG.Games.Domain** — Define entidades, enums e regras centrais do domínio de jogos.
- **FCG.Games.Infrastructure** — Implementa persistência, Elasticsearch e integrações externas.

---

## 🔄 Fluxo Principal

### 📦 Cadastro e Indexação de Jogos

1 → Um jogo é criado via Games API  
2 → Os dados são persistidos no banco relacional  
3 → O jogo é indexado no Elasticsearch  
4 → O jogo passa a estar disponível para buscas e recomendações  

---

### 🔍 Busca, Recomendações e Métricas

1 → O cliente realiza uma busca ou consulta de métricas  
2 → A Games API consulta o Elasticsearch  
3 → São retornados:
   - Resultados de busca textual
   - Filtros por gênero e plataforma
   - Recomendações similares
   - Métricas agregadas do catálogo

---

## 📌 Responsabilidades da Games API

- 🎮 Cadastro e manutenção de jogos
- 🔍 Busca avançada (full-text search)
- 🧠 Recomendações por gênero e plataforma
- 📊 Métricas e agregações (preço, distribuição, volume)
- 📨 Publicação de eventos de pedidos no Azure Service Bus

---

## 🔎 Elasticsearch

A Games API utiliza o Elasticsearch para:

- Indexação de jogos
- Busca textual em múltiplos campos
- Filtros por gênero e plataforma
- Agregações para métricas de negócio
- Base para recomendações

---

## 🛠️ Tecnologias Utilizadas

- ⚙️ **Runtime** — [.NET 8 (C#)](https://dotnet.microsoft.com/download/dotnet/8.0)
- 🐘 **Persistência** — [Entity Framework Core](https://learn.microsoft.com/ef/) e [PostgreSQL](https://www.postgresql.org)
- 🔍 **Busca** — [Elasticsearch](https://www.elastic.co/elasticsearch/)
- 🧱 **Validação** — [FluentValidation](https://fluentvalidation.net/)
- 📨 **Mensageria** — [Azure Service Bus](https://learn.microsoft.com/azure/service-bus/)
- 🐳 **Conteinerização** — [Docker](https://www.docker.com)

---

## 🐳 Execução via Docker (Local)

```bash
# Build da imagem
docker build -t fcg-games-api:latest .

# Executar container
docker run -d --name fcg-games-local -p 8080:8080 \
-e ConnectionStrings__FCG="Sua-String-Conexao" \
-e Elastic__Url="http://localhost:9200" \
fcg-games-api:latest
