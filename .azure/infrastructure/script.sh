# variables
subscription_name="subscription-poc"
resource_group_name="DefaultResourceGroup-SUK"
default_location="uksouth"
container_registry_name="registry20240830131312"
container_registry_pricing_plan="Standard"
container_app_environment_name="managedEnv20240830130926"
container_app_name="playdotnetapi-container-app"
log_analytics_name="logAnalytics"

# STEP 0: create subscription via Azure Portal UI

# STEP 1: Authenticate & Setup Defaults
az login
az configure --defaults location=$default_location
az account set --subscription $subscription_name

# STEP 2: create resource group
az group create --name $resource_group_name
az configure --defaults group=$resource_group_name
# az configure --list-defaults


# STEP 3: create container registry
az acr create --name $container_registry_name --resource-group $resource_group_name --sku $container_registry_pricing_plan

# STEP 4: create log analytics workspace
az monitor log-analytics workspace create --name $log_analytics_name --resource-group $resource_group_name
log_analytics_workspace_id=$(az monitor log-analytics workspace list --query "[].customerId" --output tsv)

# STEP 5: create container apps environment
az containerapp env create --name $container_app_environment_name --resource-group $resource_group_name --logs-workspace-id "$log_analytics_workspace_id"

# create container apps
az containerapp create --name $container_app_name --resource-group $resource_group_name --environment $container_app_environment_name

# create app service plan?s
# create app insights action group?
# create failure anomalies smart detector reule?
# create app insights?

az deployment group create --name "testarmdeploy01" --resource-group $resource_group_name --template-file .azure/infrastructure/template.json --parameters @.azure/infrastructure/parameters.json