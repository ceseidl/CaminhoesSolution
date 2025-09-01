# Caminhoes API

## Descrição
API REST para gerenciamento de caminhões, desenvolvida em .NET 9 com Entity Framework Core e SQLite.

## Instruções de Execução do Back-End

1. **Pré-requisitos:**
   - .NET 9 SDK instalado
   - (Opcional) SQLite instalado para inspeção do banco

2. **Restaurar dependências:**
   ```bash
   dotnet restore Caminhoes.Api
   ```

3. **Aplicar migrations e criar o banco:**
   (O banco será criado automaticamente ao rodar a aplicação)

4. **Executar a API:**
   ```bash
   dotnet run --project Caminhoes.Api
   ```
   A API estará disponível em `https://localhost:5001` ou `http://localhost:5000`.

## Instruções para Rodar os Testes Unitários

1. **Restaurar dependências dos testes:**
   ```bash
   dotnet restore Caminhoes.Tests
   ```
2. **Executar os testes:**
   ```bash
   dotnet test Caminhoes.Tests
   ```

## Instruções de Execução do Front-End (React)

1. **Pré-requisitos:**
   - Node.js e npm instalados

2. **Criar o projeto React:**
   ```bash
   npx create-react-app caminhoes-frontend
   cd caminhoes-frontend
   npm install axios
   ```

3. **Configurar a URL da API:**
   - Crie um arquivo `.env` na raiz do projeto React com o conteúdo:
     ```
     REACT_APP_API_URL=http://localhost:5000
     ```
   - (Ajuste a porta se necessário.)

4. **Adicionar o código do CRUD:**
   - Substitua o conteúdo de `src/App.js` pelo exemplo de CRUD React fornecido neste repositório ou pelo código gerado pelo assistente.

5. **Executar o frontend:**
   ```bash
   npm start
   ```
   O frontend estará disponível em `http://localhost:3000`.

## Endpoints da API

### Listar Caminhões
- **GET** `/api/caminhoes`
- **Resposta:** Lista de caminhões

### Obter Caminhão por Código do Chassi
- **GET** `/api/caminhoes/{codigoChassi}`
- **Resposta:** Caminhão correspondente ou 404 se não encontrado

### Criar Caminhão
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
- **Resposta:** Caminhão criado (201) ou erro de validação

### Atualizar Caminhão
- **PUT** `/api/caminhoes/{codigoChassi}`
- **Body:** (igual ao POST)
- **Resposta:** Caminhão atualizado ou 404 se não encontrado

### Excluir Caminhão
- **DELETE** `/api/caminhoes/{codigoChassi}`
- **Resposta:** 204 No Content ou 404 se não encontrado

## Observações
- O campo `codigoChassi` é único.
- Os valores de `modelo` e `planta` aceitam apenas os valores definidos nos enums.
- Validações são aplicadas via DataAnnotations.

---

Para dúvidas ou sugestões, abra uma issue no repositório.
