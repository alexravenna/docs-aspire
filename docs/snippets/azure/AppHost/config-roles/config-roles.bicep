@description('The location for the resource(s) to be deployed.')
param location string = resourceGroup().location

param config_outputs_name string

param principalType string

param principalId string

resource config 'Microsoft.AppConfiguration/configurationStores@2024-06-01' existing = {
  name: config_outputs_name
}

resource config_AppConfigurationDataOwner 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(config.id, principalId, subscriptionResourceId('Microsoft.Authorization/roleDefinitions', '5ae67dd6-50cb-40e7-96ff-dc2bfa4b606b'))
  properties: {
    principalId: principalId
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', '5ae67dd6-50cb-40e7-96ff-dc2bfa4b606b')
    principalType: principalType
  }
  scope: config
}