namespace App.OAuth

type RegisterAppRequest = {
    client_name: string
    redirect_uris: string
    scopes: string
    website: string
}

type RegisterAppResponse =
    | SuccessfulAppRegistration
    | AppRegistrationError
    
    
type SuccessfulAppRegistration =
    { id: string
      name: string
      website: string
      redirect_uri: string
      client_id: string
      client_secret: string
      vapid_key: string }
    
type AppRegistrationError = { error: string }