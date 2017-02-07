open Fake.AzureRm
open Fake.AzureRm.Env

[<EntryPoint>]
let main argv = 
    let env = GetEnvironment()
    let rm = new ResourceManager (env.SubscriptionId, env.TenantId, env.ApplicationId, env.Secret)

    let version = 1;
    let location = ``North Europe``
    let webAppSku = WebAppServiceSku.``S1``
    let resource = ( sprintf "fake-azurerm-examples-resource-%i" version )
    let plan = ( sprintf "fake-azurerm-examples-plan-%i" version )
    let appService = ( sprintf "fake-azurerm-examples-app-service-%i" version )

    rm.CreateResourceGroup resource location
        |> Async.RunSynchronously
        |> printf "%A"
    rm.CreateAppServicePlan resource plan webAppSku location 1
        |> Async.RunSynchronously
        |> printf "%A"
    rm.CreateAppService resource plan appService location
        |> Async.RunSynchronously
        |> printf "%A"

    // TODO: Figure out how to upload the app service
    0