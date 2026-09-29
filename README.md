# SupportWebApp
Jonathan Schmidt – M4.04

## Formål
En simpel Blazor WebApp til at oprette og vise supporthenvendelser. Data gemmes i Azure Cosmos DB.

## Opret Cosmos DB
Kør med Azure CLI.

```powershell
az login
$account = "navn"

az group create -n SupportRG -l westeurope
az cosmosdb create -n $account -g SupportRG --locations regionName=westeurope --capabilities EnableServerless
az cosmosdb sql database create -a $account -g SupportRG -n SupportDB
az cosmosdb sql container create -a $account -g SupportRG -d SupportDB -n SupportMessages --partition-key-path "/category"
```

## Kør projektet
Kør i projektmappen:

```powershell
dotnet user-secrets set "CosmosDb:ConnectionString" "CONNECTION_STRING"
dotnet user-secrets set "CosmosDb:DatabaseName" "SupportDB"
dotnet user-secrets set "CosmosDb:ContainerName" "SupportMessages"
dotnet run --environment Development
```

Connection string gemmes lokalt og skal ikke på GitHub.

## Status
Oprettelse, validering og visning af henvendelser er implementeret. Der mangler bedre fejlbeskeder ved databasefejl. Næste trin kunne være at redigere og slette henvendelser.
