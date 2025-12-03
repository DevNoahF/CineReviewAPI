# CineReview – API de Avaliação de Filmes e Séries

O **CineReview** é uma API feita com **ASP.NET Core** que permite cadastrar filmes e séries, registrar avaliações de usuários e gerar rankings com base nas notas.
O projeto foi desenvolvido para fins acadêmicos, praticando **POO**, **API REST**, **LINQ** e **persistência de dados**.

---

## Tecnologias Utilizadas

* ASP.NET Core Web API
* Programação Orientada a Objetos (POO)

  * Herança (Filme e Série → Mídia)
* LINQ
* Entity Framework Core
* Swagger
* Integração com API TMDB

---

## Como Executar o Projeto:

### **Requisitos**

* .NET 6.0 ou superior
* Visual Studio / VS Code

### **Rodando o projeto**

dotnet run


Após iniciar, a API ficará disponível em:

* [http://localhost:5279](http://localhost:5279)
* [https://localhost:7197](https://localhost:7197)

Documentação Swagger:
➡ **/swagger**

---

## Estrutura do Projeto

```
CineReview/
│
├── Controllers/
├── Models/
├── Services/
├── Data/
└── appsettings.json
└── Program.cs
```

---

## Endpoints Principais

### **Mídias**

| Método | Rota          | Descrição              |
| ------ | ------------- | ---------------------- |
| GET    | /midias       | Listar todas as mídias |
| POST   | /midias/filme | Cadastrar filme        |
| POST   | /midias/serie | Cadastrar série        |
| GET    | /midias/{id}  | Buscar mídia por ID    |

### **Avaliações**

| Método | Rota                  | Descrição           |
| ------ | --------------------- | ------------------- |
| POST   | /avaliacoes/{midiaId} | Adicionar avaliação |

### **Ranking**

| Método | Rota            | Descrição                           |
| ------ | --------------- | ----------------------------------- |
| GET    | /midias/ranking | Retorna mídias com maior nota média |

### **Usuários**

| Método | Rota                               | Descrição       |
| ------ | ---------------------------------- | --------------- |
| GET    | /usuarios                          | Listar usuários |
| POST   | /usuarios/{id}/favoritos/{midiaId} | Favoritar mídia |

---

## Conceitos Aplicados

### **1- Herança**

As classes **Filme** e **Serie** herdam de **Midia**, compartilhando atributos e comportamentos comuns.

### **2- Cálculo de Nota Média**

midia.NotaMedia = midia.Avaliacoes.Average(a => a.Nota);

### **3- Ranking**

midias.OrderByDescending(m => m.NotaMedia);

---

## Persistência de Dados

O projeto utiliza:

### **Entity Framework Core**

Persistência em banco de dados relacional via migrations.

---

## Autores

Projeto desenvolvido por:

**Noah Franco**  
**Leonardo Perin**  
**Joaquim Pedro**  
**Gustavo Jesus**  

---
