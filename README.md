# mzm-safelink
A safe url shortener

## Rodando as migrations do Entity Framework

Para aplicar as migrations ao banco de dados, execute o comando abaixo no terminal, a partir da raiz do projeto:

```sh
dotnet ef database update --startup-project ./mzm-safelink.api --project ./mzm-safelink.infra
```

- `--startup-project` aponta para o projeto de inicialização da aplicação (API).
- `--project` aponta para o projeto onde está o DbContext e as migrations.

Se precisar criar uma nova migration, utilize:

```sh
dotnet ef migrations add NomeDaMigration --startup-project ./mzm-safelink.api --project ./mzm-safelink.infra
```

## Connection string do Supabase

> **Atenção:**  
> Utilize a connection string do tipo **sessionpooler** (only use on a ipv4 network) para conectar ao banco do Supabase.  
> Exemplo de configuração no `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "User Id=postgres.vhuptmociiwrayguacfv;Password=[YOUR-PASSWORD];Server=aws-0-us-east-2.pooler.supabase.com;Port=5432;Database=postgres"
  }
}
```

Substitua `[YOUR-PASSWORD]` pela sua senha do Supabase.