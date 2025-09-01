# Caminhoes API

## Descri��o
API REST para gerenciamento de caminh�es, desenvolvida em .NET 9 com Entity Framework Core e SQLite.

## Instru��es de Execu��o do Back-End

1. **Pr�-requisitos:**
   - .NET 9 SDK instalado
   - (Opcional) SQLite instalado para inspe��o do banco

2. **Restaurar depend�ncias:**
   ```bash
   dotnet restore Caminhoes.Api
   ```

3. **Aplicar migrations e criar o banco:**
   (O banco ser� criado automaticamente ao rodar a aplica��o)

4. **Executar a API:**
   ```bash
   dotnet run --project Caminhoes.Api
   ```
   A API estar� dispon�vel em `https://localhost:5001` ou `http://localhost:5000`.

## Instru��es para Rodar os Testes Unit�rios

1. **Restaurar depend�ncias dos testes:**
   ```bash
   dotnet restore Caminhoes.Tests
   ```
2. **Executar os testes:**
   ```bash
   dotnet test Caminhoes.Tests
   ```

## Instru��es de Execu��o do Front-End (React)

1. **Pr�-requisitos:**
   - Node.js e npm instalados

2. **Criar o projeto React:**
   ```bash
   npx create-react-app caminhoes-frontend
   cd caminhoes-frontend
   npm install axios
   ```

3. **Configurar a URL da API:**
   - Crie um arquivo `.env` na raiz do projeto React com o conte�do:
     ```
     REACT_APP_API_URL=http://localhost:5000
     ```
   - (Ajuste a porta se necess�rio.)

4. **Adicionar o c�digo do CRUD:**
   - Substitua o conte�do de `src/App.js` pelo exemplo de CRUD React fornecido neste reposit�rio ou pelo c�digo gerado pelo assistente.

5. **Executar o frontend:**
   ```bash
   npm start
   ```
   O frontend estar� dispon�vel em `http://localhost:3000`.

## Endpoints da API

### Listar Caminh�es
- **GET** `/api/caminhoes`
- **Resposta:** Lista de caminh�es

### Obter Caminh�o por C�digo do Chassi
- **GET** `/api/caminhoes/{codigoChassi}`
- **Resposta:** Caminh�o correspondente ou 404 se n�o encontrado

### Criar Caminh�o
- **POST** `/api/caminhoes`
- **Body:**
  ```json
  {
    "modelo": "FH", // ou "FM", "VM"
    "anoFabricacao": 2023,
    "codigoChassi": "ABC123456",
    "cor": "Azul",
    "planta": "Brasil" // ou "Suecia", "EstadosUnidos", "Franca"
  }
  ```
- **Resposta:** Caminh�o criado (201) ou erro de valida��o

### Atualizar Caminh�o
- **PUT** `/api/caminhoes/{codigoChassi}`
- **Body:** (igual ao POST)
- **Resposta:** Caminh�o atualizado ou 404 se n�o encontrado

### Excluir Caminh�o
- **DELETE** `/api/caminhoes/{codigoChassi}`
- **Resposta:** 204 No Content ou 404 se n�o encontrado

## Observa��es
- O campo `codigoChassi` � �nico.
- Os valores de `modelo` e `planta` aceitam apenas os valores definidos nos enums.
- Valida��es s�o aplicadas via DataAnnotations.

---

Para d�vidas ou sugest�es, abra uma issue no reposit�rio.
