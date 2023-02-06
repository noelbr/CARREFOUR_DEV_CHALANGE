# CARREFOUR_DEV_CHALANGE

Servido de controle de fluxo de caixa 

## Sobre

Esse servico foi desenvolvido em C# utilizando dotnet.core 7 , Entity Framework e Unity Of Work 
os métodos da API estão documentados com swagger 

## Como executar ?
 
compitar e executar docker
```
cd service_account 
docker build -t service_account:latest .
docker run -d -p 8080:80 service_account
```

Acesse o servico no seu browser 

http://localhost:8080/swagger/

 
# API REST 

- [GET] ACCOUNT : Lista todas as contas 
- [POST] ACCOOUNT : Criar uma nova connta 
- [GET] ACCOUNT/{ID} : Busca uma única conta 

- [POST] ACCOUNT/{ID}/withdrawal : Realiza um débito a conta
- [POST] ACCOUNT/{ID}/deposit :  Realiza um crédito a conta
- [GET] ACCOUNT/{ID}/statement : Extrato lista toda as transações da conta 
- [GET] ACCOUNT/{ID}/balance :  Retorna o Saldo da conta
- [GET] ACCOUNT/{ID}/report : Retorna o Relatorio da conta com filto de dia


