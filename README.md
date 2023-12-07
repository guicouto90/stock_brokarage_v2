<div align="center">

# Desafio Técnico - Corretora de investimentos em ações

</div>

## Descrição do projeto:
Desenvolva uma web api que permita aos usuários simular compras e vendas de ações acompanhar seu portfólio e verificar o a variação de valores das ações.
 

#### Requisitos:
1. Implemente um sistema de autenticação para permitir que os usuários façam login e acessem a plataforma (JWT).
2. Crie um endpoint que exiba uma lista de ações disponíveis, incluindo símbolo, nome e preço atual (O preço deve ser gerado de forma aleatória). 
3. Permita que os usuários pesquisem ações pelo nome ou símbolo.
4. Os usuários devem ser capazes de comprar ações usando um saldo fictício em sua conta.
5. Deve existir um endpoint para depósito, saque e extrato da conta fictícia
6. Mantenha um registro das transações de compra e venda de ações.
7. Implemente uma api onde os usuários possam ver suas ações atuais, o preço atual e o valor total.


## Tecnologias utilizadas:

- Asp .Net Core
- SQL SERVER
- Entity Framework
- Autenticação JWT

## Instruções para uso:

- Primeiramente é necessário ter uma instância do SQL Server disponível;
- Configure o arquivo `appsettings.json` que está no projeto `StockBrokarageChallenge.WebApi`  com os seguintes campos:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "sua string de conexão",
  },
  "Jwt": {
    "SecretKey": "Seu secret do Token JWT",
    "Issuer": "Emissor do projeto",
    "Audiencie": "Audiencia do projeto",
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

- Após configurar o `appsettings.json`, e com a instância do banco de dados funcionando, rodar o comando `update-database` para  atualizar o banco de dados com as migrations do projeto.

## Endpoints:

### Customer
<b>`POST /api/Customer`</b>
- Endpoint para criação de um novo cliente com uma conta.
- Exemplo do body para requisição:
```json
{
  "name": "string", // Mínimo 3 caracteres,
  "cpf": "string", // 11 caracteres,
  "password": "string", // Mínimo 6 caracteres,
  "confirmPassword": "string", // Igual ao campo password
}
```

- Exemplo de mensagem quando um é cliente criado com sucesso:
```
Customer created with account number 3
```

### Login
<b>`POST /api/Login`</b>
- Endpoint de autenticação, que gerará um token JWT.
- Exemplo do body para requisição:
```json
{
  "customerCpf": "string", // 11 caracteres,
  "password": "string", // Mínimo 6 caracteres,
  "confirmPassword": "string", // Igual ao campo password
}
```

- Exemplo json de autenticação com sucesso:
```json
{
  "tokenJwt": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJjdXN0b21lcklkIjoiMyIsImFjY291bnRJZCI6IjMiLCJhY2NvdW50TnVtYmVyIjoiMyIsImp0aSI6IjVkNzViYTczLTAyNjktNGU0Zi1hNTc3LTdmNTE1ZjM2OTZmNCIsImV4cCI6MTY5NDkwODYyOCwiaXNzIjoiYWRtaW5fc3RvY2tfYnJvY2thcmFnZV9jaGFsbGVuZ2UiLCJhdWQiOiJjdXN0b21lcnNfc3RvY2tfYnJvY2thcmFnZV9jaGFsbGVuZ2UifQ.h2ifnBsFRV9d6QlhivCSX_skQETj3VVpvqBmviGbe_Q",
  "expiration": "2023-09-16T23:57:08.5125508Z"
}
```

- Exemplo mensagem de erro na autenticação:
```
Cpf/password invalid
```

### Stock
<b>`POST /api/Stock`</b>
- Endpoint para criação de uma nova ação. O preço da ação é gerado randomicamente, e o valor é entre 1.0 e 50.0
- Exemplo do body para requisição:
```json
{
  "name": "string", // Mínimo de 2 caracteres
  "code": "string" // 5 caracteres
}
```

- Exemplo de resposta de uma requisição:
```json
{
  "id": 3,
  "name": "Teste INC",
  "code": "TSTE4",
  "price": 32.18,
  "history": [
    {
      "id": 6,
      "stockId": 3,
      "actualPrice": 32.18,
      "updatedAt": "2023-09-16T20:03:28.9095011-03:00"
    }
  ]
}
```

<b>`GET /api/stock`</b>
- Enpoint para listar todas as ações disponíveis com o seu histórico de preço.
- A cada listagem, atualizará o preço das ações randomicamente. Simulando oscilações que ocorrem no mercado.
- Exemplo de resposta de uma requisição:
```json
[
  {
    "id": 1,
    "name": "VALE",
    "code": "VALE3",
    "price": 17.89,
    "history": [
      {
        "id": 1027,
        "stockId": 1,
        "actualPrice": 34.49,
        "updatedAt": "2023-09-17T15:11:25.6006161"
      },
      {
        "id": 1031,
        "stockId": 1,
        "actualPrice": 17.89,
        "updatedAt": "2023-09-17T15:26:05.0549727-03:00"
      }
    ]
  },
  {
    "id": 2,
    "name": "PETROBRAS",
    "code": "PETR4",
    "price": 13.38,
    "history": [
      {
        "id": 1030,
        "stockId": 2,
        "actualPrice": 36.73,
        "updatedAt": "2023-09-17T15:18:37.8433237"
      },
      {
        "id": 1032,
        "stockId": 2,
        "actualPrice": 13.38,
        "updatedAt": "2023-09-17T15:26:05.0910733-03:00"
      }
    ]
  },
  {
    "id": 1002,
    "name": "Teste INC",
    "code": "TSTE4",
    "price": 20.78,
    "history": [
      {
        "id": 1029,
        "stockId": 1002,
        "actualPrice": 30,
        "updatedAt": "2023-09-17T15:11:25.8189225"
      },
      {
        "id": 1033,
        "stockId": 1002,
        "actualPrice": 20.78,
        "updatedAt": "2023-09-17T15:26:05.1195326-03:00"
      }
    ]
  }
]
```
<b> `GET /api/Stock/code-or-name?filter=` </b>
- Enpoint para listar uma ação pelo nome ou pelo código com o seu histórico de preço.
- A cada listagem, atualizará o preço das ações randomicamente. Simulando oscilações que ocorrem no mercado.
- Exemplo de resposta de uma requisição a url `/api/Stock/code-or-name?filter=PETR4`:
```json
{
  "id": 2,
  "name": "PETROBRAS",
  "code": "PETR4",
  "price": 36.73,
  "history": [
    {
      "id": 1028,
      "stockId": 2,
      "actualPrice": 40.52,
      "updatedAt": "2023-09-17T15:11:25.7866997"
    },
    {
      "id": 1030,
      "stockId": 2,
      "actualPrice": 36.73,
      "updatedAt": "2023-09-17T15:18:37.8433237-03:00"
    }
  ]
}

```

### Account
- Todos os endpoints de `Account` só aceitará requisições autenticadas.
- Para autenticar deve passar o JWT token no header da requisição no seguinte formato:
```json
Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJjdXN0b21lcklkIjoiMyIsImFjY291bnRJZCI6IjMiLCJhY2NvdW50TnVtYmVyIjoiMyIsImp0aSI6IjE4ZjkwZjhhLTc3NTUtNDEwNC05ODk0LTE4NGUwMDUyODhmMyIsImV4cCI6MTY5NDkxMTk2OCwiaXNzIjoiYWRtaW5fc3RvY2tfYnJvY2thcmFnZV9jaGFsbGVuZ2UiLCJhdWQiOiJjdXN0b21lcnNfc3RvY2tfYnJvY2thcmFnZV9jaGFsbGVuZ2UifQ.3WkPmPHbF7iFtbsghLrysprvxGEwcrIZCHywppu1QrY
```
- Requisições sem autenticação retornará a mensagem:

```
Status Code: 401; Unauthorized
```

<b> `PUT /api/Account/deposit` </b>
- Endpoint para deposito na conta criada.
- Exemplo de json no body da requisição:
```json
{
  "value": "double" // Valor não pode ser igual ou menor que 0.0
}
```
- Exemplo de resposta deposito efetuado com sucesso:
```
Deposit succeed
```

<b> `PUT /api/Account/withdraw` </b>
- Endpoint para saque na conta criada.
- Exemplo de json no body da requisição:
```json
{
  "value": "double" // Valor não pode ser igual ou menor que 0.0
}
```
- Exemplo de resposta saque efetuado com sucesso:
```
Withdraw succeed
```
- Exemplo de resposta quando não há saldo suficiente:
```
There is no enough balance to withdraw
```

<b> `PUT /api/Account/buy-stock` </b>
- Endpoint para compra de ações disponíveis.
- Exemplo de json no body da requisição:
```json
{
  "quantity": "integer value", // A quantidade deve ser maior que 0.
  "stockCode": "string" // String com 5 caracteres.
}
```
- Exemplo de resposta saque efetuado com sucesso:
```
Purchase succeed - Quantity: 400, Stock PETR4
```
- Exemplo de resposta quando não há saldo suficiente:
```
There is no enough balance to buy these stocks
```
- Exemplo de resposta quando não encontra uma ação:
```
Stock Not Found
```

<b> `PUT /api/Account/sell-stock` </b>
- Endpoint para venda de ações disponíveis em sua carteira.
- Exemplo de json no body da requisição:
```json
{
  "quantity": "integer value", // A quantidade deve ser maior que 0.
  "stockCode": "string" // String com 5 caracteres.
}
```
- Exemplo de resposta saque efetuado com sucesso:
```
Sold succeed - Quantity: 200, Stock PETR4
```
- Exemplo de resposta quando não há ação selecionada em sua carteira:
```
You dont have this stock in your wallet
```

- Exemplo de resposta quando não há a quantidade informada na venda, é menor que a quantidade que se tem em carteira de uma determinada ação:
```
Quantity must be lower than StockQuantity
```

<b>`GET /api/Account/transaction-history`</b>
- Endpoint que lista todas as transações efetuadas pela conta logada.
- Exemplo json de resposta:
```json
{
  "id": 5,
  "customerId": 5,
  "customer": null,
  "accountNumber": 5,
  "balance": 3195,
  "transactionHistories": [
    {
      "id": 1014,
      "accountId": 5,
      "typeTransaction": "DEPOSIT",
      "transactionValue": 5000,
      "stockCode": null,
      "stockQuantity": null,
      "stockPrice": 0,
      "date": "2023-09-16T22:12:56.9623647"
    },
    {
      "id": 1015,
      "accountId": 5,
      "typeTransaction": "BUY_STOCK",
      "transactionValue": -2314,
      "stockCode": "PETR4",
      "stockQuantity": 200,
      "stockPrice": 11.57,
      "date": "2023-09-16T22:13:11.8413045"
    },
    {
      "id": 1016,
      "accountId": 5,
      "typeTransaction": "BUY_STOCK",
      "transactionValue": -959,
      "stockCode": "VALE3",
      "stockQuantity": 100,
      "stockPrice": 9.59,
      "date": "2023-09-16T22:13:21.2386095"
    },
    {
      "id": 1017,
      "accountId": 5,
      "typeTransaction": "BUY_STOCK",
      "transactionValue": -220,
      "stockCode": "VALE3",
      "stockQuantity": 100,
      "stockPrice": 2.2,
      "date": "2023-09-16T22:19:32.2154068"
    },
    {
      "id": 1018,
      "accountId": 5,
      "typeTransaction": "SELL_STOCK",
      "transactionValue": 352,
      "stockCode": "PETR4",
      "stockQuantity": 100,
      "stockPrice": 3.52,
      "date": "2023-09-16T22:20:02.6914723"
    },
    {
      "id": 1019,
      "accountId": 5,
      "typeTransaction": "SELL_STOCK",
      "transactionValue": 1586,
      "stockCode": "VALE3",
      "stockQuantity": 100,
      "stockPrice": 15.86,
      "date": "2023-09-16T22:20:06.5220538"
    },
    {
      "id": 1020,
      "accountId": 5,
      "typeTransaction": "WITHDRAW",
      "transactionValue": -250,
      "stockCode": null,
      "stockQuantity": null,
      "stockPrice": null,
      "date": "2023-09-17T15:23:59.7689472"
    }
  ]
}
```

<b>`GET /api/Account/stock-transaction-history`</b>
- Endpoint que lista todas as transações de compra e venda de ações efetuadas pela conta logada.
- Exemplo json de resposta:
```json
[
  {
    "id": 1015,
    "accountId": 5,
    "typeTransaction": "BUY_STOCK",
    "transactionValue": -2314,
    "stockCode": "PETR4",
    "stockQuantity": 200,
    "stockPrice": 11.57,
    "date": "2023-09-16T22:13:11.8413045"
  },
  {
    "id": 1016,
    "accountId": 5,
    "typeTransaction": "BUY_STOCK",
    "transactionValue": -959,
    "stockCode": "VALE3",
    "stockQuantity": 100,
    "stockPrice": 9.59,
    "date": "2023-09-16T22:13:21.2386095"
  },
  {
    "id": 1017,
    "accountId": 5,
    "typeTransaction": "BUY_STOCK",
    "transactionValue": -220,
    "stockCode": "VALE3",
    "stockQuantity": 100,
    "stockPrice": 2.2,
    "date": "2023-09-16T22:19:32.2154068"
  },
  {
    "id": 1018,
    "accountId": 5,
    "typeTransaction": "SELL_STOCK",
    "transactionValue": 352,
    "stockCode": "PETR4",
    "stockQuantity": 100,
    "stockPrice": 3.52,
    "date": "2023-09-16T22:20:02.6914723"
  },
  {
    "id": 1019,
    "accountId": 5,
    "typeTransaction": "SELL_STOCK",
    "transactionValue": 1586,
    "stockCode": "VALE3",
    "stockQuantity": 100,
    "stockPrice": 15.86,
    "date": "2023-09-16T22:20:06.5220538"
  }
]
```

<b>`GET /api/Account/wallet`</b>
- Endpoint que lista a carteira de investimento da conta ligada.
- A carteira listará as informações como total investido(`totalInvested`) e saldo atual(`currentBalance`) somando todas as ações disponíveis na carteira.
- A carteira mostrará também os detalhes de cada ação na carteira, e conterá os seguintes campos: o total investido na ação(`totalInvestedStock`), o saldo atual em uma ação(`currentInvestedStock`), o preço médio da ação(`averagePrice`) e a quantidade de ações(`stockQuantity`), e as informações da ação especifica(`stock`).
- Lembrando que, o saldo atual investido em ações oscilará conforme os preços das ações que tem em carteira.
- Exemplo json de resposta:
```json
{
  "totalInvested": 1746.5,
  "currentBalance": 7122,
  "stocksWallet": [
    {
      "averagePrice": 11.57,
      "stockQuantity": 100,
      "totalInvestedStock": 1157,
      "currentInvestedStock": 3673,
      "stock": {
        "id": 2,
        "name": "PETROBRAS",
        "code": "PETR4",
        "price": 36.73,
        "history": []
      }
    },
    {
      "averagePrice": 5.9,
      "stockQuantity": 100,
      "totalInvestedStock": 589.5,
      "currentInvestedStock": 3449,
      "stock": {
        "id": 1,
        "name": "VALE",
        "code": "VALE3",
        "price": 34.49,
        "history": []
      }
    }
  ]
}
```