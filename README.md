# dbserver
Teste para desenvolvedor .NET


Para iniciar o projeto, primeiramente subir o banco MySql utilizando o arquivo ```docker-compose-mysql``` na pasta ```docker```
Após banco inicializado, executar o script para criação de tabela e popular a mesma, script se encontra na pasta:
Docker/Database/CreateTable.sql

Ao colocar o projeto para executar, ele irá abrir a pagina com o Swagger para visualização.

Para fazer o teste inicial, por favor utilize o seguinte json:

```
{
	
	"Valor" : 100.00,
	"Origem": 
	{
		"Agencia": "0001",
		"Numero": "123456",
		"Digito" : "7"
	},
		"Destino": 
	{
		"Agencia": "0001",
		"Numero": "654321",
		"Digito" : "0"
	}
}
```
