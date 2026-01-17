# 🍔 FoodChallenge

**FoodChallenge** é um sistema de gerenciamento de pedidos de comida, desenvolvido em **.NET 9**, seguindo os princípios da **Clean Architecture**, com separação clara entre camadas de negócio, aplicação e infraestrutura. Utiliza **Entity Framework Core**, **PostgreSQL**, **Docker** e **Kubernetes** para orquestração e implantação.

---
## 📚 Índice

- [🔧 Visão Geral da Arquitetura](#-visão-geral-da-arquitetura)
- [🗂️ Estrutura dos Projetos](#-estrutura-dos-projetos)
- [🚀 Tecnologias Utilizadas](#-tecnologias-utilizadas)
- [🎥 Vídeo Explicativo](#vídeo-explicativo)
- [▶️ Como Executar](#-como-executar)
  - [🐳 Subindo com Docker](#-subindo-com-docker)
  - [☸️ Subindo com Minikube](#-subindo-com-minikube)
    - [⚙️ Escalabilidade e Alta Disponibilidade](#escalabilidade-e-alta-disponibilidade)
  - [🔗 APIs Disponíveis](#-apis-disponíveis)
- [🏗️ Arquitetura](#-arquitetura)
- [📈 Fluxo de Requisições (Diagramas de Sequência)](#-fluxo-de-requisições-diagramas-de-sequência)
  - [Diagrama Geral](#-diagrama-geral)
  - [Diagramas por Domínio](#-diagramas-por-domínio)

---

## 🔧 Visão Geral da Arquitetura

A aplicação foi construída seguindo os princípios da Clean Architecture, com o objetivo de manter o núcleo da lógica de negócio isolado de detalhes de implementação e tecnologias externas.
Esse modelo proporciona:

- 🔁 **Alta coesão e baixo acoplamento**
- 🧪 **Facilidade de testes unitários e de integração**
- 🚀 **Manutenção e evolução facilitadas**
- ♻️ **Substituição simples de tecnologias externas sem impacto no domínio**

A estrutura também está alinhada aos conceitos de **DDD (Domain-Driven Design)**, com responsabilidades bem definidas entre as camadas:

- **🔹 Core (Domínio + Application):** Lógica de negócio e casos de uso
- **🔸 Interfaces Adapters:** camadas de adaptação entre core e frameworks
- **🔌 Frameworks and Drivers:** APIs, integrações com bancos de dados, serviços externos, IoC, etc.
---

## 🗂️ Estrutura dos Projetos

```bash
./
├─ diagramas/                                     # Diagramas do projeto em .puml e .png
├─ src/
│  └─ FoodChallenge.Order/
│     ├─ 01 - Core/
│     │  ├─ FoodChallenge.Order.Application              # Casos de uso e regras de aplicação
│     │  └─ FoodChallenge.Order.Domain                   # Entidades, agregados, enums e regras de negócio
│     │
│     ├─ 02 - Interfaces Adapters/
│     │  └─ FoodChallenge.Order.Adapter                  # Adaptadores entre Application e os frameworks externos
│     │
│     ├─ 03 - Frameworks and Drivers/
│     │  ├─ FoodChallenge.Order.Api                                 # API REST (ponto de entrada da aplicação)
│     │  ├─ FoodChallenge.Order.Common                              # Utilitários e constantes compartilhadas
│     │  ├─ FoodChallenge.Order.Infrastructure.Data.Postgres        # Repositórios e DbContext (PostgreSQL)
│     │  ├─ FoodChallenge.Order.Infrastructure.Http                 # Integrações HTTP externas
│     │  └─ FoodChallenge.Order.Ioc                                 # Injeção de dependência e configurações
└─ tools/
   ├─ docker/                                     # Arquivos Docker e docker-compose
   │  ├─ .env                                     # Arquivos com as variáveis de ambiente utilizadas no Docker (Commitadas para testes)
   ├─ k8s/                                        # Arquivos relacionados ao kubernets
   |  ├─ api/                                     # Manifestos relacionados a API
   |  ├─ postgres/                                # Manifestos relacionados ao banco de dados
   |  └─ scripts/                                 # Scripts de inicialização e remoção dos pods
   └─ postman/                                    # Collections e Environment para importação no Postman 
├─ .gitignore                                     # Configurações de ignore do git
└─ README.md                                      # Conteúdo deste documento
```
---

## 🚀 Tecnologias Utilizadas

- [.NET 9](https://dotnet.microsoft.com/download)
- **Entity Framework Core 9**
- **PostgreSQL**
- **Docker/ Docker Compose**
- **Kubernetes / Minikube**
- **Clean Architecture**
- **Princípios de DDD (Domain-Driven Design)**

---

## Vídeos explicativos

[FIAP - Tech Challenge - Fase 4 - Grupo 254]()

--- 

## ▶️ Como Executar

### ✅ Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Docker](https://www.docker.com/)
- [EF Core CLI](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)
- [Minikube](https://minikube.sigs.k8s.io/docs/)

### Migrações do Entity Framework

As migrações do Entity Framework são executadas automaticamente quando a aplicação inicia via Docker. Porém, se precisar executar manualmente ou criar novas migrações, utilize os comandos abaixo:

#### Aplicar Migrações
```bash
# Via dotnet CLI (no diretório do projeto)
cd src/FoodChallenge.Order/FoodChallenge.Order.Api
dotnet ef database update --project ../FoodChallenge.Infrastructure.Data.Postgres --startup-project .
```

#### Criar Nova Migração
```bash
cd src/FoodChallenge.Order/FoodChallenge.Order.Api
dotnet ef migrations add <NomeDaMigracao> --project ../FoodChallenge.Infrastructure.Data.Postgres --output-dir EntityFramework/Migrations
```

#### Remover Última Migração
```bash
cd src/FoodChallenge.Order/FoodChallenge.Order.Api
dotnet ef migrations remove --project ../FoodChallenge.Infrastructure.Data.Postgres
```

> 📝 **Nota**: As migrações devem ser criadas no projeto `FoodChallenge.Infrastructure.Data.Postgres` e aplicadas através do projeto `FoodChallenge.Order.Api` que contém a configuração de startup.

---

### �🐳 Subindo com Docker

```bash
cd tools/docker
docker-compose up -d --build
```

Esse comando irá subir os seguintes serviços:

- **foodchallenge_postgres_db**: banco de dados PostgreSQL
- **foodchallenge_order_migrations**: aplicação das migrations de pedidos
- **foodchallenge_order_api**: aplicação Web API de pedidos (.NET 9)

---

### ☸️ Subindo com Minikube
Instale o Minikube:
```bash
#Macos
brew install minikube

#Windows
choco install minikube
```

Verifique a instalação:
```bash
minikube version
```

Instale as extensions:
```bash
minikube addons list
minikube addons enable metrics-server
```

Inicie o Minikube:
```bash
minikube start --driver=docker
```

#### Obs.: Há duas maneiras de executar os manifestos, sendo a primeira de forma automática com execução do script e a segunda manualmente seguindo a ordem fornecida. 

Opção 1 - Aplique os manifestos do k8s através do script com bash:
```bash
cd tools/k8s/scripts
./deploy.sh
```

Opção 2 - Aplique os manifestos do k8s manualmente seguindo a ordem de execução abaixo:
```bash
cd tools/k8s/

# Aplica o Namespace
kubectl apply -f api/food-challenge-ns.yaml

#POSTGRES - Secrets e Configmap
kubectl apply -f postgres/postgres-configmap.yaml
kubectl apply -f postgres/postgres-init-sql-configmap.yaml
kubectl apply -f postgres/postgres-secrets.yaml

#POSTEGRES - Service e StatefulSet
kubectl apply -f postgres/postgres-service.yaml
kubectl apply -f postgres/postgres-st.yaml

#API - Secrets e Configmap
kubectl apply -f api/food-challenge-configmap.yaml
kubectl apply -f api/food-challenge-secrets.yaml

#API - Service, HPA, Deployment e Ingress
kubectl apply -f api/food-challenge-service.yaml
kubectl apply -f api/food-challenge-hpa.yaml
kubectl apply -f api/food-challenge-deployment.yaml
kubectl apply -f api/food-challenge-ingress.yaml

```

Para expor a api na porta **5000** utilizamos o `port-forward`:
```bash
kubectl port-forward -n fiap-tech-challenge service/food-challenge-api 5000:5000 
```

#### 📄Comandos úteis
```bash
kubectl get pod -n fiap-tech-challenge -l app=food-challenge-api -o wide
kubectl logs -n fiap-tech-challenge -l app=food-challenge-api --tail=100
kubectl describe pod -n fiap-tech-challenge -l app=food-challenge-api
kubectl top pod -n fiap-tech-challenge
```

#### Escalabilidade e Alta Disponibilidade
Para garantir a alta disponibilidade da API durante picos de carga (como lentidão ou timeouts), recomenda-se ajustar a configuração do HPA (Horizontal Pod Autoscaler) e, quando necessário, dos recursos do Deployment.

Você pode fazer isso editando os arquivos abaixo:  
•	**HPA**: tools/k8s/api/food-challenge-hpa.yaml  
•	**Deployment**: tools/k8s/api/food-challenge-deployment.yaml

**Parâmetros importantes no HPA**:  
•	**minReplicas**: define o número mínimo de réplicas, garantindo disponibilidade mesmo em períodos de baixa carga.   
•	**maxReplicas**: especifica o número máximo de réplicas que podem ser criadas automaticamente.   
•	**averageUtilization**: define o percentual de uso da CPU que aciona o autoscaling (ex: 60%).

**Parâmetros importantes no Deployment (resources)**:  
•	**requests**: define os recursos mínimos garantidos para o **POD**. Esses valores são usados pelo HPA para cálculo de utilização.   
•	**limits**: define o limite máximo de recursos que o **POD** pode consumir.

Após editar o YAML, aplique novamente o(s) arquivo(s) alterado(s):
```bash
kubectl apply -f tools/k8s/api/food-challenge-hpa.yaml
kubectl apply -f api/food-challenge-deployment.yaml
```

---

### 🔗 APIs Disponíveis

Após subir os containers, acesse o Swaager localmente:

👉 [http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html)

Ou importe a collection Postman localizada em:

📁 `tools/postman/FoodChallenge.postman_collection.json`

---

## 🏗️ Arquitetura
Os diagramas da arquitetura de infraestrutura e arquitetura limpa na visão macro da API estão disponíveis no link abaixo:

🔗 [FIAP - TC 2 - Clean Arch (Miro)](https://miro.com/app/board/uXjVJXtH4qQ=/?share_link_id=897377031600)
- **Clean Architecture**
![Clean Arch](diagramas/arquitetura/clean_arch.png)


- **Infraestrutura K8S**
![Infra K8S](diagramas/k8s/infra_k8s.png)

## 📈 Fluxo de Requisições (Diagramas de Sequência)
Os diagramas de sequência estão localizados na pasta `diagramas/sequencia/` e foram gerados em [PlantUML](https://plantuml.com/). Eles descrevem os fluxos de interação entre cliente, API e banco.

### Diagramas por Domínio

- **Identificação do Cliente**  
![Identificação](diagramas/sequencia/sequencia_identificacao.png)

- **Pedido**  
![Diagrama Pedido](diagramas/sequencia/sequencia_pedido.png)

- **Ordem Pedido / Preparo**
![Diagrama Ordem Pedido](diagramas/sequencia/sequencia_preparo.png)

- **Produtos**
  - **Criar Produto**  
    ![Criar Produto](diagramas/sequencia/produto/criar_produto.png)
  - **Buscar Produto por ID**  
    ![Buscar Produto por ID](diagramas/sequencia/produto/buscar_produto_por_id.png)
  - **Atualizar Produto**  
    ![Buscar Produto por ID](diagramas/sequencia/produto/atualizar_produto.png)
  - **Deletar Produto**  
    ![Deletar Produto](diagramas/sequencia/produto/deletar_produto.png)
  - **Upload de Imagem do Produto**  
    ![Upload de Imagem do Produto](diagramas/sequencia/produto/upload_imagem_produto.png)
  - **Remover Imagem do Produto**  
    ![Remover Imagem do Produto](diagramas/sequencia/produto/remover_imagem_produto.png)

- **Consulta de Clientes**  
![Cliente](diagramas/sequencia/sequencia_cliente.png)
